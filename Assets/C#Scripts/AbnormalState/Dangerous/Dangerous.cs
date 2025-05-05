using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dangerous : MonoBehaviour
{
    public CharacterStats Target;
    public int CurrentDangerousCount = 0;
    private float DangerousNuclearBombBonus = 1;
    public void SetDangerous(CharacterStats Attacker)
    {
        StartCoroutine(OnDangerous(Attacker));
    }
    private IEnumerator OnDangerous(CharacterStats Attacker)
    {
        float DeleteHeart = 0;
        if (Attacker.CharacterData_Temp.DangerousLife)
        {
            DeleteHeart = Attacker.CharacterData_Temp.MaxHealth - Attacker.CharacterData_Temp.NowHealth;
        }
        CurrentDangerousCount++;
        if (CurrentDangerousCount >= Attacker.CharacterData_Temp.DangerousBulletCount - DeleteHeart)
        {
            if (Attacker.CharacterData_Temp.DangerousNuclearBomb)
            {
                var BombRate = UnityEngine.Random.Range(0f, 1f);
                if(BombRate < 0.01f)
                {
                    DangerousNuclearBombBonus = 10;
                }
            }
            Target.CharacterData_Temp.NowHealth -= Attacker.CharacterData_Temp.WeaponAttackPower * Attacker.CharacterData_Temp.AttackBonus * Attacker.CharacterData_Temp.DangerousBulletBonus * DangerousNuclearBombBonus;
            CurrentDangerousCount = 0;
            if (Attacker.CharacterData_Temp.DeepDangerous)
            {
                yield return new WaitForSeconds(0.2f);
                Target.CharacterData_Temp.NowHealth -= Attacker.CharacterData_Temp.WeaponAttackPower * Attacker.CharacterData_Temp.AttackBonus * Attacker.CharacterData_Temp.DangerousBulletBonus * DangerousNuclearBombBonus;
            }
            if (Attacker.CharacterData_Temp.DangerAnger)
            {
                Attacker.CharacterData_Temp.AngerValue += 0.1f;
            }
            DangerousNuclearBombBonus = 1;
        }
    }
}
