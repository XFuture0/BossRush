using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
public class GameManager : SingleTons<GameManager>
{
    public class Card
    {
        public string Description;
        public int Health;
        public int AttackPower;
        public BallType BallType;
        public int index;
    }
    public GameObject PlayerSlot;
    public PlayerData PlayerData;
    public CharacterStats PlayerStats;
    public CharacterStats BossStats;
    public ChooseCardList CardList;
    private List<Card> CardList_Choose = new List<Card>();
    public bool BossActive = true;
    public BossSkillNameList BossSkillNameList;
    public BossSkillList BossSkillList;
    public int BossSkillNameList_Count;
    private float CriticalDamageBonus;
    [Header("¹ã²¥")]
    public VoidEventSO ImpulseEvent;
    public BoundEventSO BoundEvent;
    protected override void Awake()
    {
        base.Awake();
        BossSkillNameList_Count = 0;
    }
    private void Update()
    {
        if(BossStats.CharacterData_Temp.NowHealth <= 0 && BossActive)
        {
            BossDead();
        }
    }
    public void Attack(CharacterStats Attacker,CharacterStats Defender)
    {
        var Dodge = UnityEngine.Random.Range(0f, 1f);
        if(Dodge < Defender.CharacterData_Temp.DodgeRate)
        {
            return;
        }
        var Critical = UnityEngine.Random.Range(0f, 1f);
        CriticalDamageBonus = 1;
        if(Critical < Attacker.CharacterData_Temp.CriticalDamageRate)
        {
            CriticalDamageBonus += Attacker.CharacterData_Temp.CriticalDamageBonus;
        }
        Defender.CharacterData_Temp.NowHealth -= Attacker.CharacterData_Temp.AttackPower * CriticalDamageBonus;
        if(Defender.CharacterData_Temp.NowHealth <= 0)
        {
            Defender.CharacterData_Temp.NowHealth = 0;
        }
        switch (Defender.gameObject.tag)
        {
            case "Player":
                UseImpulse();
                StartCoroutine(FrameDrop());
                break;
            case "Boss":
                break;
            default:
                break;
        }
        Defender.Invincible = true;
        Defender.InvincibleTime_Count = Defender.CharacterData.InvincibleTime;
    }
    public void Attack(CharacterStats Defender,int Count)
    {
        Defender.CharacterData_Temp.NowHealth -= Count;
        if (Defender.CharacterData_Temp.NowHealth <= 0)
        {
            Defender.CharacterData_Temp.NowHealth = 0;
        }
        switch (Defender.gameObject.tag)
        {
            case "Player":
                UseImpulse();
                StartCoroutine(FrameDrop());
                break;
            case "Boss":
                break;
            default:
                break;
        }
        Defender.Invincible = true;
        Defender.InvincibleTime_Count = Defender.CharacterData.InvincibleTime;
    }
    public List<Card> GetCards()
    {
        CardList_Choose.Clear();
        for(int i = 0; i < 3;)
        {
            var HaveCard = false;
            var GetCardCount = UnityEngine.Random.Range(0, CardList.CardLists.Count);
            foreach (var card in CardList_Choose)
            {
                if (card.index == GetCardCount)
                {
                    HaveCard = true;
                }
            }
            if (!HaveCard)
            {
                var NewCard = new Card();
                NewCard.index = GetCardCount;
                NewCard.Description = CardList.CardLists[GetCardCount].Description;
                NewCard.Health = CardList.CardLists[GetCardCount].Health;
                NewCard.AttackPower = CardList.CardLists[GetCardCount].AttackPower;
                NewCard.BallType = CardList.CardLists[GetCardCount].BallType;
                CardList_Choose.Add(NewCard);
                i++;
            }
        }
        return CardList_Choose;
    }
    public void UseCard(Card card)
    {
        PlayerStats.CharacterData_Temp.NowHealth += card.Health;
        PlayerStats.CharacterData_Temp.AttackPower += card.AttackPower;
    }
    public void UseImpulse()
    {
        ImpulseEvent.RaiseEvent();
    }
    private IEnumerator FrameDrop()
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 1;
    }
    public void RefreshBossSkill()
    {
        foreach (var skill in BossSkillList.BossSkills)
        {
            if(BossSkillNameList.BossSkillName[BossSkillNameList_Count] == skill.SkillName)
            {
                skill.IsOpen = true;
                BossSkillNameList_Count++;
                break;
            }
        }
    }
    public void AddBossHealth()
    {
        BossStats.CharacterData_Temp.NowHealth = BossStats.CharacterData_Temp.MaxHealth + BossStats.CharacterData_Temp.HealCount;
        BossStats.CharacterData_Temp.MaxHealth = BossStats.CharacterData_Temp.NowHealth;
    }
    public void ClearBossSkill()
    {
        foreach (var skill in BossSkillList.BossSkills)
        {
            skill.IsOpen = false;
        }
        BossSkillNameList_Count = 0;
    }
    public void RefreshBoss()
    {
        BossStats.gameObject.SetActive(true);
        foreach (var skill in BossSkillList.BossSkills)
        {
            skill.IsOpen = false;
        }
        BossSkillNameList_Count = 0;
        BossStats.CharacterData_Temp = Instantiate(BossStats.CharacterData);
        BossStats.gameObject.transform.position = new Vector3(-15f, 0.97f, 0);
    }
    private void BossDead()
    {
        BossActive = false;
        BossStats.gameObject.SetActive(false);
        SceneChangeManager.Instance.Door.SetActive(true);
        SceneChangeManager.Instance.Door.GetComponent<Door>().OpenDoor();
    }
    public void RefreshPlayer()
    {
        PlayerStats.CharacterData_Temp = Instantiate(PlayerStats.CharacterData);
        PlayerStats.gameObject.GetComponent<PlayerController>().Isdead = false;
        PlayerStats.gameObject.transform.position = new Vector3(-20.64f, -0.44f, 0);
    }
    public void OnBoundEvent(Collider2D collider2D) 
    {
        BoundEvent.RaiseBoundEvent(collider2D);
    }
}
