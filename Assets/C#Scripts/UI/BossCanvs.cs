using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCanvs : MonoBehaviour
{
    public GameObject BossHealth;
    public GameObject BossHealthBox;
    private int HealthCount;
    private List<BossHealth> BossHealths = new List<BossHealth>();
    private void Start()
    {
        RefreshHealth();
    }
    public void RefreshHealth()
    {
        HealthCount = (int)GameManager.Instance.BossStats.CharacterData_Temp.NowHealth;
        for (int i = 0; i < HealthCount; i++)
        {
            var NewHealth = Instantiate(BossHealth,BossHealthBox.transform.position + new Vector3(-75 * i ,0,0), Quaternion.identity,BossHealthBox.transform);
            BossHealths.Add(NewHealth.GetComponent<BossHealth>());
        }
    }
    public void ClearBossHealth()
    {
        for(int i = 0;i < BossHealthBox.transform.childCount; i++)
        {
            BossHealths.Clear();
            Destroy(BossHealthBox.transform.GetChild(i).gameObject);
        }
    }
    private void Update()
    {
        UpdateHealth();
    }
    private void UpdateHealth()
    {
        HealthCount = (int)GameManager.Instance.BossStats.CharacterData_Temp.NowHealth;
        for (int i = 0; i < BossHealths.Count; i++)
        {
            if(HealthCount <= i)
            {
                BossHealths[i].ChangeImage();
            }
        }
    }
}
