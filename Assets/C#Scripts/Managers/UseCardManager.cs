using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class UseCardManager : SingleTons<UseCardManager>
{
    private CharacterData Player;
    private CharacterData Boss;
    [Header("使用卡牌事件监听")]
    public VoidEventSO EqualCompetition;
    public VoidEventSO EqualHatred;
    public VoidEventSO EqualReborn;
    public VoidEventSO EmergencyAvoidance;
    public VoidEventSO RapidGrowth;
    public VoidEventSO SmallSprayStartUp;
    private void OnEnable()
    {
        EqualCompetition.OnEventRaised += OnEqualCompetition;
        EqualHatred.OnEventRaised += OnEqualHatred;
        EqualReborn.OnEventRaised += OnEqualReborn;
        EmergencyAvoidance.OnEventRaised += OnEmergencyAvoidance;
        RapidGrowth.OnEventRaised += OnRapidGrowth;
        SmallSprayStartUp.OnEventRaised += OnSmallSprayStartUp;
    }
    private void OnDisable()
    {
        EqualCompetition.OnEventRaised -= OnEqualCompetition;
        EqualHatred.OnEventRaised -= OnEqualHatred;
        EqualReborn.OnEventRaised -= OnEqualReborn;
        EmergencyAvoidance.OnEventRaised -= OnEmergencyAvoidance;
        RapidGrowth.OnEventRaised -= OnRapidGrowth;
        SmallSprayStartUp.OnEventRaised -= OnSmallSprayStartUp;
    }
    private void OnEqualCompetition()
    {
        GameManager.Instance.Player().NowHealth += 3;
        GameManager.Instance.Boss().NowHealth *= 1.2f;
    }
    private void OnEqualHatred()
    {
        GameManager.Instance.Player().WeaponAttackPower += 5;
        GameManager.Instance.Boss().WeaponAttackPower += 1;
    }
    private void OnEqualReborn()
    {
        GameManager.Instance.Player().AutoHealCount += 1;
        GameManager.Instance.Player().AutoHealTime = 15f;
        GameManager.Instance.Boss().AutoHealCount += 1;
        GameManager.Instance.Boss().AutoHealTime = 0.5f;
    }
    private void OnEmergencyAvoidance()
    {
        GameManager.Instance.Player().DodgeRate += 0.05f;
        GameManager.Instance.Boss().DodgeRate += 0.05f;
    }
    private void OnRapidGrowth()
    {
        GameManager.Instance.Player().AttackRate -= 0.05f;
        GameManager.Instance.Boss().AttackRate -= 0.1f;
    }
    private void OnSmallSprayStartUp()
    {
        GameManager.Instance.Player().SpeedRate += 0.05f;
    }
}
