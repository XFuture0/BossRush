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
    private bool IsJumpDown;
    private bool IsAddWeapon;
    private bool IsAddPoizonWeapon;
    private bool IsAddThunderWeapon;
    private bool IsHormoneGel;
    private float HormoneGelBaseSpeed;
    public PlayerData PlayerData;
    private CharacterStats Player;
    public GameObject EndCanvs;
    public Bullet bullet;
    private float AngerTime = 1;
    public delegate void AngerSkill();
    public AngerSkill angerskill;
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
    [Header("怒气计时器")]
    private bool IsAnger;
    private float AngerTime_Count = -2;
    private float BaseSpeed;
    private float BaseAttackRate;
    private float BaseBulletSpeed;
    private float BasePoizonDamage;
    private float BaseThunderRate;
    [Header("事件监听")]
    public VoidEventSO BossDeadEvent;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Check = rb.GetComponent<PlayerCheck>();
        Player = GetComponent<CharacterStats>();
        PlayerDashPool = new ObjectPool<PlayerDashTemp>(DashTemp);
        angerskill = BaseAngerSkill;
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
        OnImperialWeapons();
        OnPoizonWeapons();
        OnThunderWeapon();
        if (!Player.CharacterData_Temp.FuriousGatling)
        {
            PlayerAnger();
            AddAnger();
        }
        else if (Player.CharacterData_Temp.FuriousGatling)
        {
            UseGatling();
        }
        PlayerData.PlayerPosition = transform.position;
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
            IsJumpDown = false;
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
        if (Check.IsGround && rb.velocity.y <= 0 && !IsJumpDown)
        {
            IsJumpDown = true;
            CurJumpCount = Player.CharacterData_Temp.JumpCount;
            if (Player.CharacterData_Temp.UrgentEngine)
            {
                StartCoroutine(OnUrgentEngine());
            }
        }
    }
    private IEnumerator OnUrgentEngine()
    {
        var Basespeed = Player.CharacterData_Temp.SpeedRate;
        Player.CharacterData_Temp.SpeedRate *= 1.5f;
        yield return new WaitForSeconds(0.2f);
        Player.CharacterData_Temp.SpeedRate = Basespeed;
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
            if (Player.CharacterData_Temp.QuickAngerGel)
            {
                Player.CharacterData_Temp.AngerValue += 0.02f;
            }
            if (Player.CharacterData_Temp.NetworkLag)
            {
                StartCoroutine(OnNetworkLag());
            }
        }
        if (Player.CharacterData_Temp.HormoneGel)
        {
            if (KeyBoardManager.Instance.GetKey_Shift() && !IsHormoneGel)
            {
                IsHormoneGel = true;
                HormoneGelBaseSpeed = Player.CharacterData_Temp.SpeedRate;
                Player.CharacterData_Temp.SpeedRate = 1.5f;
            }
            else if(!KeyBoardManager.Instance.GetKey_Shift() && IsHormoneGel)
            {
                IsHormoneGel = false;
                Player.CharacterData_Temp.SpeedRate = HormoneGelBaseSpeed;
            }
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
        if (Player.CharacterData_Temp.SprintBuffer)
        {
            StartCoroutine(OnSprintBuffer());
        }
    }
    private IEnumerator OnSprintBuffer()
    {
        var BaseSpeed = Player.CharacterData_Temp.SpeedRate;
        Player.CharacterData_Temp.SpeedRate = 1.8f;
        yield return new WaitForSeconds(0.5f);
        Player.CharacterData_Temp.SpeedRate = BaseSpeed;
    }
    private IEnumerator OnNetworkLag()
    {
        var BacePosition = transform.position;
        yield return new WaitForSeconds(1);
        transform.position = BacePosition;
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
    public void ChangeAngerSkill(int Index)
    {
        switch (Index)
        {
            case 1:
                angerskill = BaseAngerSkill;
                break;
            case 2:
                angerskill = SacredAnger;
                break;
            case 3:
                angerskill = AlcoholAddictedSlime;
                break;
            case 4:
                angerskill = ThunderGodWrath;
                break;
        }
    }
    private void PlayerAnger()
    {
        angerskill();
    }
    private void BaseAngerSkill()
    {
        if (Player.CharacterData_Temp.AngerValue >= GameManager.Instance.Player().FullAnger && KeyBoardManager.Instance.GetKeyDown_F() && !IsAnger)
        {
            IsAnger = true;
            BaseAttackRate = Player.CharacterData_Temp.AttackRate;
            BaseBulletSpeed = bullet.BulletSpeed;
            Player.CharacterData_Temp.AttackRate = 0.2f;
            bullet.BulletSpeed = 30;
            AngerTime_Count = Player.CharacterData_Temp.AngerTime;
        }
        if (AngerTime_Count >= -1 && IsAnger)
        {
            Player.CharacterData_Temp.AngerValue = (AngerTime_Count / Player.CharacterData_Temp.AngerTime) * GameManager.Instance.Player().FullAnger;
            AngerTime_Count -= Time.deltaTime;
        }
        if (AngerTime_Count < 0 && IsAnger)
        {
            IsAnger = false;
            Player.CharacterData_Temp.AttackRate = BaseAttackRate;
            bullet.BulletSpeed = BaseBulletSpeed;
            Player.CharacterData_Temp.AngerValue = 0;
        }
    }
    private void SacredAnger()
    {
        if (Player.CharacterData_Temp.AngerValue >= GameManager.Instance.Player().FullAnger && KeyBoardManager.Instance.GetKeyDown_F() && !IsAnger)
        {
            IsAnger = true;
            Player.Invincible = true;
            Player.InvincibleTime_Count = Player.CharacterData_Temp.AngerTime;
            BaseSpeed = Player.CharacterData_Temp.SpeedRate;
            BaseAttackRate = Player.CharacterData_Temp.AttackRate;
            Player.CharacterData_Temp.AttackRate = BaseAttackRate * 0.5f;
            Player.CharacterData_Temp.SpeedRate = BaseSpeed * 1.5f;
            AngerTime_Count = Player.CharacterData_Temp.AngerTime;
        }
        if (AngerTime_Count >= -1 && IsAnger)
        {
            Player.CharacterData_Temp.AngerValue = (AngerTime_Count / Player.CharacterData_Temp.AngerTime) * GameManager.Instance.Player().FullAnger;
            AngerTime_Count -= Time.deltaTime;
        }
        if (AngerTime_Count < 0 && IsAnger)
        {
            IsAnger = false;
            Player.CharacterData_Temp.AttackRate = BaseAttackRate;
            Player.CharacterData_Temp.SpeedRate = BaseSpeed;
            Player.CharacterData_Temp.AngerValue = 0;
        }
    }
    private void AlcoholAddictedSlime()
    {
        if (Player.CharacterData_Temp.AngerValue >= GameManager.Instance.Player().FullAnger && KeyBoardManager.Instance.GetKeyDown_F() && !IsAnger)
        {
            IsAnger = true;
            Player.Invincible = true;
            Player.InvincibleTime_Count = Player.CharacterData_Temp.AngerTime;
            BasePoizonDamage = Player.CharacterData_Temp.PoizonDamage;
            BaseAttackRate = Player.CharacterData_Temp.AttackRate;
            Player.CharacterData_Temp.AttackRate = 0.2f;
            Player.CharacterData_Temp.PoizonDamage = BasePoizonDamage * 2;
            AngerTime_Count = Player.CharacterData_Temp.AngerTime;
        }
        if (AngerTime_Count >= -1 && IsAnger)
        {
            Player.CharacterData_Temp.AngerValue = (AngerTime_Count / Player.CharacterData_Temp.AngerTime) * GameManager.Instance.Player().FullAnger;
            AngerTime_Count -= Time.deltaTime;
        }
        if (AngerTime_Count < 0 && IsAnger)
        {
            IsAnger = false;
            Player.CharacterData_Temp.AttackRate = BaseAttackRate;
            Player.CharacterData_Temp.PoizonDamage = BasePoizonDamage;
            Player.CharacterData_Temp.AngerValue = 0;
        }
    }
    private void ThunderGodWrath()
    {
        if (Player.CharacterData_Temp.AngerValue >= GameManager.Instance.Player().FullAnger && KeyBoardManager.Instance.GetKeyDown_F() && !IsAnger)
        {
            IsAnger = true;
            Player.Invincible = true;
            Player.InvincibleTime_Count = Player.CharacterData_Temp.AngerTime;
            BaseThunderRate = Player.CharacterData_Temp.ThunderRate;
            AngerTime_Count = Player.CharacterData_Temp.AngerTime;
        }
        if (AngerTime_Count >= -1 && IsAnger)
        {
            Player.CharacterData_Temp.AngerValue = (AngerTime_Count / Player.CharacterData_Temp.AngerTime) * GameManager.Instance.Player().FullAnger;
            AngerTime_Count -= Time.deltaTime;
        }
        if (AngerTime_Count < 0 && IsAnger)
        {
            IsAnger = false;
            Player.CharacterData_Temp.ThunderRate = BaseThunderRate;
            Player.CharacterData_Temp.AngerValue = 0;
        }
    }
    private void AddAnger()
    {
        if (Player.CharacterData_Temp.FearlessFury && GameManager.Instance.BossStats.gameObject.activeSelf && Player.CharacterData_Temp.AngerValue <= 1.1f)
        {
            if (AngerTime >= 0)
            {
                AngerTime -= Time.deltaTime;
            }
            else
            {
                AngerTime = 1;
                Player.CharacterData_Temp.AngerValue += 0.02f;
            }
        }
    }
    private void UseGatling()
    {
        if(KeyBoardManager.Instance.GetKeyDown_F() && !IsAnger)
        {
            IsAnger = true;
            BaseAttackRate = Player.CharacterData_Temp.AttackRate;
            BaseBulletSpeed = bullet.BulletSpeed;
        }
        if (KeyBoardManager.Instance.GetKey_F() && IsAnger)
        {
            KeyBoardManager.Instance.StopMoveKey = true;
            Player.CharacterData_Temp.AttackRate = 0.2f;
            bullet.BulletSpeed = 30;
            rb.velocity = new Vector2(0,rb.velocity.y);
        }
        else if (!KeyBoardManager.Instance.GetKey_F() && IsAnger)
        {
            IsAnger = false;
            KeyBoardManager.Instance.StopMoveKey = false;
            Player.CharacterData_Temp.AttackRate = BaseAttackRate;
            bullet.BulletSpeed = BaseBulletSpeed;
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
    private void OnImperialWeapons()
    {
        if (Player.CharacterData_Temp.ImperialWeapons)
        {
            if(Player.CharacterData_Temp.SpeedRate >= 1.5f && !IsAddWeapon)
            {
                IsAddWeapon = true;
                Player.CharacterData_Temp.WeaponCount += 1; 
            }
            if(Player.CharacterData_Temp.SpeedRate < 1.5f && IsAddWeapon)
            {
                IsAddWeapon = false;
                Player.CharacterData_Temp.WeaponCount -= 1;
            }
        }
    }
    private void OnPoizonWeapons()
    {
        if (Player.CharacterData_Temp.PoizonWeapons)
        {
            if (Player.CharacterData_Temp.PoizonDamage >= 0.5f && !IsAddPoizonWeapon)
            {
                IsAddPoizonWeapon = true;
                Player.CharacterData_Temp.WeaponCount += 1;
            }
            if (Player.CharacterData_Temp.PoizonDamage < 0.5f && IsAddPoizonWeapon)
            {
                IsAddPoizonWeapon = false;
                Player.CharacterData_Temp.WeaponCount -= 1;
            }
        }
    }
    private void OnThunderWeapon()
    {
        if (Player.CharacterData_Temp.ThunderWeapon)
        {
            if (Player.CharacterData_Temp.ThunderBonus >= 1.5f && !IsAddThunderWeapon)
            {
                IsAddThunderWeapon = true;
                Player.CharacterData_Temp.WeaponCount += 1;
            }
            if (Player.CharacterData_Temp.ThunderBonus < 1.5f && IsAddThunderWeapon)
            {
                IsAddThunderWeapon = false;
                Player.CharacterData_Temp.WeaponCount -= 1;
            }
        }
    }
}
