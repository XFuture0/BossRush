using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster15 : MonoBehaviour
{
    private CharacterStats ThisStats;
    private Rigidbody2D rb;
    public GameObject DownBall;
    public float BallSpeed;
    [Header("�����")]
    public Transform Rotation1;
    public Transform Rotation2;
    public Transform Rotation3;
    public Transform Rotation4;
    [Header("�����ʱ��")]
    public float ShootTime;
    private float ShootTime_Count;
    private void Awake()
    {
        ShootTime_Count = ShootTime;
        rb = GetComponent<Rigidbody2D>();
        ThisStats = GetComponent<CharacterStats>();
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
        var MoveRo = (SceneChangeManager.Instance.Player.transform.position - transform.position).normalized;
        rb.velocity = new Vector2(MoveRo.x * 2,MoveRo.y) * ThisStats.CharacterData_Temp.Speed * Time.deltaTime;
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
}
