using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    public CharacterStats Target;
    public int Thunder_Count;
    [Header("±©À×»÷ÆÆ¼ÆÊ±Æ÷")]
    private bool IsThunderstormDamage;
    private bool IsThunderTiming;
    private float ThunderTime_Count = -2;
    private void Update()
    {
        if (ThunderTime_Count >= -1)
        {
            ThunderTime_Count -= Time.deltaTime;
        }
        if(ThunderTime_Count < 0 && IsThunderstormDamage)
        {
            GameManager.Instance.Player().CriticalDamageRate -= 0.2f;
            IsThunderstormDamage = false;
        }
        if (ThunderTime_Count < 0 && IsThunderTiming)
        {
            GameManager.Instance.Player().AttackRate += 0.15f;
            IsThunderTiming = false;
        }
    }
    public void SetThunder(CharacterStats Attacker)
    {
        StartCoroutine(OnThunder(Attacker));
    }
    private IEnumerator OnThunder(CharacterStats Attacker)
    {
        var ThunderCount = UnityEngine.Random.Range(0f, 1f);
        if (GameManager.Instance.Player().ConductiveWound && gameObject.GetComponent<Vulnerability>().VulnerabilityCount > 0)
        {
            ThunderCount -= 0.1f;
        }
        if (Thunder_Count >= 5)
        {
            ThunderCount = 0;
            Thunder_Count = 0;
        }
        if (ThunderCount < Attacker.CharacterData_Temp.ThunderRate)
        {
            if (Attacker.CharacterData_Temp.ThunderstormDamage)
            {
                ThunderTime_Count = 2;
                if (!IsThunderstormDamage)
                {
                    IsThunderstormDamage = true;
                    Attacker.CharacterData_Temp.CriticalDamageRate += 0.2f;
                }
            }
            if (Attacker.CharacterData_Temp.ThunderTiming)
            {
                ThunderTime_Count = 2;
                if (!IsThunderTiming)
                {
                    IsThunderTiming = true;
                    Attacker.CharacterData_Temp.AttackRate -= 0.15f;
                }
            }
            Target.CharacterData_Temp.NowHealth -= Attacker.CharacterData_Temp.WeaponAttackPower * 0.5f * Attacker.CharacterData_Temp.ThunderBonus;
            Attacker.CharacterData_Temp.AngerValue += 0.02f;
            if (Attacker.CharacterData_Temp.ThunderstormEmblem)
            {
                yield return new WaitForSeconds(0.2f);
                Target.CharacterData_Temp.NowHealth -= Attacker.CharacterData_Temp.WeaponAttackPower * 0.5f * Attacker.CharacterData_Temp.ThunderBonus;
                Attacker.CharacterData_Temp.AngerValue += 0.02f;
            }
        }
    }
}
