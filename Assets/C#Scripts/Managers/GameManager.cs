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
    public PlayerData PlayerData;
    public CharacterStats PlayerStats;
    public CharacterStats BossStats;
    public ChooseCardList CardList;
    public GameObject Coin;
    private List<Card> CardList_Choose = new List<Card>();
    public bool BossActive = true;
    public BossSkillNameList BossSkillNameList;
    public BossSkillList BossSkillList;
    public int BossSkillNameList_Count;
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
            BossActive = false;
            BossStats.gameObject.SetActive(false);
        }
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
    public void AddBossHealth()
    {
        BossStats.CharacterData_Temp.NowHealth = BossStats.CharacterData_Temp.MaxHealth + 1;
        BossStats.CharacterData_Temp.MaxHealth += 1;
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
    public void OnBoundEvent(Collider2D collider2D) 
    {
        BoundEvent.RaiseBoundEvent(collider2D);
    }
    public void AddCoin()
    {
        PlayerData.CoinCount++;
    }
    public void GetBonus()
    {
        for(int i = 0;i < 10; i++)
        {
            var CoinPosition = new Vector3(-14.91f, 10.95f, 0);
            Instantiate(Coin, CoinPosition, Quaternion.identity, transform);
        }
    }
}
