using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster9 : MonoBehaviour
{
    public GameObject MiniBomb;
    private CharacterStats ThisStats;
    private Rigidbody2D rb;
    [Header("Éä»÷µã")]
    public Transform Rotation1;
    [Header("Éä»÷¼ÆÊ±Æ÷")]
    public float ShootTime;
    private float ShootTime_Count;
    private void Awake()
    {
        ShootTime_Count = ShootTime;
        ThisStats = GetComponent<CharacterStats>();
        rb = GetComponent<Rigidbody2D>();
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
        rb.velocity =new Vector2(MoveRo.x * 2,MoveRo.y)* ThisStats.CharacterData_Temp.Speed * Time.deltaTime;
    }
    private void Shoot()
    {
        var NewBall1 = Instantiate(MiniBomb, transform.position + Rotation1.localPosition, Quaternion.identity);
    }
}
