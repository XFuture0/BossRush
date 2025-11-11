using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponGemDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public ExtraGemData.ExtraGem ThisExtraGem;
    private Transform LastParent;
    public bool IsWeaponGem;
    public bool IsExtraGem;
    public void OnBeginDrag(PointerEventData eventData)
    {
        IsWeaponGem = false;
        IsExtraGem = false;
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
                GetWeapon(eventData);
                return;
            }
        }
        eventData.pointerDrag.transform.SetParent(LastParent);
        gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        GetWeapon(eventData);
    }
    private void DeleteWeapon()
    {
        switch (transform.parent.parent.parent.name)
        {
            case "TeamerSlot":
                if(ThisExtraGem.GemBonus == 0)
                {
                    GameManager.Instance.PlayerData.Teamer3WeaponSlotCount--;
                    IsWeaponGem = true;
                }
                else if(ThisExtraGem.GemBonus != 0)
                {
                    ReMoveExtraGem(GameManager.Instance.PlayerData.Teamer3ExtraGemData);
                    IsExtraGem = true;
                }
                break;
            case "TeamerSlot1":
                if (ThisExtraGem.GemBonus == 0)
                {
                    GameManager.Instance.PlayerData.Teamer2WeaponSlotCount--;
                    IsWeaponGem = true;
                }
                else if (ThisExtraGem.GemBonus != 0)
                {
                    ReMoveExtraGem(GameManager.Instance.PlayerData.Teamer2ExtraGemData);
                    IsExtraGem = true;
                }
                break;
            case "TeamerSlot2":
                if (ThisExtraGem.GemBonus == 0)
                {
                    GameManager.Instance.PlayerData.Teamer1WeaponSlotCount--;
                    IsWeaponGem = true;
                }
                else if (ThisExtraGem.GemBonus != 0)
                {
                    Debug.Log(1);
                    ReMoveExtraGem(GameManager.Instance.PlayerData.Teamer1ExtraGemData);
                    IsExtraGem = true;
                }
                break;
            case "TeamerSlot3":
                if (ThisExtraGem.GemBonus == 0)
                {
                    GameManager.Instance.PlayerData.PlayerWeaponSlotCount--;
                    IsWeaponGem = true;
                }
                else if (ThisExtraGem.GemBonus != 0)
                {
                    ReMoveExtraGem(GameManager.Instance.PlayerData.PlayerExtraGemData);
                    IsExtraGem = true;
                }
                break;
            case "WeaponGemBag":
                IsWeaponGem = true;
                GameManager.Instance.PlayerData.FreeWeaponSlotCount--;
                GameManager.Instance.PlayerData.EmptyWeaponSlotCount++;
                break;
            case "ExtraGemBag":
                IsExtraGem = true;
                ReMoveExtraGem(GameManager.Instance.PlayerData.ExtraGemData);
                GameManager.Instance.PlayerData.ExtraGemData.EmptyGemSlotCount++;
                break;
        }
    }
    private void GetWeapon(PointerEventData eventData)
    {
        switch (transform.parent.parent.parent.name)
        {
            case "TeamerSlot":
                if (IsWeaponGem)
                {
                    GameManager.Instance.PlayerData.Teamer3WeaponSlotCount++;
                }
                else if (IsExtraGem)
                {
                    GameManager.Instance.PlayerData.Teamer3ExtraGemData.ExtraGemList.Add(ThisExtraGem);
                }
                break;
            case "TeamerSlot1":
                if (IsWeaponGem)
                {
                    GameManager.Instance.PlayerData.Teamer2WeaponSlotCount++;
                }
                else if (IsExtraGem)
                {
                    GameManager.Instance.PlayerData.Teamer2ExtraGemData.ExtraGemList.Add(ThisExtraGem);
                }
                break;
            case "TeamerSlot2":
                if (IsWeaponGem)
                {
                    GameManager.Instance.PlayerData.Teamer1WeaponSlotCount++;
                }
                else if (IsExtraGem)
                {
                    GameManager.Instance.PlayerData.Teamer1ExtraGemData.ExtraGemList.Add(ThisExtraGem);
                }
                break;
            case "TeamerSlot3":
                if (IsWeaponGem)
                {
                    GameManager.Instance.PlayerData.PlayerWeaponSlotCount++;
                }
                else if (IsExtraGem)
                {
                    GameManager.Instance.PlayerData.PlayerExtraGemData.ExtraGemList.Add(ThisExtraGem);
                }
                break;
            case "WeaponGemBag":
                if (IsExtraGem)
                {
                    eventData.pointerDrag.transform.SetParent(LastParent);
                    gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    GetWeapon(eventData);
                    return;
                }
                GameManager.Instance.PlayerData.FreeWeaponSlotCount++;
                GameManager.Instance.PlayerData.EmptyWeaponSlotCount--;
                break;
            case "ExtraGemBag":
                if (IsWeaponGem)
                {
                    eventData.pointerDrag.transform.SetParent(LastParent);
                    gameObject.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    GetWeapon(eventData);
                    return;
                }
                GameManager.Instance.PlayerData.ExtraGemData.ExtraGemList.Add(ThisExtraGem);
                GameManager.Instance.PlayerData.ExtraGemData.EmptyGemSlotCount--;
                break;
        }
        GameManager.Instance.PlayerStats.gameObject.GetComponent<AllPlayerController>().RefreshTeamExtraGemBonus();
        DataManager.Instance.Save(DataManager.Instance.Index);//´æµµ
    }
    private void ReMoveExtraGem(ExtraGemData Extragemdata)
    {
        for(int i = 0;i < Extragemdata.ExtraGemList.Count; i++)
        {
            if(ThisExtraGem.GemType == Extragemdata.ExtraGemList[i].GemType && ThisExtraGem.GemBonus == Extragemdata.ExtraGemList[i].GemBonus)
            {
                Extragemdata.ExtraGemList.RemoveAt(i);
                return;
            }
        }
    }
}
