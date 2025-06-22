using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossCanvs : MonoBehaviour
{
    public Image BossHealth;
    private float HealthLevel = 1;
    private void Update()
    {
        UpdateHealth();
        SetDeleteHealth();
    }
    private void UpdateHealth()
    {
        if(GameManager.Instance.Boss().NowHealth <= 0)
        {
            BossHealth.gameObject.transform.localScale = new Vector3(0, 1, 1);
            gameObject.SetActive(false);
        }
        if(GameManager.Instance.Boss().NowHealth == GameManager.Instance.Boss().MaxHealth)
        {
            BossHealth.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void SetDeleteHealth()//记得传其他类型的颜色
    {
        var BaseHealthLevel = HealthLevel;
        HealthLevel = GameManager.Instance.BossStats.CharacterData_Temp.NowHealth / GameManager.Instance.BossStats.CharacterData_Temp.MaxHealth;
        BossHealth.gameObject.transform.localScale = new Vector3(HealthLevel, 1, 1);
    }
}
