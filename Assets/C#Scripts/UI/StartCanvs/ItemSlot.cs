using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    private GameObject PlayerSlot;
    public ItemData itemData;
    private Button ChooseButton;
    private void Awake()
    {
        itemData = transform.GetChild(0).GetComponent<ItemData>();
        ChooseButton = GetComponent<Button>();
        ChooseButton.onClick.AddListener(OnChooseButton);
    }
    private void Start()
    {
        PlayerSlot = GameManager.Instance.PlayerSlot;
    }
    private void OnChooseButton()
    {
        switch (transform.parent.gameObject.tag)
        {
            case "Weapon":
                PlayerSlot.transform.GetChild(1).GetComponent<Image>().sprite = itemData.ItemImage.sprite;
                PlayerSlot.transform.GetChild(2).GetChild(0).GetComponent<Image>().sprite = itemData.ItemImage.sprite;
                PlayerEquipManager.Instance.ChangeWeapon(itemData.Index);
                break;
            case "Hat":
                PlayerSlot.transform.GetChild(0).GetComponent<Image>().sprite = itemData.ItemImage.sprite;
                PlayerSlot.transform.GetChild(3).GetChild(0).GetComponent<Image>().sprite = itemData.ItemImage.sprite;
                PlayerEquipManager.Instance.ChangeHat(itemData.Index);
                break;
            case "Character":
                PlayerSlot.transform.GetChild(4).GetChild(0).GetComponent<Image>().sprite = itemData.ItemImage.sprite;
                PlayerEquipManager.Instance.ChangeCharacter(itemData.Index);
                break;
            default:
                break;
        }
    }
}
