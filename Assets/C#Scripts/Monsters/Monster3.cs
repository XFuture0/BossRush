using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster3 : MonoBehaviour
{
    public GameObject ShootBall;
    [Header("¹¥»÷¼ÆÊ±Æ÷")]
    public float AttackTime;
    private float AttackTimeCount;
    private void Awake()
    {
        AttackTimeCount = AttackTime;
    }
    private void Update()
    {
        if(AttackTimeCount > -2)
        {
            AttackTimeCount -= Time.deltaTime;
        }
        if(AttackTimeCount <= 0)
        {
            AttackTimeCount = AttackTime;
            Shoot();
        }
    }
    private void Shoot()
    {
        var NewBall = Instantiate(ShootBall, transform.position + new Vector3(0, 0.3f, 0),Quaternion.identity);
    }
}
