using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class ColorManager : SingleTons<ColorManager>
{
    public GameObject Camera;
    public GameObject TransmissionCamera;
    public GameObject Player;
    public GameObject Boss;
    public GameObject BossHealthBox;
    public GameObject HurtText;
    public ColorData ColorData;
    public GameObject ColorCanvs;
    public ColorStats ColorStats;
    private int ColorIndex;
    private string ColorStatsName = "";
    private void Update()
    {
        /*
        Camera.GetComponent<Camera>().backgroundColor = ColorData.ColorLists[ColorIndex].Color1;
        TransmissionCamera.GetComponent<Camera>().backgroundColor = ColorData.ColorLists[ColorIndex].Color1;
        Player.GetComponent<SpriteRenderer>().color = ColorData.ColorLists[ColorIndex].Color2;
        Boss.GetComponent<SpriteRenderer>().color = ColorData.ColorLists[ColorIndex].Color2;
        BossHealthBox.GetComponent<Image>().color = ColorData.ColorLists[ColorIndex].Color2;
        BossHealthBox.transform.GetChild(0).GetComponent<Image>().color = ColorData.ColorLists[ColorIndex].Color2;
        BossHealthBox.transform.GetChild(1).GetComponent<Image>().color = ColorData.ColorLists[ColorIndex].Color2;
        */
        //测试用可在游戏界面改颜色
    }
   
    public void ChangeColor()
    {
        CancelColorStats();
        ColorIndex = UnityEngine.Random.Range(0,ColorData.ColorLists.Count);
        GameManager.Instance.PlayerData.CurrentColor = ColorData.ColorLists[ColorIndex];
        //ColorIndex = 0;//锁定颜色
        Camera.GetComponent<Camera>().backgroundColor = ColorData.ColorLists[ColorIndex].Color1;
        TransmissionCamera.GetComponent<Camera>().backgroundColor = ColorData.ColorLists[ColorIndex].Color1;
        Player.GetComponent<SpriteRenderer>().color = ColorData.ColorLists[ColorIndex].Color2;
        Boss.GetComponent<SpriteRenderer>().color = ColorData.ColorLists[ColorIndex].Color2;
        BossHealthBox.GetComponent<Image>().color = ColorData.ColorLists[ColorIndex].Color2;
        BossHealthBox.transform.GetChild(0).GetComponent<Image>().color = ColorData.ColorLists[ColorIndex].Color2;
        BossHealthBox.transform.GetChild(1).GetComponent<Image>().color = ColorData.ColorLists[ColorIndex].Color2;
    }
    public Color UpdateColor(int Index)
    {
        switch (Index)
        {
            case 1:
                return ColorData.ColorLists[ColorIndex].Color1;
            case 2:
                return ColorData.ColorLists[ColorIndex].Color2;
            default:
                return ColorData.ColorLists[ColorIndex].Color1;
        }
    }
    public void SetColorData(ColorData.Colordata colordata)
    {
        ColorIndex = colordata.Index;
        Camera.GetComponent<Camera>().backgroundColor = ColorData.ColorLists[ColorIndex].Color1;
        TransmissionCamera.GetComponent<Camera>().backgroundColor = ColorData.ColorLists[ColorIndex].Color1;
        Player.GetComponent<SpriteRenderer>().color = ColorData.ColorLists[ColorIndex].Color2;
        Boss.GetComponent<SpriteRenderer>().color = ColorData.ColorLists[ColorIndex].Color2;
        BossHealthBox.GetComponent<Image>().color = ColorData.ColorLists[ColorIndex].Color2;
        BossHealthBox.transform.GetChild(0).GetComponent<Image>().color = ColorData.ColorLists[ColorIndex].Color2;
        BossHealthBox.transform.GetChild(1).GetComponent<Image>().color = ColorData.ColorLists[ColorIndex].Color2;
    }
    public void SetColorText()
    {
        StartCoroutine(OnSetColor());
    }
    private IEnumerator OnSetColor()
    {
        ColorCanvs.SetActive(true);
        ColorCanvs.GetComponent<ColorCanvs>().SetColorText(ColorData.ColorLists[ColorIndex].Name, ColorData.ColorLists[ColorIndex].Description, ColorData.ColorLists[ColorIndex].UseDescription);
        if(ColorData.ColorLists[ColorIndex].InvokeName != "")
        {
            Invoke(ColorData.ColorLists[ColorIndex].InvokeName, 0);
        }
        yield return new WaitForSeconds(2f);
        ColorCanvs.SetActive(false);
    }
    public void CancelColorStats()
    {
        if(ColorStatsName != "")
        {
            ColorStats.Invoke("Cancel" + ColorStatsName, 0);
            ColorStatsName = "";
        }
    }
    //颜色效果
    private void Glass()
    {
        ColorStats.Glass();
    }
    private void IceBlock()
    {
        ColorStats.IceBlock();
        ColorStatsName = "IceBlock";
    }
    private void Taro()
    {
        Debug.Log("taro");
    }
    private void CaramelPudding()
    {
        ColorStats.CaramelPudding();
        ColorStatsName = "CaramelPudding";
    }
    private void AcidicLemon()
    {
        ColorStats.AcidicLemon();
        ColorStatsName = "AcidicLemon";
    }
    private void Cherry()
    {
        ColorStats.Cherry();
    }
}
