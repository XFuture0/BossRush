using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UseHatManager : SingleTons<UseHatManager>
{
    public void StartInvoke(string HatName)
    {
        Invoke(HatName, 0);
    }
    private void TopHat()
    {
        GameManager.Instance.PlayerStats.CharacterData_Temp.MaxHealth += 2;
        GameManager.Instance.Player().NowHealth = GameManager.Instance.Player().MaxHealth;
        CancelInvoke();
    }
}
