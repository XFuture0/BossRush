using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCanvs : MonoBehaviour
{
    public List<GameObject> ChooseList = new List<GameObject>();
    private List<GameManager.Card> CardList = new List<GameManager.Card>();
    private void OnEnable()
    {
        CardList = GameManager.Instance.GetCards();
        ChangeCard();
    }
    private void ChangeCard()
    {
        for (int i = 0; i < CardList.Count; i++)
        {
            ChooseList[i].transform.GetChild(1).GetComponent<Text>().text = CardList[i].Description;
            ChooseList[i].GetComponent<ChooseCard>().card = CardList[i];
        }
    }
}
