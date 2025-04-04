using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerCheck Check;
    private float InputX;
    public PlayerData PlayerData;
    [Header("��ʱ����")]
    private float Speed;
    private float JumpForce;
    private float JumpDownSpeed_Max;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Check = rb.GetComponent<PlayerCheck>();
    }
    private void OnEnable()
    {
        RefreshData();
    }
    private void Update()
    {
        Jump();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        InputX = KeyBoardManager.Instance.GetHorizontalRaw();
        rb.velocity = new Vector2(InputX * Speed * Time.deltaTime, rb.velocity.y);
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void Jump()
    {
        if (KeyBoardManager.Instance.GetKeyDown_Space() && Check.IsGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }
        if (!Check.IsGround && rb.velocity.y < 0)
        {
            if (rb.velocity.y <= JumpDownSpeed_Max)
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpDownSpeed_Max);
            }
        }
    }
    private void RefreshData()
    {
        Speed = PlayerData.Speed;
        JumpForce = PlayerData.JumpForce;
        JumpDownSpeed_Max = PlayerData.JumpDownSpeed_Max;
    }
}
