using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipCanvs : MonoBehaviour
{
    public Button ReturnButton_Weapon;
    public Button ReturnButton_Hat;
    public Button ReturnButton_Character;
    private void Awake()
    {
        ReturnButton_Hat.onClick.AddListener(CloseHatUI);
        ReturnButton_Character.onClick.AddListener(CloseCharacterUI);
        ReturnButton_Weapon.onClick.AddListener(CloseWeaponUI);
    }
    public void CloseWeaponUI()
    {
        transform.GetChild(2).gameObject.SetActive(false);
        KeyBoardManager.Instance.StopAnyKey = false;
    }
    public void CloseHatUI()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        KeyBoardManager.Instance.StopAnyKey = false;
    }
    public void CloseCharacterUI()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        KeyBoardManager.Instance.StopAnyKey = false;
    }
}
