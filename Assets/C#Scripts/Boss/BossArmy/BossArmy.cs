using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossArmy : MonoBehaviour
{
    private CharacterStats bossarmy;
    private SpriteRenderer spriteRenderer;
    private BossArmyAnim anim;
    private Rigidbody2D rb;
    public LineRenderer LinkLine;
    private bool IsStop;
    private Vector3 PlayerPosition;
    private Vector2 PlayerRotation;
    public bool IsLevel4;
    public bool IsLevel5;
    public bool Level5Line;
    public GameObject BossArmyBall;
    [Header("Éä»÷¼ÆÊ±Æ÷")]
    private float ShootTime_Count;
    private bool IsShoot;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        bossarmy = GetComponent<CharacterStats>();
        anim = GetComponent<BossArmyAnim>();
        ShootTime_Count = UnityEngine.Random.Range(5f, 10f);
    }
    private void Update()
    {
        if (IsLevel4)
        {
            Shoot();
        }
        if (IsLevel5)
        {
            IsLevel5 = false;
            Level5Line = true;
            InvokeRepeating("RebornBoss", 0, 1);
        }
        if (Level5Line)
        { 
            if (LinkLine.material.color != ColorManager.Instance.UpdateColor(2))
            {
                LinkLine.material.color = ColorManager.Instance.UpdateColor(2);
            }
            LinkLine.SetPosition(0, transform.position);
            LinkLine.SetPosition(1, GameManager.Instance.BossStats.gameObject.transform.position);
        }
        PlayerPosition = GameManager.Instance.PlayerStats.gameObject.transform.position;
        BossArmyDead();
    }
    private void FixedUpdate()
    {
        if(spriteRenderer.color != ColorManager.Instance.UpdateColor(2))
        {
            spriteRenderer.color = ColorManager.Instance.UpdateColor(2);
        }
        if (!IsStop)
        {
            TrackPlayer();
        }
        if (IsStop)
        {
            rb.velocity = Vector3.zero;
        }
        ChangeFace();
    }
    private void Shoot()
    {
        if(ShootTime_Count >= -2)
        {
            ShootTime_Count -= Time.deltaTime;
        }
        if(ShootTime_Count <= 0 && !IsShoot)
        {
            IsShoot = true;
            StartCoroutine(OnShoot());
        }
    }
    private void TrackPlayer()
    {
        PlayerRotation = ((Vector2)PlayerPosition - (Vector2)transform.position).normalized;
        rb.velocity = PlayerRotation * bossarmy.CharacterData_Temp.Speed * Time.deltaTime;
    }
    private void ChangeFace()
    {
        if(PlayerPosition.x - transform.position.x >= 0)//ÓÒ
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if(PlayerPosition.x - transform.position.x < 0)//×ó
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameManager.Instance.Attack(bossarmy,GameManager.Instance.PlayerStats);
        }
    }
    private IEnumerator OnShoot()
    {
        IsStop = true;
        anim.OnAttack();
        yield return new WaitForSeconds(2f);
        Instantiate(BossArmyBall, transform.position - new Vector3(transform.localScale.x * 1, 0, 0), Quaternion.identity);
        anim.OnAttackEnd();
        yield return new WaitForSeconds(0.5f);
        anim.OnEnd();
        IsStop = false;
        ShootTime_Count = UnityEngine.Random.Range(5f, 10f);
    }
    private void RebornBoss()
    {
        GameManager.Instance.Boss().NowHealth += 0.5f;
        if(GameManager.Instance.Boss().NowHealth >= GameManager.Instance.Boss().MaxHealth)
        {
            GameManager.Instance.Boss().NowHealth = GameManager.Instance.Boss().MaxHealth;
        }
    }
    private void BossArmyDead()
    {
        if(bossarmy.CharacterData_Temp.NowHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
