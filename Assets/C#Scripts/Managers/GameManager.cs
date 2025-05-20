using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static MapData;
public class GameManager : SingleTons<GameManager>
{
    public GameObject CardPaper;
    public GameObject AngerPanel;
    public GameObject HurtText;
    public GameObject PlayerCanvs;
    public GameObject BossCanvs;
    public PlayerData PlayerData;
    public CharacterStats PlayerStats;
    public CharacterStats BossStats;
    public bool BossActive = true;
    public BossSkillNameList BossSkillNameList;
    public BossSkillList BossSkillList;
    private float CriticalDamageBonus;
    private int BulletCount = 0;//记录保底头奖的子弹数量
    [Header("广播")]
    public VoidEventSO ImpulseEvent;
    public BoundEventSO BoundEvent;
    public VoidEventSO BossDeadEvent;
    protected override void Awake()
    {
        base.Awake();
        BossSkillList.BossSkillNameList_Count = 0;
    }
    private void Update()
    {
        PlayerCanvs.SetActive(PlayerData.StartGame);
        BossCanvs.SetActive(PlayerData.StartGame);
        if(BossStats.CharacterData_Temp.NowHealth <= 0 && BossActive)
        {
            BossActive = false;
            BossStats.gameObject.GetComponent<BossAnim>().OnBossDead();
        }
    }
    public void Attack(CharacterStats Attacker,CharacterStats Defender)
    {
        string attackName = "命中";
        int attackScore = 20;
        if(Defender.gameObject.tag == "Boss")
        {
            attackName = "暴击";
            attackScore = 20;
        }
        if (Defender.CharacterData_Temp.ShengqiCore)
        {
            if(Defender.CharacterData_Temp.AngerValue >= Defender.CharacterData_Temp.FullAnger)
            {
                Defender.CharacterData_Temp.AngerValue = 0;
                return;
            }
        }
        if (!Defender.Invincible)
        {
            var Dodge = UnityEngine.Random.Range(0f, 1f);
            if (Dodge < Defender.CharacterData_Temp.DodgeRate)
            {
                if (Defender.CharacterData_Temp.BouncyJelly)
                {
                    var TrueDodge = UnityEngine.Random.Range(0f, 1f);
                    if(TrueDodge > 0.1f)
                    {
                        if (Player().DodgeBackstab && Defender.gameObject.tag == "Player")
                        {
                            Attack(PlayerStats,BossStats);
                        }
                        Defender.gameObject.GetComponent<SetDodge>().OnSetDodge();
                        return;
                    }
                }
                if (!Defender.CharacterData_Temp.BouncyJelly)
                {
                    if (Player().DodgeBackstab && Defender.gameObject.tag == "Player")
                    {
                        Attack(PlayerStats, BossStats);
                    }
                    Defender.gameObject.GetComponent<SetDodge>().OnSetDodge();
                    return;
                }
            }
            var Critical = UnityEngine.Random.Range(0f, 1f);
            CriticalDamageBonus = 1;
            if (Critical < Attacker.CharacterData_Temp.CriticalDamageRate || BulletCount >= 15)
            {
                BulletCount = 0;
                CriticalDamageBonus += Attacker.CharacterData_Temp.CriticalDamageBonus;
                attackName = "暴击";
                attackScore = 40;
                if (Attacker.CharacterData_Temp.WaterEmblem)
                {
                    CriticalDamageBonus += 0.5f;
                }
            }
            if (Attacker.CharacterData_Temp.MucousRage)
            {
                CriticalDamageBonus += 0.5f * Player().AngerValue;
            }
            if(Attacker.gameObject.tag == "Player" && Attacker.CharacterData_Temp.SpeedEmblem)
            {
                CriticalDamageBonus += (1 - Player().AttackRate) * 0.4f + (Player().SpeedRate - 1) * 0.4f + Player().DodgeRate * 0.2f;
            }
            if (Attacker.CharacterData_Temp.WaterElementBullet)
            {
                CriticalDamageBonus += Attacker.CharacterData_Temp.WaterElementBonus;
                if (Attacker.CharacterData_Temp.AngerSeaGod)
                {
                    Attacker.CharacterData_Temp.AngerValue += 0.02f;
                }
            }
            if (Attacker.CharacterData_Temp.SeaGodMessenger)
            {
                CriticalDamageBonus += 0.2f;
            }
            Defender.CharacterData_Temp.NowHealth -= (Attacker.CharacterData_Temp.AttackPower + Attacker.CharacterData_Temp.WeaponAttackPower) * CriticalDamageBonus * Attacker.CharacterData_Temp.AttackBonus;
            Defender.Invincible = true;
            Defender.InvincibleTime_Count = Defender.CharacterData.InvincibleTime;
            if (Defender.CharacterData_Temp.NowHealth <= 0)
            {
                Defender.CharacterData_Temp.NowHealth = 0;
            }
            switch (Defender.gameObject.tag)
            {
                case "Player":
                    Player().AngerValue += 0.2f;
                    if (Player().LrritableSlime)
                    {
                        Player().AngerValue += 0.1f;
                    }
                    UseImpulse();
                    StartCoroutine(FrameDrop());
                    StartCoroutine(CheckElasticGel());
                    break;
                case "Boss":
                    ScoreManager.Instance.AddScore(attackScore, attackName);
                    HurtText.GetComponent<HurtText>().SetHurtText();
                    if (Attacker.CharacterData_Temp.PoisonBullet)
                    {
                        Defender.gameObject.GetComponent<Poizon>().SetPosizon(Attacker);
                    }
                    if (Player().GuaranteedFirstPrize)
                    {
                        BulletCount++;
                    }
                    if (Player().ElectricStorageSlime)
                    {
                        Defender.gameObject.GetComponent<Thunder>().Thunder_Count++;
                    }
                    Defender.gameObject.GetComponent<Thunder>().SetThunder(Attacker);
                    if (Player().Vulnerability)
                    {
                        var MaxVulnerabilityRate = UnityEngine.Random.Range(0f, 1f);
                        if (MaxVulnerabilityRate < Player().MaxVulnerabilityRate)
                        {
                           Defender.gameObject.GetComponent<MaxVulnerability>().SetVulnerability(Attacker);
                        }
                        else
                        {
                           Defender.gameObject.GetComponent<Vulnerability>().SetVulnerability(Attacker);
                        }
                    }
                    Defender.gameObject.GetComponent<EasyWater>().OnEasyWater(Attacker);
                    if (Attacker.CharacterData_Temp.DangerousBullet)
                    {
                        Defender.gameObject.GetComponent<Dangerous>().SetDangerous(Attacker);
                    }
                    Player().AngerValue += 0.01f;
                    if (Player().LrritableSlime)
                    {
                        Player().AngerValue += 0.02f;
                    }
                    BossCanvs.GetComponent<BossCanvs>().SetDeleteHealth(ColorManager.Instance.UpdateColor(2));
                    break;
                default:
                    break;
            }
        }
    }
    public void Attack(CharacterStats Defender,int Count)
    {
        if (Defender.CharacterData_Temp.ShengqiCore)
        {
            if (Defender.CharacterData_Temp.AngerValue >= Defender.CharacterData_Temp.FullAnger)
            {
                Defender.CharacterData_Temp.AngerValue = 0;
                return;
            }
        }
        if (!Defender.Invincible)
        {
            Defender.CharacterData_Temp.NowHealth -= Count;
            if (Defender.CharacterData_Temp.NowHealth <= 0)
            {
                Defender.CharacterData_Temp.NowHealth = 0;
            }
            switch (Defender.gameObject.tag)
            {
                case "Player":
                    Player().AngerValue += 0.2f;
                    UseImpulse();
                    StartCoroutine(FrameDrop());
                    StartCoroutine(CheckElasticGel());
                    break;
                case "Boss":
                    break;
                default:
                    break;
            }
            Defender.Invincible = true;
            Defender.InvincibleTime_Count = Defender.CharacterData.InvincibleTime;
        }
    }
    private IEnumerator CheckElasticGel()
    {
        if (Player().ElasticGel)
        {
            Player().SpeedRate += 0.5f;
            yield return new WaitForSeconds(1.5f);
            Player().SpeedRate -= 0.5f;
        }
    }
    public void UseImpulse()
    {
        ImpulseEvent.RaiseEvent();
    }
    public void UseFrameDrop()
    {
        StartCoroutine(FrameDrop());
    }
    private IEnumerator FrameDrop()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 1;
    }
    public void RefreshBossSkill()
    {
        if(BossSkillList.BossSkillNameList_Count < 6)
        {
            foreach (var skill in BossSkillList.BossSkills)
            {
                if (BossSkillNameList.BossSkillNames[BossSkillList.BossSkillNameList_Count].Name == skill.SkillName)
                {
                    skill.IsOpen = true;
                    skill.SkillLevel = 1;
                    BossSkillList.BossSkillNameList_Count++;
                    break;
                }
            }
        }
        else
        {
            AddBossSkillLevel();
        }
      
    }
    public void AddBossSkillLevel()
    {
        while (true)
        {
            var Count = UnityEngine.Random.Range(0, BossSkillList.BossSkills.Count);
            if (BossSkillList.BossSkills[Count].IsOpen && BossSkillList.BossSkills[Count].SkillLevel < 5)
            {
                BossSkillList.BossSkills[Count].SkillLevel += 1;
                return;
            }
        }
    }
    public void AddBossHealth()
    {
        BossStats.CharacterData_Temp.MaxHealth = 100 + (PlayerData.CurrentRoomCount - 1) * 50;//恢复最大生命值
        BossStats.CharacterData_Temp.MaxHealth *= BossStats.CharacterData_Temp.HealthRate;//调整最大生命值
        BossStats.CharacterData_Temp.NowHealth = BossStats.CharacterData_Temp.MaxHealth;
    }
    public void RefreshBoss()
    {
        foreach (var skill in BossSkillList.BossSkills)
        {
            skill.IsOpen = false;
            skill.SkillLevel = 0;
        }
        BossSkillList.BossSkillNameList_Count = 0;
        BossStats.CharacterData_Temp = Instantiate(BossStats.CharacterData);
        RefreshBossSkill();
    }
    public void BossDead()
    {
        BossStats.gameObject.SetActive(false);
        Instantiate(CardPaper, BossStats.transform.position, Quaternion.identity);
        if(PlayerData.RoomType == RoomType.BossRoom)
        {
            SceneChangeManager.Instance.Door.SetActive(true);
            var DoorPosition = Physics2D.OverlapPoint(PlayerStats.gameObject.transform.position, SceneChangeManager.Instance.Room).gameObject.transform.position + new Vector3(-21,0,0);
            SceneChangeManager.Instance.Door.transform.position = DoorPosition;
        }
        var GetCoin = UnityEngine.Random.Range(10, 20);
        CoinManager.Instance.GiveCoins(BossStats.gameObject.transform.position,GetCoin);
        ScoreManager.Instance.EndGetScore();
        BossDeadEvent.RaiseEvent();
        SceneChangeManager.Instance.OpenDoorEvent.RaiseEvent();
        MapManager.Instance.AccessRoom(Physics2D.OverlapPoint(PlayerStats.gameObject.transform.position,SceneChangeManager.Instance.Room).gameObject.transform.position);
        DataManager.Instance.Save(DataManager.Instance.Index);//存档
    }
    public void RefreshPlayer()
    {
        PlayerStats.CharacterData_Temp = Instantiate(PlayerStats.CharacterData);
        PlayerStats.CharacterData_Temp.NowHealth = PlayerStats.CharacterData_Temp.MaxHealth;//回复血量
        AngerPanel.SetActive(true);
        PlayerStats.gameObject.GetComponent<PlayerController>().Isdead = false;
        ChangePlayerAngerSkill(1);
        PlayerStats.gameObject.transform.position = new Vector3(-20.64f, -0.44f, 0);
    }
    public void ChangePlayerAngerSkill(int Index)
    {
        PlayerStats.gameObject.GetComponent<PlayerController>().ChangeAngerSkill(Index);
    }
    public void OnBoundEvent(Collider2D collider2D, float Size) 
    {
        BoundEvent.RaiseBoundEvent(collider2D, Size);
    }
    public CharacterData Player()
    {
        return PlayerStats.CharacterData_Temp;
    }
    public CharacterData Boss()
    {
        return BossStats.CharacterData_Temp;
    }
}
