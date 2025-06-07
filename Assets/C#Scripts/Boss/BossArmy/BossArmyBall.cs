using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArmyBall : MonoBehaviour
{
    private Rigidbody2D rb;
    public float Speed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        var PlayerPosition = SceneChangeManager.Instance.Player.transform.position;
        var PlayerRotation = (PlayerPosition - transform.position).normalized;
        rb.velocity = PlayerRotation * Speed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Ground")
        {
            Destroy(gameObject);
        }
        if(other.tag == "Player")
        {
            GameManager.Instance.Attack(GameManager.Instance.PlayerStats,1);
            Destroy(gameObject);
        }
    }
}
