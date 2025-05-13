using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BulletBox : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Transform bulletBox;
    public Bullet bullet;
    private Animator anim;
    [Header("¹¥ËÙ¼ÆÊ±Æ÷")]
    public float BaseAttackSpeedTime;
    private float AttackSpeedTime_Count;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(spriteRenderer.sprite != GameManager.Instance.PlayerData.WeaponData.WeaponSprite)
        {
           spriteRenderer.sprite = GameManager.Instance.PlayerData.WeaponData.WeaponSprite;
        }
        anim.SetBool("Shoot", KeyBoardManager.Instance.GetKey_Mouse0());
        if (AttackSpeedTime_Count >= -1)
        {
            AttackSpeedTime_Count -= Time.deltaTime;
        }
        if (KeyBoardManager.Instance.GetKey_Mouse0() && AttackSpeedTime_Count < 0)
        {
            if (GameManager.Instance.Player().TurbulentRadiation)
            {
                AttackSpeedTime_Count = BaseAttackSpeedTime * (GameManager.Instance.Player().AttackRate - GameManager.Instance.Player().WaterElementBonus * 0.2f);
            }
            else
            {
                AttackSpeedTime_Count = BaseAttackSpeedTime * GameManager.Instance.Player().AttackRate;
            }
            if (GameManager.Instance.Player().MucusDeathRage)
            {
                AttackSpeedTime_Count *= 0.5f + ((GameManager.Instance.Player().NowHealth / GameManager.Instance.Player().MaxHealth) / 2);
            }
            if (GameManager.Instance.Player().SlimeRunningChampion)
            {
                AttackSpeedTime_Count *=  1 - (GameManager.Instance.Player().SpeedRate - 1);
            }
            SetBullet();
        }
    }
    private void SetBullet()
    {
        var NewBullet = Instantiate(bullet, transform.GetChild(0));
        var bulletRotation = ((Vector2)transform.GetChild(0).position - (Vector2)transform.position).normalized;
        NewBullet.GetComponent<Bullet>().BulletRotation = bulletRotation;
        NewBullet.transform.SetParent(bulletBox);
        if (GameManager.Instance.Player().ThunderBreathIllusion)
        {
            var ThunderCount = UnityEngine.Random.Range(0f, 1f);
            if(ThunderCount < 0.2f)
            {
                Invoke("ExtraBullet", 0.05f);
            }
        }
    }
    private void ExtraBullet()
    {
        var NewBullet = Instantiate(bullet, transform.GetChild(0));
        var bulletRotation = ((Vector2)transform.GetChild(0).position - (Vector2)transform.position).normalized;
        NewBullet.GetComponent<Bullet>().BulletRotation = bulletRotation;
        NewBullet.transform.SetParent(bulletBox);
        CancelInvoke("ExtraBullet");
    } 
}
