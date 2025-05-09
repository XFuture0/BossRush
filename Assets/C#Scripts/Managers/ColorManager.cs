using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class ColorManager : SingleTons<ColorManager>
{
    public GameObject Camera;
    public GameObject Player;
    public GameObject Boss;
    public GameObject BossHealthBox;
    public GameObject Back;
    public GameObject HurtText;
    public ColorData ColorData;
    private int ColorIndex;
    private void Update()
    {
      /*  Camera.GetComponent<Camera>().backgroundColor = ColorData.ColorLists[ColorIndex].Color1;
        Player.GetComponent<SpriteRenderer>().color = ColorData.ColorLists[ColorIndex].Color2;
        Weapon.GetComponent<SpriteRenderer>().color = ColorData.ColorLists[ColorIndex].Color2;
        Boss.GetComponent<SpriteRenderer>().color = ColorData.ColorLists[ColorIndex].Color2;
        BossHealthBox.GetComponent<Image>().color = ColorData.ColorLists[ColorIndex].Color2;
        BossHealthBox.transform.GetChild(0).GetComponent<Image>().color = ColorData.ColorLists[ColorIndex].Color2;
        BossHealthBox.transform.GetChild(1).GetComponent<Image>().color = ColorData.ColorLists[ColorIndex].Color2;
        Back.GetComponent<Tilemap>().color = ColorData.ColorLists[ColorIndex].Color2;
      */
      //测试用可在游戏界面改颜色
    }
   
    public void ChangeColor()
    {
        ColorIndex = UnityEngine.Random.Range(0,ColorData.ColorLists.Count);
        //ColorIndex = ColorData.ColorLists.Count - 1;//锁定到最后的颜色
        Camera.GetComponent<Camera>().backgroundColor = ColorData.ColorLists[ColorIndex].Color1;
        Player.GetComponent<SpriteRenderer>().color = ColorData.ColorLists[ColorIndex].Color2;
        Boss.GetComponent<SpriteRenderer>().color = ColorData.ColorLists[ColorIndex].Color2;
        BossHealthBox.GetComponent<Image>().color = ColorData.ColorLists[ColorIndex].Color2;
        BossHealthBox.transform.GetChild(0).GetComponent<Image>().color = ColorData.ColorLists[ColorIndex].Color2;
        BossHealthBox.transform.GetChild(1).GetComponent<Image>().color = ColorData.ColorLists[ColorIndex].Color2;
        Back.GetComponent<Tilemap>().color = ColorData.ColorLists[ColorIndex].Color2;
        HurtText.GetComponent<Text>().color = new Color(ColorData.ColorLists[ColorIndex].Color2.r, ColorData.ColorLists[ColorIndex].Color2.g, ColorData.ColorLists[ColorIndex].Color2.b, HurtText.GetComponent<Text>().color.a);
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
}
