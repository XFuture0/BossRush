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
    public BossSkillList SkillList;
    private Vector3 PlayerPosition;
    private CharacterStats Boss;
    private bool HaveSkill;
    [Header("技能物品")]
    public GameObject Shootball;
    public GameObject laser;
    [Header("技能列表")]
    public bool ShootBall;
    public bool Laser;
    [Header("技能效果")]
    private bool StartLaser;
    public float LaserSpeed;
    private float LaserZ;
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
    }
    private void Start()
    {
        Invoke("DestoryIng", 0.01f);
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate; 
    }
    private void OnEnable()
    {
        rb.gravityScale = Settings.BossGravity;
        IsSkill = false;
        WalkTime_Count = 3;
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
                if (SkillLevel < SkillList.BossSkills[i].SkillProbability)
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
                    StartCoroutine(UseShootBall());
                    break;
                case SkillType.Laser:
                    Laser = true;
                    StartCoroutine(UseLaser());
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
        laser.SetActive(true);
        laser.transform.eulerAngles = new Vector3(0, 0, 0);
        LaserZ = 0;
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
}
