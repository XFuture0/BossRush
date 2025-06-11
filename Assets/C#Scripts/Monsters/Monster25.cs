using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster25 : MonoBehaviour
{
    private CharacterStats ThisStats;
    private Rigidbody2D rb;
    public GameObject ShootBall;
    public float BallSpeed;
    [Header("Éä»÷µã")]
    public Transform Rotation1;
    public Transform Rotation2;
    public Transform Rotation3;
    public Transform Rotation4;
    public Transform Rotation5;
    public Transform Rotation6;
    public Transform Rotation7;
    public Transform Rotation8;
    [Header("Éä»÷¼ÆÊ±Æ÷")]
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
        rb.velocity = new Vector2(MoveRo.x * 2, MoveRo.y) * ThisStats.CharacterData_Temp.Speed * Time.deltaTime;
    }
    private void Shoot()
    {
        var NewBall1 = Instantiate(ShootBall, transform.position + Rotation1.localPosition, Quaternion.identity);
        NewBall1.GetComponent<Rigidbody2D>().velocity = Rotation1.localPosition * BallSpeed;
        var NewBall2 = Instantiate(ShootBall, transform.position + Rotation2.localPosition, Quaternion.identity);
        NewBall2.GetComponent<Rigidbody2D>().velocity = Rotation2.localPosition * BallSpeed;
        var NewBall3 = Instantiate(ShootBall, transform.position + Rotation3.localPosition, Quaternion.identity);
        NewBall3.GetComponent<Rigidbody2D>().velocity = Rotation3.localPosition * BallSpeed;
        var NewBall4 = Instantiate(ShootBall, transform.position + Rotation4.localPosition, Quaternion.identity);
        NewBall4.GetComponent<Rigidbody2D>().velocity = Rotation4.localPosition * BallSpeed;
        var NewBall5 = Instantiate(ShootBall, transform.position + Rotation5.localPosition, Quaternion.identity);
        NewBall5.GetComponent<Rigidbody2D>().velocity = Rotation5.localPosition * BallSpeed;
        var NewBall6 = Instantiate(ShootBall, transform.position + Rotation6.localPosition, Quaternion.identity);
        NewBall6.GetComponent<Rigidbody2D>().velocity = Rotation6.localPosition * BallSpeed;
        var NewBall7 = Instantiate(ShootBall, transform.position + Rotation7.localPosition, Quaternion.identity);
        NewBall7.GetComponent<Rigidbody2D>().velocity = Rotation7.localPosition * BallSpeed;
        var NewBall8 = Instantiate(ShootBall, transform.position + Rotation8.localPosition, Quaternion.identity);
        NewBall8.GetComponent<Rigidbody2D>().velocity = Rotation8.localPosition * BallSpeed;
    }
}
