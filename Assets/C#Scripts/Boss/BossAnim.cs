using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnim : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void OnJumpUp()
    {
        anim.SetTrigger("JumpUp");
    }
    public void OnJumpDown()
    {
        anim.SetTrigger("JumpDown");
    }
    public void OnJumpEnd()
    {
        anim.SetTrigger("JumpEnd");
    }
    public void OnJumpOnGround()
    {
        anim.SetTrigger("JumpOnGround");
    }
    public void OnBossDead()
    {
        anim.SetTrigger("Dead");
    }
}
