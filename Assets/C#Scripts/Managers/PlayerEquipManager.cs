using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
    public void ChangeWeapon(int Index)
    {
        if (WeaponList.WeaponDatas[Index] != null)
        {
            CurrentWeaponIndex = Index;
            Weapon.GetComponent<SpriteRenderer>().sprite = WeaponList.WeaponDatas[Index].WeaponSprite;
            GameManager.Instance.PlayerStats.CharacterData.AttackPower = WeaponList.WeaponDatas[Index].AttackPower;
            Bullet.GetComponent<SpriteRenderer>().sprite = WeaponList.WeaponDatas[Index].BulletSprite;
            Weapon.GetComponent<BulletBox>().ClearPool();
        }
    }
    public void ChangeHat(int Index)
    {
        if (HatList.HatDatas[Index] != null)
        {
            CurrentHatIndex= Index;
        }
    }
    public void ChangeCharacter(int Index)
    {
        if (CharacterList.CharacterDatas[Index] != null)
        {
            CurrentCharacterIndex = Index;
        }
    }
}
