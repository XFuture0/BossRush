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
    }
    private void PlayerRun()
    {
        anim.SetFloat("Speed", math.abs(rb.velocity.x));
    }
}
