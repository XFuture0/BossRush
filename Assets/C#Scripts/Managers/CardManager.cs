using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : SingleTons<CardManager>
{
    private ChooseCardList CardList;
    public GameObject CardCanvs;
    private List<ChooseCardList.Card> CardList_Choose = new List<ChooseCardList.Card>();
    [Header("ÊÂ¼þ¼àÌý")]
    public VoidEventSO OpenCardCanvsEvent;
    public float ChangeQuality(Quality quality)
    {
        switch (quality)
        {
            case Quality.Common:
                return 1;
            case Quality.Rare:
                return 0.7f;
            case Quality.Epic:
                return 0.4f;
            case Quality.Legendary:
                return 0.2f;
        }
        return 0f;
    }
    public List<ChooseCardList.Card> GetCards()
    {
        CardList_Choose.Clear();
        for (int i = 0; i < 3;)
        {
            var OpenCount = 0;
            foreach (ChooseCardList.Card card in CardList.CardLists)
            {
                if (card.IsOpen)
                {
                    OpenCount++;
                }
            }
            if(OpenCount < 3)
            {
                return null;
            }
            var HaveCard = false;
            var GetCardCount = UnityEngine.Random.Range(0, CardList.CardLists.Count);
            var CardLevel = UnityEngine.Random.Range(0f, 1f);
            if(CardLevel > ChangeQuality(CardList.CardLists[GetCardCount].Quality))
            {
                HaveCard = true;
            }
            if (!CardList.CardLists[GetCardCount].IsOpen)
            {
                HaveCard = true;
            }
            foreach (var card in CardList_Choose)
            {
                if (card.CardInvokeName == CardList.CardLists[GetCardCount].CardInvokeName)
                {
                    HaveCard = true;
                }
            }
            if (!HaveCard)
            {
                CardList_Choose.Add(CardList.CardLists[GetCardCount]);
                i++;
            }
        }
        return CardList_Choose;
    }
    private void OnEnable()
    {
        OpenCardCanvsEvent.OnEventRaised += OpenCardCanvs;
    }
    public void RefreshCard()
    {
        foreach (var card in CardList.CardLists)
        {
            card.IsOpen = true;
        }
    }
    private void OpenCardCanvs()
    {
        CardCanvs.SetActive(true);
    }

    private void OnDisable()
    {
        OpenCardCanvsEvent.OnEventRaised -= OpenCardCanvs;
    }
    public void SetCardList(ChooseCardList cardList)
    {
        CardList = cardList;
    }
}
