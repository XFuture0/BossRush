using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class UseCardManager : SingleTons<UseCardManager>
{
    private CharacterData Player;
    private CharacterData Boss;
    public void StartInvoke(string CardName)
    {
        Invoke(CardName, 0);
    }
    private void EqualCompetition()
    {
        GameManager.Instance.Player().MaxHealth += 3;
        GameManager.Instance.Player().NowHealth += 3;
        GameManager.Instance.Boss().MaxHealth *= 1.2f;
        CancelInvoke();
    }
    private void EqualHatred()
    {
        GameManager.Instance.Player().WeaponAttackPower *= 1.5f;
        GameManager.Instance.Boss().WeaponAttackPower += 1;
        CancelInvoke();
    }
    private void EqualReborn()
    {
        GameManager.Instance.Player().AutoHealCount += 1;
        GameManager.Instance.Player().AutoHealTime = 15f;
        GameManager.Instance.Boss().AutoHealCount += 1;
        GameManager.Instance.Boss().AutoHealTime = 0.5f;
        CancelInvoke();
    }
    private void EmergencyAvoidance()
    {
        GameManager.Instance.Player().DodgeRate += 0.05f;
        GameManager.Instance.Boss().DodgeRate += 0.05f;
        CancelInvoke();
    }
    private void RapidGrowth()
    {
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
        CancelInvoke();
    }
    private void SmallSprayStartUp()
    {
        GameManager.Instance.Player().SpeedRate += 0.05f;
        CancelInvoke();
    }
    private void EmergencyResponse()
    {
        GameManager.Instance.Player().CanDash = true;
        GameManager.Instance.Player().DashCount = 1;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
    }
    private void RabbitLegs()
    {
        GameManager.Instance.Player().JumpCount = 2;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
    }
    private void VoidWalkAlone()
    {
        GameManager.Instance.Player().DashInvincibleFrame = true;
        GameManager.Instance.Boss().DodgeRate += 0.1f;
    }
}
