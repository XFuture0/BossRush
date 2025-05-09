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
        anim.SetBool("Attack", KeyBoardManager.Instance.GetKey_Mouse0());
    }
    private void PlayerRun()
    {
        anim.SetFloat("Speed", math.abs(rb.velocity.x));
    }
    private IEnumerator AttackTimer()
    {
       yield return new WaitForSeconds(0.2f);
       KeyBoardManager.Instance.StopMoveKey = false;
    }
}
