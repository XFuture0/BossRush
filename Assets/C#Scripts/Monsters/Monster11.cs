using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster11 : MonoBehaviour
{
    public GameObject ShootBall;
    public float BallSpeed;
    [Header("Éä»÷µã")]
    public Transform Rotation1;
    public Transform Rotation2;
    [Header("Éä»÷¼ÆÊ±Æ÷")]
    public float ShootTime;
    private float ShootTime_Count;
    private void Awake()
    {
        ShootTime_Count = ShootTime;
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
    private void Shoot()
    {
        var NewBall1 = Instantiate(ShootBall, transform.position + Rotation1.localPosition, Quaternion.identity);
        NewBall1.GetComponent<Rigidbody2D>().velocity = new Vector2(BallSpeed, 0);
        var NewBall2 = Instantiate(ShootBall, transform.position + Rotation2.localPosition, Quaternion.identity);
        NewBall2.GetComponent<Rigidbody2D>().velocity = new Vector2(-BallSpeed, 0);
    }
}
