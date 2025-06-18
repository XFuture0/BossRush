using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlimeDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform LastParent;
    public SlimeData ThisSlimeData;
    public void OnBeginDrag(PointerEventData eventData)
    {
        LastParent = eventData.pointerDrag.transform.parent;
        eventData.pointerDrag.transform.SetParent(transform.parent.parent.parent);
        transform.position = eventData.position;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(210, 140);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
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
                    case "Player":
                        raycastResult.gameObject.GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
                        GameManager.Instance.PlayerData.Player = ThisSlimeData;
                        break;
                    case "Slime1":
                        raycastResult.gameObject.GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
                        GameManager.Instance.PlayerData.Teamer1 = ThisSlimeData;
                        break;
                    case "Slime2":
                        raycastResult.gameObject.GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
                        GameManager.Instance.PlayerData.Teamer2 = ThisSlimeData;
                        break;
                    case "Slime3":
                        raycastResult.gameObject.GetComponent<Image>().sprite = eventData.pointerDrag.GetComponent<Image>().sprite;
                        GameManager.Instance.PlayerData.Teamer3 = ThisSlimeData;
                        break;
                }
            }
        }
        GameManager.Instance.PlayerStats.gameObject.GetComponent<AllPlayerController>().ReFreshTeam();
        eventData.pointerDrag.transform.SetParent(LastParent);
        gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(40, 40);
        DataManager.Instance.Save(DataManager.Instance.Index);//´æµµ
    }
}
