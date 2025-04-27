using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossCanvs : MonoBehaviour
{
    public Image BossHealth;
    private float HealthLevel;
    private void Update()
    {
        UpdateHealth();
    }
    private void UpdateHealth()
    {
        HealthLevel = GameManager.Instance.BossStats.CharacterData_Temp.NowHealth / GameManager.Instance.BossStats.CharacterData_Temp.MaxHealth;
        BossHealth.gameObject.transform.localScale = new Vector3(HealthLevel, 1, 1);
        if(GameManager.Instance.BossStats.CharacterData_Temp.NowHealth <= 0)
        {
            BossHealth.gameObject.transform.localScale = new Vector3(0, 1, 1);
        }
    }
}
