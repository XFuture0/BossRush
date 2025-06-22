using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDesk : MonoBehaviour
{
    [Header("…Ã∆∑")]
    public GameObject Goods1;
    public GameObject Goods2;
    public GameObject Goods3;
    public GameObject Goods4;
    private void Start()
    {
        RefreshGoods();
    }
    private void RefreshGoods()
    {
        if(MapManager.Instance.MapData.Goods1 != null)
        {
            Goods1.GetComponent<SpriteRenderer>().sprite = MapManager.Instance.MapData.Goods1.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            Goods1.SetActive(false);
        }
        if (MapManager.Instance.MapData.Goods2 != null)
        {
            Goods2.GetComponent<SpriteRenderer>().sprite = MapManager.Instance.MapData.Goods2.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            Goods2.SetActive(false);
        }
        if (MapManager.Instance.MapData.Goods3 != null)
        {
            Goods3.GetComponent<SpriteRenderer>().sprite = MapManager.Instance.MapData.Goods3.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            Goods3.SetActive(false);
        }
        if (MapManager.Instance.MapData.Goods4 != null)
        {
            Goods4.GetComponent<SpriteRenderer>().sprite = MapManager.Instance.MapData.Goods4.GetComponent<SpriteRenderer>().sprite;
        }
        else
        {
            Goods4.SetActive(false);
        }
    }
}
