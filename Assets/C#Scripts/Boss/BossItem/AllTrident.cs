using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllTrident : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        Invoke("StartAttack",0.75f);
    }
    private void StartAttack()
    {
        anim.SetBool("Attack", true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameManager.Instance.Attack(GameManager.Instance.BossStats, GameManager.Instance.PlayerStats);
        }
    }
    private void OnDisable()
    {
        anim.SetBool("Attack", false);
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
