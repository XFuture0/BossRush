using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EasyWater : MonoBehaviour
{
    private bool IsEasyWater;
    private float EasyWaterTime_Count = -2;
    private delegate void EasyWaterLevel();
    private EasyWaterLevel Setwaterlevel;
    private EasyWaterLevel Delwaterlevel;
    private void Update()
    {
        if (EasyWaterTime_Count <= 0 && IsEasyWater)
        {
            Delwaterlevel();
            if (GameManager.Instance.Player().CondensingColdPuncture)
            {
                CancelInvoke("WaterDamage");
            }
            if (GameManager.Instance.Player().VenomCoagulation)
            {
                GameManager.Instance.Player().PoizonDamage -= 0.15f;
            }
            if (GameManager.Instance.Player().ConductiveWaterFlow)
            {
                GameManager.Instance.Player().ThunderRate -= 0.15f;
            }
            IsEasyWater = false;
        }
        else if (EasyWaterTime_Count >= -2 && IsEasyWater)
        {
            EasyWaterTime_Count -= Time.deltaTime;
        }
    }
    public void OnEasyWater(CharacterStats Attacker)
    {
        if (Attacker.CharacterData_Temp.EasyWater)
        {
            EasyWaterTime_Count = Attacker.CharacterData_Temp.EasyWaterTime;
            if (!IsEasyWater)
            {
                IsEasyWater = true;
                if (Attacker.CharacterData_Temp.CondensingColdPuncture)
                {
                    InvokeRepeating("WaterDamage",0,0.1f);
                }
                if (Attacker.CharacterData_Temp.VenomCoagulation)
                {
                    Attacker.CharacterData_Temp.PoizonDamage += 0.15f;
                }
                if (Attacker.CharacterData_Temp.ConductiveWaterFlow)
                {
                    Attacker.CharacterData_Temp.ThunderRate += 0.15f;
                }
                Setwaterlevel();
            }
        }
    }
    public void SetEasyWater(int index)
    {
        switch (index)
        {
            case 1:
                Setwaterlevel = SetEasyWater1;
                Delwaterlevel = DelEasyWater1;
                break;
            case 2:
                Setwaterlevel = SetEasyWater2;
                Delwaterlevel = DelEasyWater2;
                break;
            case 3:
                Setwaterlevel = SetEasyWater3;
                Delwaterlevel = DelEasyWater3;
                break;
        }
    }
    private void WaterDamage()
    {
        gameObject.GetComponent<CharacterStats>().CharacterData_Temp.NowHealth -= 0.1f;
    }
    private void SetEasyWater1()
    {
        GameManager.Instance.Player().CriticalDamageRate += 0.1f;
        GameManager.Instance.Player().DodgeRate += 0.1f;
    }
    private void DelEasyWater1()
    {
        GameManager.Instance.Player().CriticalDamageRate -= 0.1f;
        GameManager.Instance.Player().DodgeRate -= 0.1f;
    }
    private void SetEasyWater2()
    {
        GameManager.Instance.Player().AttackBonus += 0.3f;
        GameManager.Instance.Player().CriticalDamageRate += 0.2f;
        GameManager.Instance.Player().DodgeRate += 0.1f;
    }
    private void DelEasyWater2()
    {
        GameManager.Instance.Player().AttackBonus -= 0.3f;
        GameManager.Instance.Player().CriticalDamageRate -= 0.2f;
        GameManager.Instance.Player().DodgeRate -= 0.1f;
    }
    private void SetEasyWater3()
    {
        GameManager.Instance.Player().AttackBonus += 0.5f;
        GameManager.Instance.Player().CriticalDamageRate += 0.3f;
        GameManager.Instance.Player().CriticalDamageBonus += 0.5f;
        GameManager.Instance.Player().DodgeRate += 0.2f;
    }
    private void DelEasyWater3()
    {
        GameManager.Instance.Player().AttackBonus -= 0.5f;
        GameManager.Instance.Player().CriticalDamageRate -= 0.3f;
        GameManager.Instance.Player().CriticalDamageBonus -= 0.5f;
        GameManager.Instance.Player().DodgeRate -= 0.2f;
    }
}
