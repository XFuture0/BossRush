using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster19 : MonoBehaviour
{
    private CharacterStats ThisStats;
    private Rigidbody2D rb;
    public LayerMask Ground;
    public GameObject ShootBall;
    public float BallSpeed;
    [Header("ת��λ��")]
    public Vector2 PositionLeft;
    public Vector2 PositionRight;
    public Vector2 PositionLeftCenter;
    public Vector2 PositionRightCenter;
    [Header("�����")]
    public Transform Rotation1;
    [Header("�����ʱ��")]
    public float ShootTime;
    private float ShootTime_Count;
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
        var NewBall1 = Instantiate(ShootBall, transform.position + Rotation1.localPosition, Quaternion.identity);
        NewBall1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -BallSpeed);
    }
}
