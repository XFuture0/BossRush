using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEquipManager : SingleTons<PlayerEquipManager>
{
    public GameObject EquipCanvs;
    public WeaponList WeaponList;
    public HatList HatList;
    public CharacterList CharacterList;
    public void OpenEquipCanvs(int Index)
    {
        KeyBoardManager.Instance.StopAnyKey = true;
        EquipCanvs.transform.GetChild(Index).gameObject.SetActive(true);
    }
    public void ChangeWeapon(int Index)
    {
        if (WeaponList.WeaponDatas[Index] != null)
        {
            GameManager.Instance.PlayerData.WeaponData = WeaponList.WeaponDatas[Index];
            GameManager.Instance.PlayerStats.CharacterData.WeaponAttackPower = WeaponList.WeaponDatas[Index].AttackPower;
        }
    }
    public void ChangeHat(int Index)
    {
        if (HatList.HatDatas[Index] != null)
        {
            GameManager.Instance.PlayerData.HatData = HatList.HatDatas[Index];
        }
    }
    public void UseHat(int Index)
    {
        if (HatList.HatDatas[Index] != null && HatList.HatDatas[Index].HatInvokeName != "")
        {
            UseHatManager.Instance.StartInvoke(HatList.HatDatas[Index].HatInvokeName);
        }
    }
}
