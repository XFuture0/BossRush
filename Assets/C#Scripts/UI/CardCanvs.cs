using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCanvs : MonoBehaviour
{
    public List<Card> ChooseList = new List<Card>();
    private List<ChooseCardList.Card> CardList = new List<ChooseCardList.Card>();
    private void OnEnable()
    {
        KeyBoardManager.Instance.StopMoveKey = true;
        CardList = CardManager.Instance.GetCards();
        ChangeCard();
    }
    private void ChangeCard()
    {
        for (int i = 0; i < CardList.Count; i++)
        {
            ChooseList[i].UpdateCard(CardList[i]);
        }
    }
}
