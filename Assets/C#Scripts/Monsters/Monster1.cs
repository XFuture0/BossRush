using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Monster1 : MonoBehaviour
{
    private Rigidbody2D rb;
    public float JumpHigh;
    public float JumpDistance;
    [Header("ÌøÔ¾¼ÆÊ±Æ÷")]
    public float JumpTime;
    private float JumpTimeCount;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        JumpTimeCount = JumpTime;
    }
    private void Update()
    {
        if(JumpTimeCount >= -2)
        {
            JumpTimeCount -= Time.deltaTime;
        }
        if(JumpTimeCount < 0)
        {
            JumpTimeCount = JumpTime;
            Jump();
        }
    }
    private void Jump()
    {
        var jumpro = UnityEngine.Random.Range(0f, 1f);
        if(jumpro < 0.5f)
        {
            rb.AddForce(new Vector2(JumpDistance, JumpHigh), ForceMode2D.Impulse);
        }
        else if(jumpro >= 0.5f)
        {
            rb.AddForce(new Vector2(-JumpDistance, JumpHigh), ForceMode2D.Impulse);
        }
    }
}
