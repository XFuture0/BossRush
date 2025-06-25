using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerTeamer : MonoBehaviour
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
    public CircleCollider2D TeamerCollider;
    public GameObject Player;
    public GameObject WeaponBox;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        TeamerAnim();
       if (Player.transform.localScale.x == 1)
       {
            WeaponBox.transform.localScale = new Vector3(1, 1, 1);
       }
       else if(Player.transform.localScale.x == -1)
       {
            WeaponBox.transform.localScale = new Vector3(-1, 1, 1);
       }
    }
    private void TeamerAnim()
    {
        anim.SetBool("Attack", KeyBoardManager.Instance.GetKey_Mouse0());
        anim.SetFloat("Speed", math.abs(Player.GetComponent<Rigidbody2D>().velocity.x));
    }
    public void RefreshSlimeData(SlimeData slimeData)
    {
        anim.runtimeAnimatorController = slimeData.SlimeAnim;
        WeaponBox.GetComponent<Weapon>().SlimeData = slimeData;
    }
    public void CheckExtraGemBonus(ExtraGemData extraGemData,SlimeData ThisSlime)
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
        AddExtraGemBonus(ThisSlime);
    }
    private void AddExtraGemBonus(SlimeData ThisSlime)
    {
        TeamerCollider.radius = ThisSlime.Distance * (1 + TeamerBonus.ShootBonus);
        WeaponBox.GetComponent<Weapon>().AttackPower = ThisSlime.BasePower * (1 + TeamerBonus.DamageBonus);
        WeaponBox.GetComponent<Weapon>().AttackSpeedTime = ThisSlime.BaseAttackSpeedTime - TeamerBonus.SpeedBonus;
        WeaponBox.GetComponent<Weapon>().BulletLarge = ThisSlime.BaseBulletLarge + TeamerBonus.BiggerBonus;
    }
}
