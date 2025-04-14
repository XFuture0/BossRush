using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [Header("�㲥")]
    public GameObjectEventSO ReturnPoolEvent;
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
        if (spriteRenderer.color != ColorManager.Instance.bulletColor)
        {
            spriteRenderer.color = ColorManager.Instance.bulletColor;
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
    private void ReturnPool()
    {
        ReturnPoolEvent.RaiseGameObjectEvent(this.gameObject);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Boss")
        {
            GameManager.Instance.Attack(GameManager.Instance.BossStats,(int)GameManager.Instance.PlayerStats.CharacterData_Temp.AttackPower);
            ReturnPool();
        }
        if(other.tag == "Ground")
        {
            ReturnPool();
        }
    }
}
