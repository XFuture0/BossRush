using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster23 : MonoBehaviour
{
    private CharacterStats ThisStats;
    private Rigidbody2D rb;
    public LayerMask Ground;
    public GameObject ShootBall;
    public float BallSpeed;
    [Header("转弯位置")]
    public Vector2 PositionLeft;
    public Vector2 PositionRight;
    public Vector2 PositionLeftCenter;
    public Vector2 PositionRightCenter;
    [Header("射击点")]
    public Transform Rotation1;
    public Transform Rotation2;
    [Header("射击计时器")]
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
            StartCoroutine(Shoot());
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
    private IEnumerator Shoot()
    {
        for(int i = 0; i < 3; i++)
        {
            var NewBall1 = Instantiate(ShootBall, transform.position + Rotation1.localPosition, Quaternion.identity);
            NewBall1.GetComponent<Rigidbody2D>().velocity = Rotation1.localPosition * BallSpeed;
            var NewBall2 = Instantiate(ShootBall, transform.position + Rotation2.localPosition, Quaternion.identity);
            NewBall2.GetComponent<Rigidbody2D>().velocity = Rotation2.localPosition * BallSpeed;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
