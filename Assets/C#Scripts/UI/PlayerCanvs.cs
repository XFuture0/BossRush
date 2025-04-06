using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerCanvs : MonoBehaviour
{
    public Text HealthCount;
    private void Update()
    {
        RefreshHealthCount();
    }
    private void RefreshHealthCount()
    {
        HealthCount.text = "X " + GameManager.Instance.PlayerStats.CharacterData_Temp.NowHealth.ToString();
    }
}
