using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArmyAnim : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    public void OnAttack()
    {
        anim.SetTrigger("Attack");
    }
    public void OnAttackEnd()
    {
        anim.SetTrigger("AttackEnd");
    }
    public void OnEnd()
    {
        anim.SetTrigger("End");
    }
}
