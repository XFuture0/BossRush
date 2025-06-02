using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardCanvs : MonoBehaviour
{
    public GameObject Point;
    public List<Card> ChooseList = new List<Card>();
    private List<ChooseCardList.Card> CardList = new List<ChooseCardList.Card>();
    private void OnEnable()
    {
        KeyBoardManager.Instance.StopMoveKey = true;
        CardList = CardManager.Instance.GetCards();
        ChangeCard();
    }
    private void Update()
    {
        SetPoint();
    }
    private void ChangeCard()
    {
        for (int i = 0; i < CardList.Count; i++)
        {
            ChooseList[i].UpdateCard(CardList[i]);
        }
    }
    private void SetPoint()
    {
        List<RaycastResult> result = new List<RaycastResult>();
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        EventSystem.current.RaycastAll(pointerEventData, result);
        foreach (RaycastResult raycastResult in result)
        {
            if (raycastResult.gameObject.name != null)
            {
                switch (raycastResult.gameObject.name)
                {
                    case "Card":
                        Point.transform.SetParent(ChooseList[0].gameObject.transform);
                        break;
                    case "Card1":
                        Point.transform.SetParent(ChooseList[1].gameObject.transform);
                        break;
                    case "Card2":
                        Point.transform.SetParent(ChooseList[2].gameObject.transform);
                        break;
                }
                Point.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 360);
            }
        }
    }
}
