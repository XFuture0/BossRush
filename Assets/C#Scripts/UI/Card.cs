using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Card : MonoBehaviour
{
    private Button CardButton;
    private ChooseCardList.Card ThisCard;
    private void Awake()
    {
        CardButton = GetComponent<Button>();
        CardButton.onClick.AddListener(UseCard);
    }
    public void UpdateCard(ChooseCardList.Card card)
    {
        ThisCard = card;
        transform.GetChild(0).GetComponent<Text>().text = card.CardName;
        transform.GetChild(1).GetComponent<Text>().text = card.Description;
        transform.GetChild(2).GetComponent<Text>().text = card.Quality.ToString();
    }
    private void UseCard()
    {
        ThisCard.IsOpen = false;
        UseCardManager.Instance.StartInvoke(ThisCard.CardInvokeName);
        KeyBoardManager.Instance.StopMoveKey = false;
        SceneChangeManager.Instance.Door.GetComponent<Door>().OpenDoor();
        SceneChangeManager.Instance.OpenDoorEvent.RaiseEvent();
        transform.parent.gameObject.SetActive(false);
    }
}
