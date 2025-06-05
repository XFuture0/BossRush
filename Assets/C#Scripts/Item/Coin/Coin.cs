using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public ItemList.Item Thisitem;
    private void OnEnable()
    {
       MapManager.Instance.SaveItemEvent.OnEventRaised += OnSaveItem;
    }
    private void Update()
    {
        Thisitem.ItemPosition = transform.position;
    }
    private void OnSaveItem()
    {
        MapManager.Instance.AddItemList(Thisitem);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameManager.Instance.PlayerData.CoinCount++;
            ScoreManager.Instance.AddScore(10,"»ñµÃ½ð±Ò");
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        MapManager.Instance.SaveItemEvent.OnEventRaised -= OnSaveItem;
        MapManager.Instance.DeleteItemList(Thisitem);
    }
}
