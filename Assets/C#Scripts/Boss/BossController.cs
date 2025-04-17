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
    [Header("������Ʒ")]
    public GameObject Shootball;
    public GameObject laser;
    public GameObject FissueBox;
    public GameObject BossArmy;
    [Header("�����б�")]
    public bool ShootBall;
    public bool Laser;
    public bool GroundFissue;
    public bool Collide;
    public bool SummonArmy;
    [Header("����Ч��")]
    private bool StartLaser;
    public float LaserSpeed;
    private float LaserZ;
    private bool IsGroundFissue;
    public float CollideForce; 
    [Header("���ܼ�ʱ��")]
    public float BaseSkillTime;
    private float SkillTime_Count;
    [Header("���߼�ʱ��")]
    private float WalkTime_Count = -2;
    [Header("��Ȼ��Ѫ��ʱ��")]
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
        State = BossState.Ground;
        IsSkill = false;
        NoMove = false;
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
        if (IsGroundFissue)
        {
            EndGroundFissue();
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
                if (SkillLevel < SkillList.BossSkills[i].SkillProbability && (SkillList.BossSkills[i].State == State || SkillList.BossSkills[i].State == BossState.ALL))
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
                case SkillType.GroundFissue:
                    GroundFissue = true;
                    StartCoroutine(UseGroundFissue());
                    break;
                case SkillType.Collide:
                    Collide = true;
                    StartCoroutine(UseCollide());
                    break;
                case SkillType.SummonArmy:
                    SummonArmy = true;
                    StartCoroutine(UseSummonArmy());
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
        if (GameManager.Instance.PlayerStats.gameObject.transform.position.x >= transform.position.x)//���ұ�
        {
            ForceRotation = new Vector2(1 * Walkspeed, 1 * Walkspeed * 2f);
        }
        if (GameManager.Instance.PlayerStats.gameObject.transform.position.x < transform.position.x)//�����
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
    private void EndGroundFissue()
    {
        if (Check.IsGround)
        {
            IsGroundFissue = false;
            FissueBox.SetActive(true);
            GroundFissue = false;
            IsSkill = false;
            SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
        }
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
        if(PlayerPosition.x - transform.position.x >= 0)//��
        {
            rb.AddForce(Vector2.right * CollideForce, ForceMode2D.Impulse);
        }
        else if (PlayerPosition.x - transform.position.x < 0)//��
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
}
