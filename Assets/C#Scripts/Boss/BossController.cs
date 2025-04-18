using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class BossController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BossCheck Check;
    private bool IsSkill;
    public bool IsStopBoss;
    private bool NoMove;
    private float SkillLevel;
    private SkillType Skill;
    private BossState State;
    public BossSkillList SkillList;
    private Vector3 PlayerPosition;
    private CharacterStats Boss;
    private bool HaveSkill;
    [Header("技能物品")]
    public GameObject Shootball;
    public GameObject laser;
    public GameObject FissueBox;
    public GameObject BossArmy;
    public Transform TridentBox;
    public Trident trident;
    [Header("技能列表")]
    private SkillType LastSkill;
    public bool ShootBall;
    public bool Laser;
    public bool GroundFissue;
    public bool Collide;
    public bool SummonArmy;
    public bool Trident;
    [Header("技能效果")]
    private bool StartLaser;
    public float LaserSpeed;
    private float LaserZ;
    private bool IsGroundFissue;
    public float CollideForce;
    [HideInInspector] public ObjectPool<Trident> TridentPool;
    private int TridentCount;
    [Header("技能计时器")]
    public float BaseSkillTime;
    private float SkillTime_Count;
    [Header("行走计时器")]
    private float WalkTime_Count = -2;
    [Header("自然回血计时器")]
    private float RebornTiemCount = -2;
    private void Awake()
    {
        IsStopBoss = true;
        rb = GetComponent<Rigidbody2D>();
        Check = GetComponent<BossCheck>();
        Boss = GetComponent<CharacterStats>();
        TridentPool = new ObjectPool<Trident>(trident);
        TridentPool.Box = TridentBox;
    }
    private void Start()
    {
        Invoke("DestoryIng", 0.01f);
    }
    private void OnEnable()
    {
        rb.gravityScale = Settings.BossGravity;
        State = BossState.Ground;
        SkillTime_Count = 2;
        IsSkill = false;
        NoMove = false;
        WalkTime_Count = 3;
    }
    private void OnDisable()
    {
        laser.SetActive(false);
        FissueBox.SetActive(false);
    }
    private void DestoryIng()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (SkillTime_Count > -2 && !IsStopBoss)
        {
            SkillTime_Count -= Time.deltaTime;
        }
        if (SkillTime_Count < 0 && !IsSkill && !IsStopBoss && (Check.IsGround || NoMove))
        {
            IsSkill = true;
            ChangeSkill();
        }
        PlayerPosition = GameManager.Instance.PlayerStats.gameObject.transform.position;
        Reborn();
        LaserRotation();
        if (IsGroundFissue)
        {
            CheckGroundFissue();
        }
    }
    private void Reborn()
    {
        if(RebornTiemCount > -1)
        {
            RebornTiemCount -= Time.deltaTime;
        }
        if (RebornTiemCount <= 0)
        {
            Boss.CharacterData_Temp.NowHealth += Boss.CharacterData_Temp.AutoHealCount;
            RebornTiemCount = Boss.CharacterData_Temp.AutoHealTime;
        }
    }
    private void ChangeSkill()
    {
        for (int i = 0;i < SkillList.BossSkills.Count; i++)
        {
            if (SkillList.BossSkills[i].IsOpen)
            {
                SkillLevel = UnityEngine.Random.Range(0f, 1f);
                if (SkillLevel < SkillList.BossSkills[i].SkillProbability && (SkillList.BossSkills[i].State == State || SkillList.BossSkills[i].State == BossState.ALL) && SkillList.BossSkills[i].Type != LastSkill)
                {
                    Skill = SkillList.BossSkills[i].Type;
                    HaveSkill = true;
                    break;
                }
            }
        }
        if (HaveSkill)
        {
            HaveSkill = false;
            switch (Skill)
            {
                case SkillType.ShootBall:
                    ShootBall = true;
                    LastSkill = Skill;
                    StartCoroutine(UseShootBall());
                    break;
                case SkillType.Laser:
                    Laser = true;
                    LastSkill = Skill;
                    StartCoroutine(UseLaser());
                    break;
                case SkillType.GroundFissue:
                    GroundFissue = true;
                    LastSkill = Skill;
                    StartCoroutine(UseGroundFissue());
                    break;
                case SkillType.Collide:
                    Collide = true;
                    LastSkill = Skill;
                    StartCoroutine(UseCollide());
                    break;
                case SkillType.SummonArmy:
                    SummonArmy = true;
                    LastSkill = Skill;
                    StartCoroutine(UseSummonArmy());
                    break;
                case SkillType.Trident:
                    Trident = true;
                    LastSkill = Skill;
                    StartCoroutine(UseTrident());
                    break;
                default:
                    break;
            }
        }
        else
        {
            HaveSkill = false;
            IsSkill = false;
            SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
        }
    }
    private void FixedUpdate()
    {
        CanWalk();
    }
    private void CanWalk()
    {
        if(WalkTime_Count > -1 && !IsStopBoss && !IsSkill && !NoMove)
        {
            WalkTime_Count -= Time.deltaTime;
        }
        if(WalkTime_Count <= 0 && !IsStopBoss && !IsSkill && !NoMove)
        {
            WalkTime_Count = UnityEngine.Random.Range(1f, 3f);
            Walk();
        }
    }
    private void Walk()
    {
        var ForceRotation = new Vector2(0, 0);
        var RealSpeed = Boss.CharacterData_Temp.Speed * Boss.CharacterData_Temp.SpeedRate;
        var Walkspeed = UnityEngine.Random.Range(RealSpeed * 0.8f, RealSpeed * 1.5f);
        if (GameManager.Instance.PlayerStats.gameObject.transform.position.x >= transform.position.x)//在右边
        {
            ForceRotation = new Vector2(1 * Walkspeed, 1 * Walkspeed * 2f);
        }
        if (GameManager.Instance.PlayerStats.gameObject.transform.position.x < transform.position.x)//在左边
        {
            ForceRotation = new Vector2(-1 * Walkspeed, 1 * Walkspeed * 2f);
        }
        rb.AddForce(ForceRotation, ForceMode2D.Impulse);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameManager.Instance.Attack(gameObject.GetComponent<CharacterStats>(),other.GetComponent<CharacterStats>());
        }
    }
    private void BossFly()
    {
        transform.position = new Vector3(-14.88f, 11.75f, 0);
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        NoMove = true;
        State = BossState.Flying;
    }
    private void EndFly()
    {
        rb.gravityScale = Settings.BossGravity;
        State = BossState.Ground;
    }
    private IEnumerator UseShootBall()
    {
        var SetShootBall = transform.position + new Vector3(0, 2.5f, 0);
        Instantiate(Shootball, SetShootBall, Quaternion.identity);
        yield return new WaitForSeconds(3);
        ShootBall = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseLaser()
    {
        if(rb.gravityScale != 0)
        {
            BossFly();
            yield return new WaitForSeconds(1);
        }
        yield return new WaitForSeconds(1);
        LaserZ = 0;
        laser.transform.eulerAngles = new Vector3(0, 0, 0);
        laser.SetActive(true);
        StartLaser = true;
        yield return new WaitForSeconds(5);
        laser.SetActive(false);
        StartLaser = false;
        Laser = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private void LaserRotation()
    {
        if (StartLaser)
        {
            LaserZ = Mathf.Lerp(LaserZ,200,LaserSpeed * Time.deltaTime);
            laser.transform.eulerAngles = new Vector3(0, 0, LaserZ);
        }
    }
    private IEnumerator UseGroundFissue()
    {
        if (rb.gravityScale != 0)
        {
            BossFly();
            yield return new WaitForSeconds(1);
        }
        EndFly();
        IsGroundFissue = true;
        yield return null;
    }
    private void CheckGroundFissue()
    {
        if (Check.IsGround)
        {
            IsGroundFissue = false;
            StartCoroutine(EndGroundFissue());
        }
    }
    public IEnumerator EndGroundFissue()
    {
        FissueBox.SetActive(true);
        yield return new WaitForSeconds(1);
        GroundFissue = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseCollide()
    {
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        var NewPosition = (Vector2)PlayerPosition + new Vector2(-4.53f,1.25f);
        if(!Physics2D.OverlapArea(Check.LeftUpPo + NewPosition,Check.RightDownPo + NewPosition, Check.Ground))
        {
            transform.position = NewPosition;
        }
        else if (!Physics2D.OverlapArea(Check.LeftUpPo + NewPosition + new Vector2(9.06f,0), Check.RightDownPo + NewPosition + new Vector2(9.06f, 0), Check.Ground))
        {
            transform.position = NewPosition + new Vector2(9.06f, 0);
        }
        yield return new WaitForSeconds(1);
        if(PlayerPosition.x - transform.position.x >= 0)//右
        {
            rb.AddForce(Vector2.right * CollideForce, ForceMode2D.Impulse);
        }
        else if (PlayerPosition.x - transform.position.x < 0)//左
        {
            rb.AddForce(Vector2.left * CollideForce, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(0.3f);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.3f);
        rb.gravityScale = Settings.BossGravity;
        Collide = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseSummonArmy()
    {
        yield return new WaitForSeconds(0.3f);
        if (!Physics2D.OverlapArea(Check.LeftUpPo + (Vector2)transform.position + new Vector2(-3,0), Check.RightDownPo + (Vector2)transform.position + new Vector2(-3, 0), Check.Ground))
        {
            var NewArmy = Instantiate(BossArmy,transform.position + new Vector3(-3,0,0),Quaternion.identity);
            NewArmy.GetComponent<SpriteRenderer>().color = ColorManager.Instance.UpdateColor(2);
        }
        else if (!Physics2D.OverlapArea(Check.LeftUpPo + (Vector2)transform.position + new Vector2(3, 0), Check.RightDownPo + (Vector2)transform.position + new Vector2(3, 0), Check.Ground))
        {
            var NewArmy = Instantiate(BossArmy, transform.position + new Vector3(3, 0, 0), Quaternion.identity);
            NewArmy.GetComponent<SpriteRenderer>().color = ColorManager.Instance.UpdateColor(2);
        }
        yield return new WaitForSeconds(0.3f);
        SummonArmy = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseTrident()
    {
        TridentCount = 3;
        for(int i = 0; i < TridentCount; i++)
        {
            var NewTrident = TridentPool.GetObject();
            NewTrident.gameObject.transform.position = GameManager.Instance.PlayerStats.gameObject.transform.position + new Vector3(0,-0.5f,0);
            yield return new WaitForSeconds(0.5f);
        }
        Trident = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
}
