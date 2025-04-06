using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArmy : MonoBehaviour
{
    public float Speed;
    private Vector3 PlayerPosition;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        PlayerPosition = GameManager.Instance.PlayerStats.gameObject.transform.position;
    }
    private void FixedUpdate()
    {
        TrackPlayer();
        if(gameObject.GetComponent<CharacterStats>().CharacterData_Temp.NowHealth <= 0)
        {
            Dead();
        }
    }
    private void TrackPlayer()
    {
        if(transform.position.x - PlayerPosition.x >= 0)
        {
            rb.velocity = new Vector2(-Speed * Time.deltaTime, rb.velocity.y);
        }
        else if(transform.position.x - PlayerPosition.x < 0)
        {
            rb.velocity = new Vector2(Speed * Time.deltaTime, rb.velocity.y);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.Attack(gameObject.GetComponent<CharacterStats>(), other.GetComponent<CharacterStats>());
        }
    }
    private void Dead()
    {
        Destroy(gameObject);
    }
}
