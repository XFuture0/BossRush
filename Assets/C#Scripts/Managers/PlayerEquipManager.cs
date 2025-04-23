using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEquipManager : SingleTons<PlayerEquipManager>
{
    public GameObject Bullet;
    public GameObject Weapon;
    public WeaponList WeaponList;
    public HatList HatList;
    public CharacterList CharacterList;
    private int CurrentWeaponIndex;
    private int CurrentHatIndex;
    private int CurrentCharacterIndex;
    private void Start()
    {
        CurrentWeaponIndex = 0;
        CurrentHatIndex = 0;
        CurrentCharacterIndex = 0;
        ChangeWeapon(CurrentWeaponIndex);
        ChangeHatDescription(CurrentHatIndex);
        ChangeCharacter(CurrentCharacterIndex);
    }
    public void ChangeWeapon(int Index)
    {
        if (WeaponList.WeaponDatas[Index] != null)
        {
            CurrentWeaponIndex = Index;
            GameManager.Instance.PlayerSlot.transform.GetChild(2).GetChild(1).GetComponent<Text>().text = WeaponList.WeaponDatas[Index].Description;
            Weapon.GetComponent<SpriteRenderer>().sprite = WeaponList.WeaponDatas[Index].WeaponSprite;
            GameManager.Instance.PlayerStats.CharacterData.WeaponAttackPower = WeaponList.WeaponDatas[Index].AttackPower;
            Bullet.GetComponent<SpriteRenderer>().sprite = WeaponList.WeaponDatas[Index].BulletSprite;
        }
    }
    public void ChangeHat(int Index)
    {
        if (HatList.HatDatas[Index] != null && HatList.HatDatas[Index].HatInvokeName != "")
        {
            CurrentHatIndex= Index;
            UseHatManager.Instance.StartInvoke(HatList.HatDatas[Index].HatInvokeName);
        }
    }
    public void ChangeHatDescription(int Index)
    {
        GameManager.Instance.PlayerSlot.transform.GetChild(3).GetChild(1).GetComponent<Text>().text = HatList.HatDatas[Index].Description;
    }
    public void ChangeCharacter(int Index)
    {
        if (CharacterList.CharacterDatas[Index] != null)
        {
            CurrentCharacterIndex = Index;
            GameManager.Instance.PlayerSlot.transform.GetChild(4).GetChild(1).GetComponent<Text>().text = CharacterList.CharacterDatas[Index].Description;
            GameManager.Instance.PlayerStats.CharacterData.MaxHealth = CharacterList.CharacterDatas[Index].BaseHealth;
            GameManager.Instance.PlayerStats.CharacterData.AttackPower = CharacterList.CharacterDatas[Index].BaseAttackPower;
            GameManager.Instance.PlayerStats.CharacterData.Speed = CharacterList.CharacterDatas[Index].BaseSpeed;
        }
    }
}
