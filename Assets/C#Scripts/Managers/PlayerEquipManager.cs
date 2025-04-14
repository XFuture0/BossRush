using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipManager : SingleTons<PlayerEquipManager>
{
    public GameObject Bullet;
    public GameObject Weapon;
    public WeaponList WeaponList;
    public HatList HatList;
    private int CurrentIndex;
    private void Start()
    {
        CurrentIndex = 0;
        ChangeWeapon(CurrentIndex);
    }
    public void ChangeWeapon(int Index)
    {
        if (WeaponList.WeaponDatas[Index] != null)
        {
            CurrentIndex = Index;
            Weapon.GetComponent<SpriteRenderer>().sprite = WeaponList.WeaponDatas[Index].WeaponSprite;
            GameManager.Instance.PlayerStats.CharacterData.AttackPower = WeaponList.WeaponDatas[Index].AttackPower;
            Bullet.GetComponent<SpriteRenderer>().sprite = WeaponList.WeaponDatas[Index].BulletSprite;
            Weapon.GetComponent<BulletBox>().ClearPool();
        }
    }
    public void ChangeHat(int Index)
    {

    }
}
