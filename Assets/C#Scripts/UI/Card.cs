using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Card : MonoBehaviour
{
    private Button CardButton;
    private CardManager.Card ThisCard;
    private void Awake()
    {
        CardButton = GetComponent<Button>();
        CardButton.onClick.AddListener(UseCard);
    }
    public void UpdateCard(CardManager.Card card)
    {
        ThisCard = card;
        transform.GetChild(0).GetComponent<Text>().text = card.CardName;
        transform.GetChild(1).GetComponent<Text>().text = card.Description;
    }
    private void UseCard()
    {
        UseCardManager.Instance.StartInvoke(ThisCard.CardInvokeName);
        transform.parent.gameObject.SetActive(false);
    }
}
