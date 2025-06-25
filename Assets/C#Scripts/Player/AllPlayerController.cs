using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllPlayerController : MonoBehaviour
{
    [System.Serializable]
    public class ExtraGemBonus
    {
        public float ShootBonus;
        public float DamageBonus;
        public float SpeedBonus;
        public float BiggerBonus;
    }
    public ExtraGemBonus TeamerBonus;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private int TeamerCount;
    public GameObject WeaponBox;
    public PlayerData PlayerData;
    [Header("小队成员")]
    public GameObject Teamer1;
    public GameObject Teamer2;
    public GameObject Teamer3;
    [Header("属性值继承")]
    private float Speed;//移动速度
    private float HeartCount;//生命值
    private float ShieldCount;//护盾值
    [Header("射程")]
    public CircleCollider2D PlayerCollider;
    public CircleCollider2D Teamer1Collider;
    public CircleCollider2D Teamer2Collider;
    public CircleCollider2D Teamer3Collider;
    [Header("属性")]
    public Weapon PlayerWeapon;
    public Weapon Teamer1Weapon;
    public Weapon Teamer2Weapon;
    public Weapon Teamer3Weapon;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    public void ReFreshTeam()
    {
        HeartCount = PlayerData.Player.HeartCount;
        ShieldCount = PlayerData.Player.ShieldCount;
        Speed = PlayerData.Player.Speed;
        PlayerCollider.radius = PlayerData.Player.Distance;
        PlayerData.PlayerWeaponSlotCount = PlayerData.Player.BaseWeaponCount;
        PlayerWeapon.AttackPower = PlayerData.Player.BasePower;
        PlayerWeapon.AttackSpeedTime = PlayerData.Player.BaseAttackSpeedTime;
        PlayerWeapon.BulletLarge = PlayerData.Player.BaseBulletLarge;
        GameManager.Instance.PlayerData.PlayerExtraGemData.ExtraGemList.Clear();
        GameManager.Instance.PlayerData.PlayerExtraGemData.EmptyGemSlotCount = 0;
        RefreshSlimeData(PlayerData.Player);
        TeamerCount = 1;
        if(PlayerData.Teamer1 == null)
        {
            Teamer1.SetActive(false);
        }
        else
        {
            TeamerCount++;
            Teamer1.SetActive(true);
            Teamer1.GetComponent<PlayerTeamer>().RefreshSlimeData(PlayerData.Teamer1);
            HeartCount += PlayerData.Teamer1.HeartCount;
            ShieldCount += PlayerData.Teamer1.ShieldCount;
            Teamer1Collider.radius = PlayerData.Teamer1.Distance; 
            PlayerData.Teamer1WeaponSlotCount = PlayerData.Teamer1.BaseWeaponCount;
            Teamer1Weapon.AttackPower = PlayerData.Teamer1.BasePower;
            Teamer1Weapon.AttackSpeedTime = PlayerData.Teamer1.BaseAttackSpeedTime;
            Teamer1Weapon.BulletLarge = PlayerData.Teamer1.BaseBulletLarge;
            GameManager.Instance.PlayerData.Teamer1ExtraGemData.ExtraGemList.Clear();
            GameManager.Instance.PlayerData.Teamer1ExtraGemData.EmptyGemSlotCount = 0;
        }
        if(PlayerData.Teamer2 == null)
        {
            Teamer2.SetActive(false);
        }
        else
        {
            TeamerCount++;
            Teamer2.SetActive(true);
            Teamer2.GetComponent<PlayerTeamer>().RefreshSlimeData(PlayerData.Teamer2);
            HeartCount += PlayerData.Teamer2.HeartCount;
            ShieldCount += PlayerData.Teamer2.ShieldCount;
            Teamer2Collider.radius = PlayerData.Teamer2.Distance;
            PlayerData.Teamer2WeaponSlotCount = PlayerData.Teamer2.BaseWeaponCount;
            Teamer2Weapon.AttackPower = PlayerData.Teamer2.BasePower;
            Teamer2Weapon.AttackSpeedTime = PlayerData.Teamer2.BaseAttackSpeedTime;
            Teamer2Weapon.BulletLarge = PlayerData.Teamer2.BaseBulletLarge;
            GameManager.Instance.PlayerData.Teamer2ExtraGemData.ExtraGemList.Clear();
            GameManager.Instance.PlayerData.Teamer2ExtraGemData.EmptyGemSlotCount = 0;
        }
        if (PlayerData.Teamer3 == null)
        {
            Teamer3.SetActive(false);
        }
        else
        {
            TeamerCount++;
            Teamer3.SetActive(true);
            Teamer3.GetComponent<PlayerTeamer>().RefreshSlimeData(PlayerData.Teamer3);
            HeartCount += PlayerData.Teamer3.HeartCount;
            ShieldCount += PlayerData.Teamer3.ShieldCount;
            Teamer3Collider.radius = PlayerData.Teamer3.Distance;
            PlayerData.Teamer3WeaponSlotCount = PlayerData.Teamer3.BaseWeaponCount;
            Teamer3Weapon.AttackPower = PlayerData.Teamer3.BasePower;
            Teamer3Weapon.AttackSpeedTime = PlayerData.Teamer3.BaseAttackSpeedTime;
            Teamer3Weapon.BulletLarge = PlayerData.Teamer3.BaseBulletLarge;
            GameManager.Instance.PlayerData.Teamer3ExtraGemData.ExtraGemList.Clear();
            GameManager.Instance.PlayerData.Teamer3ExtraGemData.EmptyGemSlotCount = 0;
        }
        GameManager.Instance.PlayerStats.CharacterData_Temp.MaxHealth = HeartCount;
        GameManager.Instance.PlayerStats.CharacterData_Temp.NowHealth = HeartCount;
        GameManager.Instance.PlayerStats.CharacterData_Temp.Speed = Speed;
        GameManager.Instance.PlayerStats.CharacterData_Temp.Shield = ShieldCount;
        SetBoxCollider();
    }
    public void RefreshSlimeData(SlimeData slimeData)
    {
        anim.runtimeAnimatorController = slimeData.SlimeAnim;
        WeaponBox.GetComponent<Weapon>().SlimeData = slimeData;
    }
    private void SetBoxCollider()
    {
        switch (TeamerCount) 
        {
            case 1:
                boxCollider.offset = new Vector2(0, 0);
                boxCollider.size = new Vector2(1.5f, 1f);
                break;
            case 2:
                boxCollider.offset = new Vector2(0, 0.4f);
                boxCollider.size = new Vector2(1.5f, 1.8f);
                break;
            case 3:
                boxCollider.offset = new Vector2(0, 0.8f);
                boxCollider.size = new Vector2(1.5f, 2.6f);
                break;
            case 4:
                boxCollider.offset = new Vector2(0, 1.2f);
                boxCollider.size = new Vector2(1.5f,3.4f);
                break;
        }
    }
    public void OpenTeam()
    {
        RefreshSlimeData(PlayerData.Player);
        TeamerCount = 1;
        if (PlayerData.Teamer1 == null)
        {
            Teamer1.SetActive(false);
        }
        else
        {
            TeamerCount++;
            Teamer1.SetActive(true);
            Teamer1.GetComponent<PlayerTeamer>().RefreshSlimeData(PlayerData.Teamer1);
        }
        if (PlayerData.Teamer2 == null)
        {
            Teamer2.SetActive(false);
        }
        else
        {
            TeamerCount++;
            Teamer2.SetActive(true);
            Teamer2.GetComponent<PlayerTeamer>().RefreshSlimeData(PlayerData.Teamer2);
        }
        if (PlayerData.Teamer3 == null)
        {
            Teamer3.SetActive(false);
        }
        else
        {
            TeamerCount++;
            Teamer3.SetActive(true);
            Teamer3.GetComponent<PlayerTeamer>().RefreshSlimeData(PlayerData.Teamer3);
        }
        SetBoxCollider();
        RefreshTeamExtraGemBonus();
    }
    public void RefreshTeamExtraGemBonus()
    {
        CheckExtraGemBonus(GameManager.Instance.PlayerData.PlayerExtraGemData);
        if(PlayerData.Teamer1 != null)
        {
            Teamer1.GetComponent<PlayerTeamer>().CheckExtraGemBonus(GameManager.Instance.PlayerData.Teamer1ExtraGemData, GameManager.Instance.PlayerData.Teamer1);
        }
        if (PlayerData.Teamer2 != null)
        {
            Teamer2.GetComponent<PlayerTeamer>().CheckExtraGemBonus(GameManager.Instance.PlayerData.Teamer2ExtraGemData, GameManager.Instance.PlayerData.Teamer2);
        }
        if (PlayerData.Teamer3 != null)
        {
            Teamer3.GetComponent<PlayerTeamer>().CheckExtraGemBonus(GameManager.Instance.PlayerData.Teamer3ExtraGemData, GameManager.Instance.PlayerData.Teamer3);
        }
    }
    public void CheckExtraGemBonus(ExtraGemData extraGemData)
    {
        TeamerBonus.ShootBonus = 0;
        TeamerBonus.DamageBonus = 0;
        TeamerBonus.SpeedBonus = 0;
        TeamerBonus.BiggerBonus = 0;
        foreach (var extragem in extraGemData.ExtraGemList)
        {
            switch (extragem.GemType)
            {
                case GemType.ShootGem:
                    TeamerBonus.ShootBonus += extragem.GemBonus;
                    break;
                case GemType.DamageGem:
                    TeamerBonus.DamageBonus += extragem.GemBonus;
                    break;
                case GemType.SpeedGem:
                    TeamerBonus.SpeedBonus += extragem.GemBonus;
                    break;
                case GemType.BiggerGem:
                    TeamerBonus.BiggerBonus += extragem.GemBonus;
                    break;
            }
        }
        AddExtraGemBonus();
    }
    private void AddExtraGemBonus()
    {
        PlayerCollider.radius = PlayerData.Player.Distance * (1 + TeamerBonus.ShootBonus);
        WeaponBox.GetComponent<Weapon>().AttackPower = PlayerData.Player.BasePower * (1 + TeamerBonus.DamageBonus);
        WeaponBox.GetComponent<Weapon>().AttackSpeedTime = PlayerData.Player.BaseAttackSpeedTime - TeamerBonus.SpeedBonus;
        WeaponBox.GetComponent<Weapon>().BulletLarge = PlayerData.Player.BaseBulletLarge + TeamerBonus.BiggerBonus;
    }
}
