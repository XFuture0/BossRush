using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerCheck Check;
    private float InputX;
    public bool Isdead;
    public PlayerData PlayerData;
    private CharacterStats Player;
    public GameObject EndCanvs;
    [Header("临时属性")]
    public float BaseSpeed;
    private float JumpForce;
    private float JumpDownSpeed_Max;
    [Header("自然回血计时器")]
    private float RebornTiemCount = -2;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Check = rb.GetComponent<PlayerCheck>();
        Player = GetComponent<CharacterStats>();
    }
    private void Start()
    {
        RefreshData();
    }
    private void Update()
    {
        if(Player.CharacterData_Temp.NowHealth > Player.CharacterData_Temp.MaxHealth)
        {
            Player.CharacterData_Temp.NowHealth = Player.CharacterData_Temp.MaxHealth;
        }
        Jump();
        PlayerDead();
        Reborn();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        InputX = KeyBoardManager.Instance.GetHorizontalRaw();
        rb.velocity = new Vector2(InputX * BaseSpeed * Player.CharacterData_Temp.Speed * Time.deltaTime, rb.velocity.y);
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void Jump()
    {
        if (KeyBoardManager.Instance.GetKeyDown_Space() && Check.IsGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }
        if (!Check.IsGround && rb.velocity.y < 0)
        {
            if (rb.velocity.y <= JumpDownSpeed_Max)
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpDownSpeed_Max);
            }
        }
    }
    private void Reborn()
    {
        if (RebornTiemCount > -1)
        {
            RebornTiemCount -= Time.deltaTime;
        }
        if (RebornTiemCount <= 0)
        {
            Player.CharacterData_Temp.NowHealth += Player.CharacterData_Temp.HealCount;
            RebornTiemCount = Player.CharacterData_Temp.AutoHealTime;
        }
    }
    private void RefreshData()
    {
        JumpForce = PlayerData.JumpForce;
        JumpDownSpeed_Max = PlayerData.JumpDownSpeed_Max;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Boss" && transform.GetChild(0).gameObject.activeSelf)
        {
            GameManager.Instance.Attack(gameObject.GetComponent<CharacterStats>(), other.GetComponent<CharacterStats>());
        }
    }
    private void PlayerDead()
    {
        if(GameManager.Instance.PlayerStats.CharacterData_Temp.NowHealth <= 0 && !Isdead)
        {
            Isdead = true;
            KeyBoardManager.Instance.StopMoveKey = true;
            EndCanvs.SetActive(true);
        }
    }
}
