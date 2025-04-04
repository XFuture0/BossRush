using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : MonoBehaviour
{
    private Rigidbody2D rb;
    public float Speed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        Invoke("OnDestroying", 4f);
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(-transform.localScale.x * Speed * Time.deltaTime, 0);
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
