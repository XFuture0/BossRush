using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStrike : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        anim.SetTrigger("Strike");
    }
    private void OnDisable()
    {
        anim.SetTrigger("StrikeEnd");
    }
}
