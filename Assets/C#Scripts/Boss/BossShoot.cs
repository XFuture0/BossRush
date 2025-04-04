using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShoot : MonoBehaviour
{
    private Rigidbody2D rb;
    public float Speed;
    public Transform Boss;
    private Vector2 Direction;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        Direction = (Vector2)(transform.position - Boss.position).normalized;
        Invoke("OnDestroying", 1.5f);
    }
    private void FixedUpdate()
    {
        rb.velocity = Direction * Speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log(1);
        }
    }
    private void OnDestroying()
    {
        Destroy(gameObject);
    }
}
