using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trident : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    public float WaitTime;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        Invoke("StartAttack", WaitTime);
        Invoke("Destorying", 1.5f);
    }
    private void Update()
    {
        if(spriteRenderer.color != ColorManager.Instance.UpdateColor(2))
        {
            spriteRenderer.color = ColorManager.Instance.UpdateColor(2);
        }
    }
    private void StartAttack()
    {
        anim.SetBool("Attack", true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameManager.Instance.Attack(GameManager.Instance.BossStats,GameManager.Instance.PlayerStats);
        }
    }
    private void Destorying()
    {
        Destroy(gameObject);
    }
}
