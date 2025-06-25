using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemItem : MonoBehaviour
{
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        var CoinSpeedX = UnityEngine.Random.Range(-10, 10f);
        var CoinSpeedY = UnityEngine.Random.Range(0, 12.5f);
        rb.velocity = new Vector2(CoinSpeedX, CoinSpeedY);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.PlayerData.FreeWeaponSlotCount++;
            Destroy(gameObject);
        }
    }
}
