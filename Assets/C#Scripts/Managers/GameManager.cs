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
    public CharacterStats PlayerStats;
    public CharacterStats BossStats;
    public Collider2D MainBounds;
    public GameObject BossBound;
    public ChooseCardList CardList;
    private List<Card> CardList_Choose = new List<Card>();
    public bool BossActive = true;
    private BallType[] BallTypes = new BallType[2];
    public BallType ThisBallType;
    public BossSkillNameList BossSkillNameList;
    public BossSkillList BossSkillList;
    public int BossSkillNameList_Count = 0;
    [Header("¹ã²¥")]
    public BoundEventSO BoundEvent;
    public VoidEventSO ImpulseEvent;
    [Header("ÊÂ¼þ¼àÌý")]
    public BoundEventSO GetBoundEvent;
    private void Update()
    {
        if(BossStats.CharacterData_Temp.NowHealth <= 0 && BossActive)
        {
            BossActive = false;
            BossStats.gameObject.SetActive(false);
        }
    }
    private void OnEnable()
    {
        GetBoundEvent.OnBoundEventRaised += OnGetBound;
    }
    private void OnGetBound(Collider2D target)
    {
        BossBound = target.gameObject;
    }

    public void ReturnMainBounds()
    {
        BoundEvent.BoundRaiseEvent(MainBounds);
    }
    public void Attack(CharacterStats Attacker,CharacterStats Defender)
    {
        Defender.CharacterData_Temp.NowHealth -= Attacker.CharacterData_Temp.AttackPower;
        switch (Defender.gameObject.tag)
        {
            case "Player":
                UseImpulse();
                StartCoroutine(FrameDrop());
                break;
            case "Boss":
                UseImpulse();
                StartCoroutine(FrameDrop());
                break;
            case "BossArmy":
                StartCoroutine(FrameDrop());
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
        switch (Defender.gameObject.tag)
        {
            case "Player":
                UseImpulse();
                StartCoroutine(FrameDrop());
                break;
            case "Boss":
                UseImpulse();
                StartCoroutine(FrameDrop());
                break;
            case "BossArmy":
                StartCoroutine(FrameDrop());
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
            if(ThisBallType == CardList.CardLists[GetCardCount].BallType)
            {
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
    public BallType[] ChooseBallType()
    {
        var BallCountTemp = -1;
        for(int i = 0; i < 2;)
        {
            var BallCount = UnityEngine.Random.Range(0, 2);
            if (BallCountTemp != BallCount)
            {
                BallCountTemp = BallCount;
                switch (BallCountTemp)
                {
                    case 0:
                        BallTypes[i] = BallType.Attack;
                        i++;
                        break;
                    case 1:
                        BallTypes[i] = BallType.Speed;
                        i++;
                        break;
                    default:
                        break;
                }
            }
        }
        return BallTypes;
    }
    public void AddBossHealth()
    {
        BossStats.CharacterData_Temp.NowHealth = BossStats.CharacterData_Temp.MaxHealth + 1;
        BossStats.CharacterData_Temp.MaxHealth += 1;
    }
    public void SetBoss_Close()
    {
        BossStats.gameObject.SetActive(false);
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
    public void ClearBossSkill()
    {
        foreach (var skill in BossSkillList.BossSkills)
        {
            skill.IsOpen = false;
        }
    }
}
