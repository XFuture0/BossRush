using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameManager.Instance.PlayerData.CoinCount++;
            ScoreManager.Instance.AddScore(10,"»ñµÃ½ð±Ò");
            Destroy(gameObject);
        }
    }
}
