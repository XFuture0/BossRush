using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        PlayerRun();
        PlayerAttack();
    }
    private void PlayerRun()
    {
        anim.SetFloat("Speed", math.abs(rb.velocity.x));
    }
    private void PlayerAttack()
    {
        if (KeyBoardManager.Instance.GetKeyDown_J())
        {
            anim.SetTrigger("Attack");
            KeyBoardManager.Instance.StopMoveKey = true;
            StartCoroutine(AttackTimer());
        }
    }
    private IEnumerator AttackTimer()
    {
       yield return new WaitForSeconds(0.2f);
       KeyBoardManager.Instance.StopMoveKey = false;
    }
}
