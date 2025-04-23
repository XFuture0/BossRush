using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public Vector2 BulletRotation;
    public float BulletSpeed;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        Invoke("ReturnPool", 3f);
    }
    private void Update()
    {
        if (spriteRenderer.color != ColorManager.Instance.UpdateColor(2))
        {
            spriteRenderer.color = ColorManager.Instance.UpdateColor(2);
        }
    }
    private void FixedUpdate()
    {
        BulletFly();
    }
    private void BulletFly()
    {
        rb.velocity = BulletRotation * BulletSpeed;
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Boss")
        {
            GameManager.Instance.Attack(GameManager.Instance.PlayerStats,GameManager.Instance.BossStats);
            Destroy(gameObject);
        }
        if(other.tag == "BossArmy")
        {
            GameManager.Instance.Attack(GameManager.Instance.PlayerStats,other.GetComponent<CharacterStats>());
            Destroy(gameObject);
        }
        if(other.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
