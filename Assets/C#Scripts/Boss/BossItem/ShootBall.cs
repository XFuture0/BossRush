using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShootBall : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Vector3 PlayerPosition;
    public float Speed;
    private float NowSpeed;
    private float WaitTime;
    private bool IsHit;
    public bool IsLevel1;
    public bool IsLevel2;
    public bool IsLevel4;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        IsHit = false;
        WaitTime = 1;
        NowSpeed = Speed;
        InvokeRepeating("SpeedUp", 1, 0.2f);
    }
    private void Update()
    {
        PlayerPosition = GameManager.Instance.PlayerStats.gameObject.transform.position;
        if(GetComponent<SpriteRenderer>().color != ColorManager.Instance.UpdateColor(2))
        {
            GetComponent<SpriteRenderer>().color = ColorManager.Instance.UpdateColor(2);
        }
    }
    private void FixedUpdate()
    {
        WaitTime -= Time.deltaTime;
        if (WaitTime <= 0 && !IsHit)
        {
            TrackPlayer();
        }
    }
    private void TrackPlayer()
    {
        var ForwardRotation = (PlayerPosition - transform.position).normalized;
        rb.velocity = ForwardRotation * NowSpeed * Time.deltaTime;
    }
    private void SpeedUp()
    {
        NowSpeed *= 1.5f;
        if(NowSpeed >= Speed * 2.5f)
        {
            NowSpeed = Speed * 2.5f;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            IsHit = true;
            rb.velocity = Vector2.zero;
            GameManager.Instance.Attack(GameManager.Instance.BossStats, GameManager.Instance.PlayerStats);
            if (IsLevel1)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            if (IsLevel2)
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1);
            }
            if (IsLevel4)
            {
                transform.localScale = new Vector3(2, 2, 1);
            }
            anim.SetTrigger("Attack");
        }
        if(other.tag == "Ground")
        {
            IsHit = true;
            rb.velocity = Vector2.zero;
            if (IsLevel1)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            if (IsLevel2)
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1);
            }
            if (IsLevel4)
            {
                transform.localScale = new Vector3(2, 2, 1);
            }
            anim.SetTrigger("Attack");
        }
    }
    private void DestoryIng()
    {
        Destroy(gameObject);
    }
}
