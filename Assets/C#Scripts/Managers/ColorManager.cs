using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class ColorManager : SingleTons<ColorManager>
{
    public Color DoorColor;
    public GameObject Camera;
    public GameObject Player;
    public GameObject Weapon;
    public Color PlayerHeart;
    public GameObject Boss;
    public GameObject BossHealthBox;
    public GameObject Back;
    public ColorData ColorData;
    public Bullet bullet;
    public Color bulletColor;
    public void ChangeColor()
    {
        var ColorIndex = UnityEngine.Random.Range(0,ColorData.ColorLists.Count);
        DoorColor = ColorData.ColorLists[ColorIndex].Color2;
        Camera.GetComponent<Camera>().backgroundColor = ColorData.ColorLists[ColorIndex].Color1;
        Player.GetComponent<SpriteRenderer>().color = ColorData.ColorLists[ColorIndex].Color2;
        Weapon.GetComponent<SpriteRenderer>().color = ColorData.ColorLists[ColorIndex].Color2;
        PlayerHeart = ColorData.ColorLists[ColorIndex].Color2;
        Boss.GetComponent<SpriteRenderer>().color = ColorData.ColorLists[ColorIndex].Color2;
        BossHealthBox.GetComponent<Image>().color = ColorData.ColorLists[ColorIndex].Color2;
        BossHealthBox.transform.GetChild(0).GetComponent<Image>().color = ColorData.ColorLists[ColorIndex].Color2;
        BossHealthBox.transform.GetChild(1).GetComponent<Image>().color = ColorData.ColorLists[ColorIndex].Color2;
        Back.GetComponent<Tilemap>().color = ColorData.ColorLists[ColorIndex].Color2;
        bullet.GetComponent<SpriteRenderer>().color = ColorData.ColorLists[ColorIndex].Color2;
        bulletColor = ColorData.ColorLists[ColorIndex].Color2;
    }
}
