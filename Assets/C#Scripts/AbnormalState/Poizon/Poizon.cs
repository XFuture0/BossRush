using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Poizon : MonoBehaviour
{
    public CharacterStats Target;
    private int PoizonCount = 0;
    private float AttackRate_PoizonDamage;
    private void Update()
    {
        if (GameManager.Instance.Player().PersistentToxic && GameManager.Instance.Player().AttackRate > 1)
        {
            AttackRate_PoizonDamage = GameManager.Instance.Player().AttackRate;
        }
        else
        {
            AttackRate_PoizonDamage = 1;
        }
        if (GameManager.Instance.Player().PoisonExplosionGel)
        {
            if(PoizonCount >= 10)
            {
                StopAllCoroutines();
                PoizonCount = 0;
                Target.CharacterData_Temp.NowHealth -= GameManager.Instance.Player().PoizonDamage * GameManager.Instance.Player().WeaponAttackPower * 20;
            }
        }
        if (GameManager.Instance.Player().ToxicHeart)
        {
            if (PoizonCount >= 15)
            {
                StopAllCoroutines();
                PoizonCount = 0;
                GameManager.Instance.Player().NowHealth += 1;
            }
        }
    }
    public void SetPosizon(CharacterStats Attacker)
    {
        StartCoroutine(OnPosizon(Attacker));
    }
    private IEnumerator OnPosizon(CharacterStats Attacker)
    {
        float ExtraDamage = 0;
        float VulnerabilityDamage = 0;
        float DangerousBullet_PoizonDamage = 0;
        if(Attacker.CharacterData_Temp.DangerousBullet && Attacker.CharacterData_Temp.UndergroundToxicGel)
        {
            DangerousBullet_PoizonDamage = Attacker.CharacterData_Temp.WeaponAttackPower * Attacker.CharacterData_Temp.DangerousBulletBonus * 0.05f;
        }
        if (Attacker.CharacterData_Temp.ToxicWrathSlime)
        {
            Attacker.CharacterData_Temp.AngerValue += 0.02f;
        }
        if (Attacker.CharacterData_Temp.PoisonExplosionGel)
        {
            PoizonCount++;
        }
        if (GameManager.Instance.Player().ToxinImmersionInjury && gameObject.GetComponent<Vulnerability>().VulnerabilityCount > 0)
        {
            VulnerabilityDamage = GameManager.Instance.Player().WeaponAttackPower * 0.1f;
        }
        for (int i = 0;i < GameManager.Instance.Player().PoizonTime; i++)
        {
            if(ExtraDamage >= Attacker.CharacterData_Temp.WeaponAttackPower * 0.1f)
            {
                ExtraDamage = Attacker.CharacterData_Temp.WeaponAttackPower * 0.1f;
            }
            if(Target.CharacterData_Temp.NowHealth >= 0)
            {
                if (Attacker.CharacterData_Temp.PoisonousStrike)
                {
                    var Critical = UnityEngine.Random.Range(0f, 1f);
                    if (Critical < Attacker.CharacterData_Temp.CriticalDamageRate)
                    {
                        Target.CharacterData_Temp.NowHealth -= Attacker.CharacterData_Temp.PoizonDamage * Attacker.CharacterData_Temp.WeaponAttackPower * 0.2f * 2 * AttackRate_PoizonDamage + ExtraDamage + VulnerabilityDamage + DangerousBullet_PoizonDamage;
                    }
                }
                else
                {
                    Target.CharacterData_Temp.NowHealth -= Attacker.CharacterData_Temp.PoizonDamage * Attacker.CharacterData_Temp.WeaponAttackPower * 0.2f * AttackRate_PoizonDamage + ExtraDamage + VulnerabilityDamage + DangerousBullet_PoizonDamage;
                }
                if (Attacker.CharacterData_Temp.ThreePartsPoisoning)
                {
                    ExtraDamage += Attacker.CharacterData_Temp.WeaponAttackPower * 0.002f;
                }
                yield return new WaitForSeconds(0.2f);
            }
        }
        if (Attacker.CharacterData_Temp.PoisonExplosionGel)
        {
            PoizonCount--;
        }
    }
}
