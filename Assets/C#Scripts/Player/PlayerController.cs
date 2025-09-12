using System.Collections;
using Unity.Mathematics;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerCheck Check;
    private SpriteRenderer SpriteRenderer;
    private Vector3 LastPosition = Vector3.zero;
    public float AutoUpSpeed;
    private float InputX;
    public bool Isdead;
    private bool IsJumpDown;
    private bool IsAddWeapon;
    private bool IsAddPoizonWeapon;
    private bool IsAddThunderWeapon;
    private bool IsWeaponsSeaGod;
    private bool IsDangerousWeapons;
    private bool IsGlidingWaterSurface;
    private bool IsSpiderSense;
    private bool IsHormoneGel;
    private float HormoneGelBaseSpeed;
    public PlayerData PlayerData;
    private CharacterStats Player;
    public GameObject EndCanvs;
    public Bullet bullet;
    private float AngerTime = 1;
    public delegate void AngerSkill();
    public AngerSkill angerskill;
    public GameObject AngerRing;
    [Header("×Ô¶¯ÌøÔ¾¼ÆÊ±Æ÷")]
    public float AutoUpTime;
    private float AutoUpTime_Count;
    [Header("ÁÙÊ±ÊôÐÔ")]
    private int CurDashCount;
    private int CurJumpCount;
    [Header("³å´Ì")]
    public PlayerDashTemp DashTemp;
    public Transform DashPool;
    [HideInInspector] public ObjectPool<PlayerDashTemp> PlayerDashPool;
    public float DashForce;
    public float DashTime;
    private float DashTime_Count;
    private bool IsDash;
    public float CanDashTime;
    private float CanDashTime_Count;
    [Header("Å­Æø¼ÆÊ±Æ÷")]
    private bool IsAnger;
    private float AngerTime_Count = -2;
    private float BaseSpeed;
    private float BaseAttackRate;
    private float BaseBulletSpeed;
    private float BasePoizonDamage;
    private float BaseThunderRate;
    private float BaseMaxVulnerabilityRate;
    private float BaseWaterElementBonus;
    [Header("ÊÜ»÷¼ÆÊ±Æ÷")]
    private bool IsHurt;
    public float HurtSpeed;
    public float HurtTime;
    private float HurtTime_Count;
    [Header("ÊÂ¼þ¼àÌý")]
    public VoidEventSO BossDeadEvent;
    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        Check = rb.GetComponent<PlayerCheck>();
        Player = GetComponent<CharacterStats>();
        PlayerDashPool = new ObjectPool<PlayerDashTemp>(DashTemp);
        angerskill = BaseAngerSkill;
    }
    private void Start()
    {
        StopPlayer();
    }
    private void Update()
    {
        PlayerData.PlayerPosition = transform.position;
        AutoUp();
        Jump();
        OnHurt();
        PlayerDead();
        CheckDash();
        OnImperialWeapons();
        OnPoizonWeapons();
        OnThunderWeapon();
        OnWeaponsSeaGod();
        OnDangerousWeapons();
        OnGlidingWaterSurface();
        OnSpiderSense();
        if (!Player.CharacterData_Temp.FuriousGatling)
        {
            PlayerAnger();
            AddAnger();
        }
        else if (Player.CharacterData_Temp.FuriousGatling)
        {
            UseGatling();
        }
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
    private void AutoUp()
    {
        if(LastPosition != transform.position)
        {
            AutoUpTime_Count = AutoUpTime;
        }
        if(AutoUpTime_Count > -2 && KeyBoardManager.Instance.GetHorizontalRaw() != 0)
        {
            AutoUpTime_Count -= Time.deltaTime;
        }
        if(AutoUpTime_Count < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + AutoUpSpeed);
        }
        LastPosition = transform.position;
    }
    private void Jump()
    {
        if (KeyBoardManager.Instance.GetKeyDown_Space()&& CurJumpCount > 0)
        {
            IsJumpDown = false;
            CurJumpCount--;
            rb.velocity = Vector2.zero;
            rb.velocity = new Vector2(rb.velocity.x, PlayerData.JumpForce);
        }
        if (!Check.IsGround && rb.velocity.y < 0)
        {
            if (rb.velocity.y <= PlayerData.JumpDownSpeed_Max)
            {
                rb.velocity = new Vector2(rb.velocity.x, PlayerData.JumpDownSpeed_Max);
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
    public void StartHurt()
    {
        HurtTime_Count = HurtTime;
        IsHurt = true;
    }
    private void OnHurt()
    {
        if (IsHurt)
        {
            HurtTime_Count -= Time.deltaTime;
            var ThisAlpha = math.lerp(SpriteRenderer.color.a, 0, HurtSpeed * Time.deltaTime);
            if(ThisAlpha > 0.25f)
            {
                SpriteRenderer.color = new Color(SpriteRenderer.color.r, SpriteRenderer.color.g, SpriteRenderer.color.b, ThisAlpha);
            }
            else if(ThisAlpha <= 0.25f)
            {
                SpriteRenderer.color = new Color(1, 1, 1, 1);
            }
            if(HurtTime_Count <= 0)
            {
                IsHurt = false;
            }
        }
        if (!IsHurt)
        {
            SpriteRenderer.color = new Color(1, 1, 1, 1);
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
                Player.CharacterData_Temp.SpeedRate += 0.5f;
            }
            else if(!KeyBoardManager.Instance.GetKey_Shift() && IsHormoneGel)
            {
                IsHormoneGel = false;
                Player.CharacterData_Temp.SpeedRate -= 0.5f;
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
        NewTemp.transform.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Boss" && transform.GetChild(0).gameObject.activeSelf)
        {
            GameManager.Instance.Attack(gameObject.GetComponent<CharacterStats>(), other.GetComponent<CharacterStats>());
        }
        if(other.tag == "Boss" && IsDash && Player.CharacterData_Temp.DashDamage)
        {
            GameManager.Instance.Attack(gameObject.GetComponent<CharacterStats>(), other.GetComponent<CharacterStats>());
            if (Player.CharacterData_Temp.DangerousSprint)
            {
                GameManager.Instance.BossStats.gameObject.GetComponent<Dangerous>().SetDangerous(Player);
            }
        }
    }
    private void PlayerDead()
    {
        if(GameManager.Instance.PlayerStats.CharacterData_Temp.NowHealth <= 0 && !Isdead)
        {
            Isdead = true;
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
            case 5:
                angerskill = PassionBloodAnger;
                break;
            case 6:
                angerskill = DragonEnraged;
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
            AngerRing.SetActive(true);
            GameManager.Instance.UseFrameDrop();//¶ÙÖ¡
            BaseAttackRate = Player.CharacterData_Temp.AttackRate;
            BaseBulletSpeed = bullet.BulletSpeed;
            bullet.BulletSpeed = 30;
            Player.CharacterData_Temp.AttackRate = 0.2f;
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
            AngerRing.SetActive(false);
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
            AngerRing.SetActive(true);
            GameManager.Instance.UseFrameDrop();//¶ÙÖ¡
            Player.Invincible = true;
            Player.InvincibleTime_Count = Player.CharacterData_Temp.AngerTime;
            BaseBulletSpeed = bullet.BulletSpeed;
            bullet.BulletSpeed = 30;
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
            AngerRing.SetActive(false);
            bullet.BulletSpeed = BaseBulletSpeed;
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
            AngerRing.SetActive(true);
            GameManager.Instance.UseFrameDrop();//¶ÙÖ¡
            BaseBulletSpeed = bullet.BulletSpeed;
            bullet.BulletSpeed = 30;
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
            AngerRing.SetActive(false);
            bullet.BulletSpeed = BaseBulletSpeed;
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
            AngerRing.SetActive(true);
            GameManager.Instance.UseFrameDrop();//¶ÙÖ¡
            BaseBulletSpeed = bullet.BulletSpeed;
            bullet.BulletSpeed = 30;
            BaseThunderRate = Player.CharacterData_Temp.ThunderRate;
            Player.CharacterData_Temp.ThunderRate = 1;
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
            AngerRing.SetActive(false);
            bullet.BulletSpeed = BaseBulletSpeed;
            Player.CharacterData_Temp.ThunderRate = BaseThunderRate;
            Player.CharacterData_Temp.AngerValue = 0;
        }
    }
    private void PassionBloodAnger()
    {
        if (Player.CharacterData_Temp.AngerValue >= GameManager.Instance.Player().FullAnger && KeyBoardManager.Instance.GetKeyDown_F() && !IsAnger)
        {
            IsAnger = true;
            AngerRing.SetActive(true);
            GameManager.Instance.UseFrameDrop();//¶ÙÖ¡
            BaseBulletSpeed = bullet.BulletSpeed;
            bullet.BulletSpeed = 30;
            BaseMaxVulnerabilityRate = Player.CharacterData_Temp.MaxVulnerabilityRate;
            Player.CharacterData_Temp.MaxVulnerabilityRate = 1;
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
            AngerRing.SetActive(false);
            bullet.BulletSpeed = BaseBulletSpeed;
            Player.CharacterData_Temp.MaxVulnerabilityRate = BaseMaxVulnerabilityRate;
            Player.CharacterData_Temp.AngerValue = 0;
        }
    }
    private void DragonEnraged()
    {
        if (Player.CharacterData_Temp.AngerValue >= GameManager.Instance.Player().FullAnger && KeyBoardManager.Instance.GetKeyDown_F() && !IsAnger)
        {
            IsAnger = true;
            AngerRing.SetActive(true);
            GameManager.Instance.UseFrameDrop();//¶ÙÖ¡
            BaseBulletSpeed = bullet.BulletSpeed;
            bullet.BulletSpeed = 30;
            BaseWaterElementBonus = Player.CharacterData_Temp.WaterElementBonus;
            BaseAttackRate = Player.CharacterData_Temp.AttackRate;
            Player.CharacterData_Temp.WaterElementBonus += 1;
            Player.CharacterData_Temp.AttackRate = 0.4f;
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
            AngerRing.SetActive(false);
            bullet.BulletSpeed = BaseBulletSpeed;
            Player.CharacterData_Temp.WaterElementBonus = BaseWaterElementBonus;
            Player.CharacterData_Temp.AttackRate = BaseAttackRate;
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
    public void StopPlayer()
    {
        KeyBoardManager.Instance.StopAnyKey = true;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
    }
    public void ContinuePlayer()
    {
        KeyBoardManager.Instance.StopAnyKey = false;
        rb.gravityScale = Settings.PlayerGravity;
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
            }
            if(Player.CharacterData_Temp.SpeedRate < 1.5f && IsAddWeapon)
            {
                IsAddWeapon = false;
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
            }
            if (Player.CharacterData_Temp.PoizonDamage < 0.5f && IsAddPoizonWeapon)
            {
                IsAddPoizonWeapon = false;
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
            }
            if (Player.CharacterData_Temp.ThunderBonus < 1.5f && IsAddThunderWeapon)
            {
                IsAddThunderWeapon = false;
            }
        }
    }
    private void OnWeaponsSeaGod()
    {
        if (Player.CharacterData_Temp.WeaponsSeaGod)
        {
            if (Player.CharacterData_Temp.WaterElementBonus >= 0.6f && !IsWeaponsSeaGod)
            {
                IsWeaponsSeaGod = true;
            }
            if (Player.CharacterData_Temp.WaterElementBonus < 0.6f && IsWeaponsSeaGod)
            {
                IsWeaponsSeaGod = false;
            }
        }
    }
    private void OnDangerousWeapons()
    {
        if (Player.CharacterData_Temp.DangerousWeapons)
        {
            if (Player.CharacterData_Temp.DangerousBulletBonus >= 4f && !IsDangerousWeapons)
            {
                IsDangerousWeapons = true;
            }
            if (Player.CharacterData_Temp.DangerousBulletBonus < 4f && IsDangerousWeapons)
            {
                IsDangerousWeapons = false;
            }
        }
    }
    private void OnGlidingWaterSurface()
    {
        if (Player.CharacterData_Temp.GlidingWaterSurface)
        {
            if (Player.CharacterData_Temp.WaterElementBonus >= 0.3f && !IsGlidingWaterSurface)
            {
                IsGlidingWaterSurface = true;
                Player.CharacterData_Temp.SpeedRate += 0.1f;
            }
            if (Player.CharacterData_Temp.WaterElementBonus < 0.3f && IsGlidingWaterSurface)
            {
                IsGlidingWaterSurface = false;
                Player.CharacterData_Temp.SpeedRate -= 0.1f;
            }
        }
    }
    private void OnSpiderSense()
    {
        if (Player.CharacterData_Temp.SpiderSense)
        {
            if (GameManager.Instance.BossStats.gameObject.GetComponent<Dangerous>().CurrentDangerousCount > 0 && !IsSpiderSense)
            {
                IsSpiderSense = true;
                Player.CharacterData_Temp.AttackBonus += 0.2f;
                Player.CharacterData_Temp.CriticalDamageRate += 0.1f;
                Player.CharacterData_Temp.AttackRate -= 0.1f;
            }
            if (GameManager.Instance.BossStats.gameObject.GetComponent<Dangerous>().CurrentDangerousCount == 0 && IsSpiderSense)
            {
                IsSpiderSense = false;
                Player.CharacterData_Temp.AttackBonus -= 0.2f;
                Player.CharacterData_Temp.CriticalDamageRate -= 0.1f;
                Player.CharacterData_Temp.AttackRate += 0.1f;
            }
        }
    }
}
