using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goods : MonoBehaviour
{
    public int CoinCount;
    public GameObject RKey;
    private bool IsPlayer;
    private void Update()
    {
        if(IsPlayer && KeyBoardManager.Instance.GetKeyDown_R())
        {
            IsPlayer = false;
            BuyGoods();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            IsPlayer = true;
            RKey.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            IsPlayer = false;
            RKey.SetActive(false);
        }
    }
    private void BuyGoods()
    {
        switch (gameObject.name)
        {
            case "Goods1":
                if(GameManager.Instance.PlayerData.CoinCount >= 30)
                {
                    GameManager.Instance.PlayerData.CoinCount -= 30;
                    Instantiate(MapManager.Instance.MapData.Goods1,gameObject.transform.position,Quaternion.identity);
                    MapManager.Instance.MapData.Goods1 = null;
                    Destroy(gameObject);
                }
                break;
            case "Goods2":
                if (GameManager.Instance.PlayerData.CoinCount >= 20)
                {
                    GameManager.Instance.PlayerData.CoinCount -= 20;
                    Instantiate(MapManager.Instance.MapData.Goods2, gameObject.transform.position, Quaternion.identity);
                    MapManager.Instance.MapData.Goods2 = null;
                    Destroy(gameObject);
                }
                break;
            case "Goods3":
                if (GameManager.Instance.PlayerData.CoinCount >= 10)
                {
                    GameManager.Instance.PlayerData.CoinCount -= 10;
                    Instantiate(MapManager.Instance.MapData.Goods3, gameObject.transform.position, Quaternion.identity);
                    MapManager.Instance.MapData.Goods3 = null;
                    Destroy(gameObject);
                }
                break;
            case "Goods4":
                if (GameManager.Instance.PlayerData.CoinCount >= 10)
                {
                    GameManager.Instance.PlayerData.CoinCount -= 10;
                    Instantiate(MapManager.Instance.MapData.Goods4, gameObject.transform.position, Quaternion.identity);
                    MapManager.Instance.MapData.Goods4 = null;
                    Destroy(gameObject);
                }
                break;
        }
        DataManager.Instance.Save(DataManager.Instance.Index);//´æµµ
    }
}
