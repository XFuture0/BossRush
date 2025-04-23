using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BulletBox : MonoBehaviour
{
    public Transform bulletBox;
    public Bullet bullet;
    [Header("¹¥ËÙ¼ÆÊ±Æ÷")]
    public float BaseAttackSpeedTime;
    private float AttackSpeedTime_Count;
    private void Update()
    {
        if (AttackSpeedTime_Count >= -1)
        {
            AttackSpeedTime_Count -= Time.deltaTime;
        }
        if (KeyBoardManager.Instance.GetKey_Mouse0() && AttackSpeedTime_Count < 0)
        {
            AttackSpeedTime_Count = BaseAttackSpeedTime * GameManager.Instance.PlayerStats.CharacterData_Temp.AttackRate;
            SetBullet();
        }
    }
    private void SetBullet()
    {
        var NewBullet = Instantiate(bullet, transform.GetChild(0));
        var bulletRotation = ((Vector2)transform.GetChild(0).position - (Vector2)transform.position).normalized;
        NewBullet.GetComponent<Bullet>().BulletRotation = bulletRotation;
        NewBullet.transform.SetParent(bulletBox);
    }
}
