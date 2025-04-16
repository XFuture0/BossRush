using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseHatManager : SingleTons<UseHatManager>
{
    [Header("使用帽子事件监听")]
    public VoidEventSO TopHat;
    private void OnEnable()
    {
        TopHat.OnEventRaised += OnTopHat;
    }
    private void OnDisable()
    {
        TopHat.OnEventRaised -= OnTopHat;
    }
    private void OnTopHat()
    {
        GameManager.Instance.PlayerStats.CharacterData_Temp.MaxHealth += 2;
        GameManager.Instance.Player().NowHealth = GameManager.Instance.Player().MaxHealth;
    }
}
