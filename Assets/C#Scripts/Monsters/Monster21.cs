using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster21 : MonoBehaviour
{
    private CharacterStats ThisStats;
    private Rigidbody2D rb;
    public LayerMask Ground;
    public GameObject DownBall;
    public float BallSpeed;
    [Header("转弯位置")]
    public Vector2 PositionLeft;
    public Vector2 PositionRight;
    public Vector2 PositionLeftCenter;
    public Vector2 PositionRightCenter;
    [Header("射击点")]
    public Transform Rotation1;
    public Transform Rotation2;
    public Transform Rotation3;
    public Transform Rotation4;
    [Header("射击计时器")]
    public float ShootTime;
    private float ShootTime_Count;
    [Header("自动跳跃计时器")]
    private Vector3 LastPosition = Vector3.zero;
    public float AutoUpTime;
    private float AutoUpTime_Count;
    public float AutoUpSpeed;
    private void Awake()
    {
        ShootTime_Count = ShootTime;
        rb = GetComponent<Rigidbody2D>();
        ThisStats = GetComponent<CharacterStats>();
    }
    private void Start()
    {
        LeftWalk();
    }
    private void Update()
    {
        if (ShootTime_Count > -2)
        {
            ShootTime_Count -= Time.deltaTime;
        }
        if (ShootTime_Count < 0)
        {
            ShootTime_Count = ShootTime;
            Shoot();
        }
        AutoUp();
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
    private void Shoot()
    {
        var NewBall1 = Instantiate(DownBall, transform.position + Rotation1.localPosition, Quaternion.identity);
        NewBall1.GetComponent<Rigidbody2D>().velocity = Rotation1.localPosition * BallSpeed;
        var NewBall2 = Instantiate(DownBall, transform.position + Rotation2.localPosition, Quaternion.identity);
        NewBall2.GetComponent<Rigidbody2D>().velocity = Rotation2.localPosition * BallSpeed;
        var NewBall3 = Instantiate(DownBall, transform.position + Rotation3.localPosition, Quaternion.identity);
        NewBall3.GetComponent<Rigidbody2D>().velocity = Rotation3.localPosition * BallSpeed;
        var NewBall4 = Instantiate(DownBall, transform.position + Rotation4.localPosition, Quaternion.identity);
        NewBall4.GetComponent<Rigidbody2D>().velocity = Rotation4.localPosition * BallSpeed;
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
