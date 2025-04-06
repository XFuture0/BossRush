using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyTrident : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 NormalPosition;
    public float Speed;
    public bool IsDown;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        rb.gravityScale = 0f;
        NormalPosition = transform.position;
    }
    private void FixedUpdate()
    {
        if (IsDown)
        {
            rb.gravityScale = 1f;
            rb.velocity = new Vector2(0, Speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameManager.Instance.Attack(GameManager.Instance.BossStats, other.GetComponent<CharacterStats>());
        }
    }
    private void OnDisable()
    {
        IsDown = false;
        transform.position = NormalPosition;
    }
}
