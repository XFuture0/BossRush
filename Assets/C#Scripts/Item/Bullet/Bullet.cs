using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public Vector2 BulletRotation;
    public float BulletSpeed;
    private bool IsHit;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        if (!IsHit)
        {
            BulletFly();
        }
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
            if (!GameManager.Instance.Player().ElectricBullets)
            {
                IsHit = true;
                rb.velocity = Vector2.zero;
                anim.SetTrigger("Hit");
            }
            GameManager.Instance.Attack(GameManager.Instance.PlayerStats,GameManager.Instance.BossStats);
        }
        if(other.tag == "BossArmy")
        {
            if (!GameManager.Instance.Player().ElectricBullets)
            {
                IsHit = true;
                rb.velocity = Vector2.zero;
                anim.SetTrigger("Hit");
            }
            GameManager.Instance.Attack(GameManager.Instance.PlayerStats,other.GetComponent<CharacterStats>());
        }
        if(other.tag == "Ground")
        {
            IsHit = true;
            rb.velocity = Vector2.zero;
            anim.SetTrigger("Hit");
        }
    }
    private void Destorying()
    {
        Destroy(gameObject);
    }
}
