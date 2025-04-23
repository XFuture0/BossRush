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
    private float JumpForce;
    private float JumpDownSpeed_Max;
    private int CurDashCount;
    private int CurJumpCount;
    [Header("冲刺")]
    public PlayerDashTemp DashTemp;
    public Transform DashPool;
    [HideInInspector] public ObjectPool<PlayerDashTemp> PlayerDashPool;
    public float DashForce;
    public float DashTime;
    private float DashTime_Count;
    private bool IsDash;
    public float CanDashTime;
    private float CanDashTime_Count;
    [Header("事件监听")]
    public VoidEventSO BossDeadEvent;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Check = rb.GetComponent<PlayerCheck>();
        Player = GetComponent<CharacterStats>();
        PlayerDashPool = new ObjectPool<PlayerDashTemp>(DashTemp);
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
        CheckDash();
    }
    private void FixedUpdate()
    {
        Move();
        if (IsDash)
        {
            Dash();
        }
    }
    private void Move()
    {
        InputX = KeyBoardManager.Instance.GetHorizontalRaw();
        rb.velocity = new Vector2(InputX * GameManager.Instance.PlayerStats.CharacterData_Temp.Speed * Player.CharacterData_Temp.SpeedRate * Time.deltaTime, rb.velocity.y);
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
        if (KeyBoardManager.Instance.GetKeyDown_Space()&& CurJumpCount > 0)
        {
            CurJumpCount--;
            rb.velocity = Vector2.zero;
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }
        if (!Check.IsGround && rb.velocity.y < 0)
        {
            if (rb.velocity.y <= JumpDownSpeed_Max)
            {
                rb.velocity = new Vector2(rb.velocity.x, JumpDownSpeed_Max);
            }
        }
        if (Check.IsGround && rb.velocity.y <= 0)
        {
            CurJumpCount = Player.CharacterData_Temp.JumpCount;
        }
    }
    private void CheckDash()
    {
        if(CanDashTime_Count <= 0 && Player.CharacterData_Temp.CanDash)
        {
            CurDashCount = Player.CharacterData_Temp.DashCount;
        }
        if(CanDashTime_Count > -2)
        {
            CanDashTime_Count -= Time.deltaTime;
        }
        if (DashTime_Count > -2)
        {
            DashTime_Count -= Time.deltaTime;
        }
        if (DashTime_Count <= 0 && IsDash)
        {
            IsDash = false;
            EndDash();
        }
        if (KeyBoardManager.Instance.GetKeyDown_Shift() && CurDashCount > 0)
        {
            if (Player.CharacterData_Temp.DashInvincibleFrame)
            {
                Player.Invincible = true;
                Player.InvincibleTime_Count = Player.CharacterData.InvincibleTime;
            }
            CurDashCount--;
            CanDashTime_Count = CanDashTime + DashTime;
            DashTime_Count = DashTime;
            IsDash = true;
            rb.gravityScale = 0;
            KeyBoardManager.Instance.StopMoveKey = true;
            InvokeRepeating("AddPlayerDashTemp", 0, 0.02f);
        }
    }
    private void Dash()
    {
        rb.velocity = new Vector2(transform.localScale.x,0) * DashForce;
    }
    private void AddPlayerDashTemp()
    {
        var NewTemp = PlayerDashPool.GetObject(DashPool);
        NewTemp.transform.position = transform.position;
    }
    private void EndDash()
    {
        CancelInvoke("AddPlayerDashTemp");
        rb.velocity = Vector2.zero;
        KeyBoardManager.Instance.StopMoveKey = false;
        rb.gravityScale = Settings.PlayerGravity;
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
    private void OnEnable()
    {
        BossDeadEvent.OnEventRaised += OnBossDead;
    }
    private void OnBossDead()
    {
        Player.CharacterData_Temp.NowHealth += Player.CharacterData_Temp.AutoHealCount;
    }
    private void OnDisable()
    {
        BossDeadEvent.OnEventRaised -= OnBossDead;
    }
}
