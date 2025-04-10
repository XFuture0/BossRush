using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.gameObject.tag == "Player")
        {
            GameManager.Instance.AddCoin();
            Destroy(gameObject);
        }
    }
}
