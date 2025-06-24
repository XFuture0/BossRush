using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster13 : MonoBehaviour
{
    private CharacterStats ThisStats;
    private Rigidbody2D rb;
    public LayerMask Ground;
    [Header("转弯位置")]
    public Vector2 PositionLeft;
    public Vector2 PositionRight;
    public Vector2 PositionLeftCenter;
    public Vector2 PositionRightCenter;
    [Header("自动跳跃计时器")]
    private Vector3 LastPosition = Vector3.zero;
    public float AutoUpTime;
    private float AutoUpTime_Count;
    public float AutoUpSpeed;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ThisStats = GetComponent<CharacterStats>();
    }
    private void Start()
    {
        LeftWalk();
    }
    private void FixedUpdate()
    {
        if (Physics2D.OverlapCircle((Vector2)transform.position + PositionLeftCenter, 0.1f, Ground))
        {
            RightWalk();
        }
        if (Physics2D.OverlapCircle((Vector2)transform.position + PositionRightCenter, 0.1f, Ground))
        {
            LeftWalk();
        }
        if (!Physics2D.OverlapCircle((Vector2)transform.position + PositionLeft, 0.1f, Ground))
        {
            RightWalk();
        }
        if (!Physics2D.OverlapCircle((Vector2)transform.position + PositionRight, 0.1f, Ground))
        {
            LeftWalk();
        }
    }
    private void Update()
    {
        AutoUp();
    }
    private void LeftWalk()
    {
        rb.velocity = new Vector2(-ThisStats.CharacterData_Temp.Speed, 0);
    }
    private void RightWalk()
    {
        rb.velocity = new Vector2(ThisStats.CharacterData_Temp.Speed, 0);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere((Vector2)transform.position + PositionLeft, 0.1f);
        Gizmos.DrawSphere((Vector2)transform.position + PositionRight, 0.1f);
        Gizmos.DrawSphere((Vector2)transform.position + PositionLeftCenter, 0.1f);
        Gizmos.DrawSphere((Vector2)transform.position + PositionRightCenter, 0.1f);
    }
    private void AutoUp()
    {
        if (LastPosition != transform.position)
        {
            AutoUpTime_Count = AutoUpTime;
        }
        if (AutoUpTime_Count > -2)
        {
            AutoUpTime_Count -= Time.deltaTime;
        }
        if (AutoUpTime_Count < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + AutoUpSpeed);
        }
        LastPosition = transform.position;
    }
}
