using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponGemDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Transform LastParent;
    public void OnBeginDrag(PointerEventData eventData)
    {
        DeleteWeapon();
        LastParent = eventData.pointerDrag.transform.parent;
        eventData.pointerDrag.transform.SetParent(transform.parent.parent.parent.parent);
        transform.position = eventData.position;
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
            if (raycastResult.gameObject.name != null && raycastResult.gameObject.tag == "GemSlot" && raycastResult.gameObject.transform.childCount == 1)
            {
                transform.SetParent(raycastResult.gameObject.transform);
                gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                GetWeapon();
                return;
            }
        }
        eventData.pointerDrag.transform.SetParent(LastParent);
        gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        GetWeapon();
    }
    private void DeleteWeapon()
    {
        switch (transform.parent.parent.parent.name)
        {
            case "TeamerSlot":
                GameManager.Instance.PlayerData.Teamer3WeaponSlotCount--;
                break;
            case "TeamerSlot1":
                GameManager.Instance.PlayerData.Teamer2WeaponSlotCount--;
                break;
            case "TeamerSlot2":
                GameManager.Instance.PlayerData.Teamer1WeaponSlotCount--;
                break;
            case "TeamerSlot3":
                GameManager.Instance.PlayerData.PlayerWeaponSlotCount--;
                break;
            case "GemBag":
                GameManager.Instance.PlayerData.FreeWeaponSlotCount--;
                GameManager.Instance.PlayerData.EmptyWeaponSlotCount++;
                break;
        }
    }
    private void GetWeapon()
    {
        switch (transform.parent.parent.parent.name)
        {
            case "TeamerSlot":
                GameManager.Instance.PlayerData.Teamer3WeaponSlotCount++;
                break;
            case "TeamerSlot1":
                GameManager.Instance.PlayerData.Teamer2WeaponSlotCount++;
                break;
            case "TeamerSlot2":
                GameManager.Instance.PlayerData.Teamer1WeaponSlotCount++;
                break;
            case "TeamerSlot3":
                GameManager.Instance.PlayerData.PlayerWeaponSlotCount++;
                break;
            case "GemBag":
                GameManager.Instance.PlayerData.FreeWeaponSlotCount++;
                GameManager.Instance.PlayerData.EmptyWeaponSlotCount--;
                break;
        }
        DataManager.Instance.Save(DataManager.Instance.Index);//´æµµ
    }
}
