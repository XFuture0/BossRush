using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossCanvs : MonoBehaviour
{
    public Image BossHealth;
    public GameObject DeleteHealth;
    public Transform DeleteHealthTemp;
    public Transform DeleteHealthBox;
    private float HealthLevel = 1;
    private void Update()
    {
        UpdateHealth();
    }
    private void UpdateHealth()
    {
        if(GameManager.Instance.BossStats.CharacterData_Temp.NowHealth <= 0)
        {
            BossHealth.gameObject.transform.localScale = new Vector3(0, 1, 1);
        }
    }
    public void SetDeleteHealth(Color color)//记得传其他类型的颜色
    {
        var BaseHealthLevel = HealthLevel;
        HealthLevel = GameManager.Instance.BossStats.CharacterData_Temp.NowHealth / GameManager.Instance.BossStats.CharacterData_Temp.MaxHealth;
        BossHealth.gameObject.transform.localScale = new Vector3(HealthLevel, 1, 1);
        if (BaseHealthLevel - HealthLevel > 0 && HealthLevel > 0)
        {
            var NewDeleteHealth = Instantiate(DeleteHealth, DeleteHealthTemp);
            NewDeleteHealth.GetComponent<RectTransform>().localScale = new Vector3((BaseHealthLevel - HealthLevel) / BossHealth.gameObject.transform.localScale.x, 1, 1);
            NewDeleteHealth.GetComponent<Image>().color = color;
        }
    }
}
