using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemCanvs : MonoBehaviour
{
    public GameObject CoinImage;
    private int CoinCount = 0;
    private bool IsOpen;
    [Header("¿ªÆô¼ÆÊ±Æ÷")]
    public float OpenTime;
    private float OpenTime_Count;
    private void Update()
    {
        ShowCoinCount();
    }
    private void ShowCoinCount()
    {
        if(CoinCount != GameManager.Instance.PlayerData.CoinCount)
        {
            OpenTime_Count = OpenTime;
            IsOpen = true;
        }
        if (IsOpen)
        {
            CoinImage.SetActive(true);
            OpenTime_Count -= Time.deltaTime;
            CoinImage.transform.GetChild(0).GetComponent<Text>().text = GameManager.Instance.PlayerData.CoinCount.ToString();
            CoinCount = GameManager.Instance.PlayerData.CoinCount;
        }
        if(OpenTime_Count <= 0 && OpenTime_Count > -2f)
        {
            IsOpen = false;
            CoinImage.SetActive(false);
        }
    }
}
