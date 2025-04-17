using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : SingleTons<CardManager>
{
    public class Card
    {
        public string CardName;
        public string CardInvokeName;
        public string Description;
        public BallType BallType;
        public int index;
    }
    public ChooseCardList CardList;
    private List<Card> CardList_Choose = new List<Card>();
    public List<Card> GetCards()
    {
        CardList_Choose.Clear();
        for (int i = 0; i < 3;)
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
                NewCard.CardName = CardList.CardLists[GetCardCount].CardName;
                NewCard.CardInvokeName = CardList.CardLists[GetCardCount].CardInvokeName;
                NewCard.Description = CardList.CardLists[GetCardCount].Description;
                NewCard.BallType = CardList.CardLists[GetCardCount].BallType;
                CardList_Choose.Add(NewCard);
                i++;
            }
        }
        return CardList_Choose;
    }
}
