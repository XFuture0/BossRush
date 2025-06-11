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
    [Header("¾àÀë¼à²â")]
    public Vector2 LeftUpPo;
    public Vector2 RightDownPo;
    public LayerMask Player;
    private float JumpRo;
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
            JumpTimeCount = UnityEngine.Random.Range(JumpTime * 0.7f,JumpTime * 1.3f);
            JumpRo = UnityEngine.Random.Range(0f, 1f);
            if (Physics2D.OverlapArea((Vector2)transform.position + LeftUpPo,(Vector2)transform.position + RightDownPo, Player))
            {
                JumpTimeCount = 0.5f;
                if(SceneChangeManager.Instance.Player.transform.position.x >= transform.position.x)
                {
                    JumpRo = 0.4f;
                }
                if(SceneChangeManager.Instance.Player.transform.position.x < transform.position.x)
                {
                    JumpRo = 0.6f;
                }
            }
            Jump();
        }
    }
    private void Jump()
    {
        if (JumpRo < 0.5f)
        {
            rb.AddForce(new Vector2(JumpDistance, JumpHigh), ForceMode2D.Impulse);
        }
        else if (JumpRo >= 0.5f)
        {
            rb.AddForce(new Vector2(-JumpDistance, JumpHigh), ForceMode2D.Impulse);
        }
    }
}
