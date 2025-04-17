using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArmy : MonoBehaviour
{
    private CharacterStats bossarmy;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector3 PlayerPosition;
    private Vector2 PlayerRotation;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bossarmy = GetComponent<CharacterStats>();
    }
    private void Update()
    {
        PlayerPosition = GameManager.Instance.PlayerStats.gameObject.transform.position;
        BossArmyDead();
    }
    private void FixedUpdate()
    {
        if(spriteRenderer.color != ColorManager.Instance.UpdateColor(2))
        {
            spriteRenderer.color = ColorManager.Instance.UpdateColor(2);
        }
        TrackPlayer();
        ChangeFace();
    }
    private void TrackPlayer()
    {
        PlayerRotation = ((Vector2)PlayerPosition - (Vector2)transform.position).normalized;
        rb.velocity = PlayerRotation * bossarmy.CharacterData_Temp.Speed * Time.deltaTime;
    }
    private void ChangeFace()
    {
        if(PlayerPosition.x - transform.position.x >= 0)//”“
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if(PlayerPosition.x - transform.position.x < 0)//◊Û
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameManager.Instance.Attack(bossarmy,GameManager.Instance.PlayerStats);
        }
    }
    private void BossArmyDead()
    {
        if(bossarmy.CharacterData_Temp.NowHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
