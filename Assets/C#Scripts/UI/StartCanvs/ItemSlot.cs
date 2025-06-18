using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData itemData;
    public Sprite Quesrtion;
    private Button ChooseButton;
    private void Awake()
    {
        itemData = transform.GetChild(0).GetComponent<ItemData>();
        if(transform.parent.gameObject.tag != "Character")
        {
            ChooseButton = GetComponent<Button>();
            ChooseButton.onClick.AddListener(OnChooseButton);
        }
    }
    private void OnChooseButton()
    {
        if (itemData.IsOpen)
        {
            switch (transform.parent.gameObject.tag)
            {
                case "Weapon":
                    PlayerEquipManager.Instance.ChangeWeapon(itemData.Index);
                    break;
                case "Hat":
                    PlayerEquipManager.Instance.ChangeHat(itemData.Index);
                    break;
                default:
                    break;
            }
        }
    }
    private void OnChnageImage()
    {
        switch (transform.parent.gameObject.tag)
        {
            case "Weapon":
                itemData.ItemImage.sprite = PlayerEquipManager.Instance.WeaponList.WeaponDatas[itemData.Index].WeaponSprite;
                break;
            case "Hat":
                itemData.ItemImage.sprite = PlayerEquipManager.Instance.HatList.HatDatas[itemData.Index].HatImage;
                break;
            default:
                break;
        }
    }
    public void CheckOpen()
    {
        if (!itemData.IsOpen)
        {
            GetComponent<Image>().sprite = Quesrtion;
            transform.GetChild(0).GetComponent<Image>().color = new Color(1,1,1,0);
        }
        else
        {
            OnChnageImage();
        }
    }
}
