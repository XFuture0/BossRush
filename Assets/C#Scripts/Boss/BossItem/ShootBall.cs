using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShootBall : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 PlayerPosition;
    public float Speed;
    private float NowSpeed;
    private float WaitTime;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        WaitTime = 1;
        NowSpeed = Speed;
        InvokeRepeating("SpeedUp", 1, 0.2f);
    }
    private void Update()
    {
        PlayerPosition = GameManager.Instance.PlayerStats.gameObject.transform.position;
        GetComponent<SpriteRenderer>().color = ColorManager.Instance.UpdateColor(2);
    }
    private void FixedUpdate()
    {
        WaitTime -= Time.deltaTime;
        if (WaitTime <= 0)
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
            GameManager.Instance.Attack(GameManager.Instance.BossStats, GameManager.Instance.PlayerStats);
            Destroy(gameObject);
        }
        if(other.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
