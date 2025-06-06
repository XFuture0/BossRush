using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class BossController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BossCheck Check;
    private BossAnim anim;
    private bool IsSkill;
    public bool IsStopBoss;
    private bool NoMove;
    public bool IsJump;
    private bool IsCollideLeft;
    private float SkillProbability;
    private SkillType Skill;
    private int SkillLevel;
    public BossState State;
    public BossSkillList SkillList;
    private Vector3 PlayerPosition;
    private CharacterStats Boss;
    private bool HaveSkill;
    private delegate IEnumerator skill();
    private skill UseSkill;
    [Header("广播")]
    public VoidEventSO ClosePlatformBoxEvent;
    [Header("事件监听")]
    public Vector3EventSO ChangeBossSkillPosition;
    [Header("技能物品")]
    public Vector3 FlyPosition;
    public GameObject Shootball;
    public GameObject laser;
    public Transform LaserBox;
    public GameObject CollideForward;
    public GameObject FissueBox;
    public GameObject BossArmy;
    public CharacterData BoostBossArmy;
    public GameObject BossArmyBox;
    public Transform TridentBox;
    public GameObject trident;
    public GameObject Alltrident;
    [Header("技能列表")]
    private SkillType LastSkill;
    public bool ShootBall;
    public bool Laser;
    public bool GroundFissue;
    public bool Collide;
    public bool SummonArmy;
    public bool Trident;
    [Header("技能效果")]
    private bool IsGroundFissue;
    private bool IsCollide5;
    private int TridentCount;
    [Header("技能计时器")]
    public float BaseSkillTime;
    private float SkillTime_Count;
    [Header("行走计时器")]
    private float WalkTime_Count = -2;
    [Header("自然回血计时器")]
    private float RebornTimeCount = -2;
    private void Awake()
    {
        IsStopBoss = true;
        rb = GetComponent<Rigidbody2D>();
        Check = GetComponent<BossCheck>();
        Boss = GetComponent<CharacterStats>();
        anim = GetComponent<BossAnim>();
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
        IsJump = false;
        WalkTime_Count = 3;
        ChangeBossSkillPosition.OnVector3EventRaised += OnChangeBossSkillPosition;
    }
    private void OnDisable()
    {
        for(int i = 0; i < FissueBox.transform.childCount; i++)
        {
            FissueBox.transform.GetChild(i).gameObject.SetActive(false);
        }
        Alltrident.SetActive(false);
        ChangeBossSkillPosition.OnVector3EventRaised -= OnChangeBossSkillPosition;
        StopAllCoroutines();
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
        if (SkillTime_Count < 0 && !IsSkill && !IsStopBoss && !IsJump)
        {
            IsSkill = true;
            ChangeSkill();
        }
        PlayerPosition = GameManager.Instance.PlayerStats.gameObject.transform.position;
        Reborn();
        if (IsGroundFissue)
        {
            CheckGroundFissue();
        }
        OnJump();
    }
    private void Reborn()
    {
        if(RebornTimeCount > -1)
        {
            RebornTimeCount -= Time.deltaTime;
        }
        if (RebornTimeCount <= 0)
        {
            Boss.CharacterData_Temp.NowHealth += Boss.CharacterData_Temp.AutoHealCount;
            RebornTimeCount = 1;
        }
    }
    private void ChangeSkill()
    {
        for (int i = 0;i < SkillList.BossSkills.Count; i++)
        {
            if (SkillList.BossSkills[i].IsOpen)
            {
                SkillProbability = UnityEngine.Random.Range(0f, 1f);
                if (SkillProbability < SkillList.BossSkills[i].SkillProbability && (SkillList.BossSkills[i].State == State || SkillList.BossSkills[i].State == BossState.ALL) && (SkillList.BossSkills[i].Type != LastSkill || GameManager.Instance.PlayerData.CurrentRoomCount == 1))
                {
                    Skill = SkillList.BossSkills[i].Type;
                    SkillLevel = SkillList.BossSkills[i].SkillLevel;
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
                    UseShootBall(SkillLevel);
                    break;
                case SkillType.Laser:
                    Laser = true;
                    LastSkill = Skill;
                    UseLaser(SkillLevel);
                    break;
                case SkillType.GroundFissue:
                    GroundFissue = true;
                    LastSkill = Skill;
                    UseGroundFissue(SkillLevel);
                    break;
                case SkillType.Collide:
                    Collide = true;
                    LastSkill = Skill;
                    UseCollide(SkillLevel);
                    break;
                case SkillType.SummonArmy:
                    SummonArmy = true;
                    LastSkill = Skill;
                    UseSummonArmy(SkillLevel);
                    break;
                case SkillType.Trident:
                    Trident = true;
                    LastSkill = Skill;
                    UseTrident(SkillLevel);
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
        if(WalkTime_Count > -1 && !IsStopBoss && !IsSkill && !NoMove && !IsJump)
        {
            WalkTime_Count -= Time.deltaTime;
        }
        if(WalkTime_Count <= 0 && !IsStopBoss && !IsSkill && !NoMove && !IsJump)
        {
            WalkTime_Count = UnityEngine.Random.Range(1f, 3f);
            Walk();
        }
    }
    private void Walk()
    {
        if (GameManager.Instance.Player().WoundTearing)
        {
            Boss.CharacterData_Temp.NowHealth -= gameObject.GetComponent<Vulnerability>().VulnerabilityCount * 1;
        }
        var ForceRotation = new Vector2(0, 0);
        var RealSpeed = Boss.CharacterData_Temp.Speed * Boss.CharacterData_Temp.SpeedRate;
        var Walkspeed = UnityEngine.Random.Range(RealSpeed * 1.2f, RealSpeed * 1.7f);
        if (GameManager.Instance.PlayerStats.gameObject.transform.position.x >= transform.position.x)//在右边
        {
            ForceRotation = new Vector2(1 * Walkspeed, 1 * Walkspeed * 2f);
        }
        if (GameManager.Instance.PlayerStats.gameObject.transform.position.x < transform.position.x)//在左边
        {
            ForceRotation = new Vector2(-1 * Walkspeed, 1 * Walkspeed * 2f);
        }
        if (!IsJump)
        {
            IsJump = true;
            anim.OnJumpUp();
        }
        rb.AddForce(ForceRotation, ForceMode2D.Impulse);
    }
    private void OnJump()
    {
        if(IsJump && rb.velocity.y <= 0)
        {
            anim.OnJumpDown();
            rb.gravityScale = 15f;
            if (rb.velocity == Vector2.zero)
            {
                IsJump = false;
                StartCoroutine(OnJumpEnd());
            }
        }
    }
    private IEnumerator OnJumpEnd()
    {
        anim.OnJumpOnGround();
        yield return new WaitForSeconds(0.3f);
        anim.OnJumpEnd();
        rb.gravityScale = Settings.BossGravity;
    }
    private void OnChangeBossSkillPosition(Vector3 RoomPosition)
    {
        LaserBox.transform.position = RoomPosition + new Vector3(-14.88f, 11.75f, 0);
        FlyPosition = RoomPosition + new Vector3(-14.88f, 11.75f, 0);
        Alltrident.transform.position = RoomPosition + new Vector3(-14.8f, 1.03f, 0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameManager.Instance.Attack(gameObject.GetComponent<CharacterStats>(),other.GetComponent<CharacterStats>());
        }
        if(other.transform.parent != null)
        {
            if (other.transform.parent.tag == "Platform" && IsCollide5)
            {
                other.gameObject.SetActive(false);
                StartCoroutine(RebornGameObject(other));
            }
        }
    }
    private void BossDead()//在动画中使用
    {
        GameManager.Instance.BossDead();
    }
    private IEnumerator RebornGameObject(Collider2D other)
    {
        yield return new WaitForSeconds(1.5f);
        other.gameObject.SetActive(true);
    }
    private void BossFly()
    {
        transform.position = FlyPosition;
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
    private void UseShootBall(int level)
    {
        switch (level)
        {
            case 1:
                UseSkill = UseShootBall_1;
                break;
            case 2:
                UseSkill = UseShootBall_2;
                break;
            case 3:
                UseSkill = UseShootBall_3;
                break;
            case 4:
                UseSkill = UseShootBall_4;
                break;
            case 5:
                UseSkill = UseShootBall_5;
                break;
        }
        StartCoroutine(UseSkill());
    }
    private void UseLaser(int level)
    {
        switch (level)
        {
            case 1:
                UseSkill = UseLaser_1;
                break;
            case 2:
                UseSkill = UseLaser_2;
                break;
            case 3:
                UseSkill = UseLaser_3;
                break;
            case 4:
                UseSkill = UseLaser_4;
                break;
            case 5:
                UseSkill = UseLaser_5;
                break;
        }
        StartCoroutine(UseSkill());
    }
    private void UseGroundFissue(int level)
    {
        switch (level)
        {
            case 1:
                UseSkill = EndGroundFissue_1;
                break;
            case 2:
                UseSkill = EndGroundFissue_2;
                break;
            case 3:
                UseSkill = EndGroundFissue_3;
                break;
            case 4:
                UseSkill = EndGroundFissue_4;
                break;
            case 5:
                UseSkill = EndGroundFissue_5;
                break;
        }
        StartCoroutine(UseGroundFissue());
    }
    private void UseCollide(int level)
    {
        switch (level)
        {
            case 1:
                UseSkill = UseCollide_1;
                break;
            case 2:
                UseSkill = UseCollide_2;
                break;
            case 3:
                UseSkill = UseCollide_3;
                break;
            case 4:
                UseSkill = UseCollide_4;
                break;
            case 5:
                UseSkill = UseCollide_5;
                break;
        }
        StartCoroutine(UseSkill());
    }
    private void UseSummonArmy(int level)
    {
        switch (level)
        {
            case 1:
                UseSkill = UseSummonArmy_1;
                break;
            case 2:
                UseSkill = UseSummonArmy_2;
                break;
            case 3:
                UseSkill = UseSummonArmy_3;
                break;
            case 4:
                UseSkill = UseSummonArmy_4;
                break;
            case 5:
                UseSkill = UseSummonArmy_5;
                break;
        }
        StartCoroutine(UseSkill());
    }
    private void UseTrident(int level)
    {
        switch (level)
        {
            case 1:
                UseSkill = UseTrident_1;
                break;
            case 2:
                UseSkill = UseTrident_2;
                break;
            case 3:
                UseSkill = UseTrident_3;
                break;
            case 4:
                UseSkill = UseTrident_4;
                break;
            case 5:
                UseSkill = UseTrident_5;
                break;
        }
        StartCoroutine(UseSkill());
    }
    private IEnumerator UseShootBall_1()
    {
        var SetShootBall = transform.position + new Vector3(0, 2.5f, 0);
        var NewBall = Instantiate(Shootball, SetShootBall, Quaternion.identity);
        NewBall.GetComponent<ShootBall>().IsLevel1 = true;
        yield return new WaitForSeconds(1);
        ShootBall = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseShootBall_2()
    {
        var SetShootBall = transform.position + new Vector3(0, 2.5f, 0);
        var NewBall = Instantiate(Shootball, SetShootBall, Quaternion.identity);
        NewBall.GetComponent<ShootBall>().IsLevel2 = true;
        yield return new WaitForSeconds(1f);
        ShootBall = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseShootBall_3()
    {
        for(int i = 0;i < 3; i++)
        {
            var SetShootBall = transform.position + new Vector3(0, 2.5f, 0);
            var NewBall = Instantiate(Shootball, SetShootBall, Quaternion.identity);
            NewBall.GetComponent<ShootBall>().IsLevel2 = true;
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1f);
        ShootBall = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseShootBall_4()
    {
        for (int i = 0; i < 3; i++)
        {
            var SetShootBall = transform.position + new Vector3(0, 2.5f, 0);
            var NewBall = Instantiate(Shootball, SetShootBall, Quaternion.identity);
            NewBall.GetComponent<ShootBall>().IsLevel4 = true;
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1f);
        ShootBall = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseShootBall_5()
    {
        for (int i = 0; i < 5; i++)
        {
            var SetShootBall = transform.position + new Vector3(0, 2.5f, 0);
            var NewBall = Instantiate(Shootball, SetShootBall, Quaternion.identity);
            NewBall.GetComponent<ShootBall>().IsLevel4 = true;
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1f);
        ShootBall = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseLaser_1()
    {
        if(rb.gravityScale != 0)
        {
            BossFly();
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        var NewLaser = Instantiate(laser, LaserBox);
        NewLaser.GetComponent<Laser>().LaserSpeed = 0.2f;
        yield return new WaitForSeconds(1f);
        Laser = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseLaser_2()
    {
        if (rb.gravityScale != 0)
        {
            BossFly();
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        var NewLaser = Instantiate(laser, LaserBox);
        NewLaser.GetComponent<Laser>().LaserSpeed = 0.4f;
        yield return new WaitForSeconds(1);
        Laser = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseLaser_3()
    {
        if (rb.gravityScale != 0)
        {
            BossFly();
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        var NewLaser1 = Instantiate(laser, LaserBox);
        NewLaser1.GetComponent<Laser>().LaserSpeed = 0.4f;
        var NewLaser2 = Instantiate(laser, LaserBox);
        NewLaser2.transform.localScale = new Vector3(-2, 2, 1);
        NewLaser2.GetComponent<Laser>().LaserSpeed = 0.4f;
        yield return new WaitForSeconds(1);
        Laser = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseLaser_4()
    {
        if (rb.gravityScale != 0)
        {
            BossFly();
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        var NewLaser1 = Instantiate(laser, LaserBox);
        NewLaser1.GetComponent<Laser>().LaserSpeed = 0.4f;
        var NewLaser2 = Instantiate(laser, LaserBox);
        NewLaser2.transform.localScale = new Vector3(-2, 2, 1);
        NewLaser2.GetComponent<Laser>().LaserSpeed = 0.4f;
        NewLaser1.GetComponent<LineRenderer>().startWidth = 2f;
        NewLaser2.GetComponent<LineRenderer>().startWidth = 2f;
        yield return new WaitForSeconds(1);
        Laser = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseLaser_5()
    {
        if (rb.gravityScale != 0)
        {
            BossFly();
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.5f);
        var NewLaser1 = Instantiate(laser, LaserBox);
        NewLaser1.GetComponent<Laser>().LaserSpeed = 0.4f;
        var NewLaser2 = Instantiate(laser, LaserBox);
        NewLaser2.transform.localScale = new Vector3(-2, 2, 1);
        NewLaser2.GetComponent<Laser>().LaserSpeed = 0.4f;
        NewLaser1.GetComponent<LineRenderer>().startWidth = 2f;
        NewLaser2.GetComponent<LineRenderer>().startWidth = 2f;
        NewLaser1.GetComponent<Laser>().IsLevel5 = true;
        NewLaser2.GetComponent<Laser>().IsLevel5 = true;
        yield return new WaitForSeconds(1);
        Laser = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseGroundFissue()
    {
        if (rb.gravityScale != 0)
        {
            BossFly();
            yield return new WaitForSeconds(0.5f);
        }
        EndFly();
        IsGroundFissue = true;
        yield return null;
    }
    private void CheckGroundFissue()
    {
        if (Check.IsGround)
        {
            NoMove = false;
            WalkTime_Count = UnityEngine.Random.Range(1f, 3f);
            IsGroundFissue = false;
            StartCoroutine(UseSkill());
        }
    }
    public IEnumerator EndGroundFissue_1()
    {
        FissueBox.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        FissueBox.transform.GetChild(0).gameObject.SetActive(false);
        GroundFissue = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    public IEnumerator EndGroundFissue_2()
    {
        FissueBox.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        FissueBox.transform.GetChild(0).gameObject.SetActive(false);
        GroundFissue = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    public IEnumerator EndGroundFissue_3()
    {
        FissueBox.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        FissueBox.transform.GetChild(1).gameObject.SetActive(false);
        GroundFissue = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    public IEnumerator EndGroundFissue_4()
    {
        FissueBox.transform.GetChild(1).gameObject.SetActive(true);
        ClosePlatformBoxEvent.RaiseEvent();
        yield return new WaitForSeconds(1.5f);
        FissueBox.transform.GetChild(1).gameObject.SetActive(false);
        GroundFissue = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    public IEnumerator EndGroundFissue_5()
    {
        FissueBox.transform.GetChild(2).gameObject.SetActive(true);
        ClosePlatformBoxEvent.RaiseEvent();
        yield return new WaitForSeconds(1.5f);
        FissueBox.transform.GetChild(2).gameObject.SetActive(false);
        GroundFissue = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseCollide_1()
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
        CollideForward.SetActive(true);
        if (PlayerPosition.x - transform.position.x >= 0)//右
        {
            IsCollideLeft = false;
            CollideForward.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (PlayerPosition.x - transform.position.x < 0)//左
        {
            IsCollideLeft = true;
            CollideForward.transform.localScale = new Vector3(-1,1, 1);
        }
        yield return new WaitForSeconds(0.8f);
        CollideForward.transform.GetChild(0).gameObject.SetActive(true);
        if(!IsCollideLeft)//右
        {
            rb.AddForce(Vector2.right * 20, ForceMode2D.Impulse);
        }
        else if (IsCollideLeft)//左
        {
            rb.AddForce(Vector2.left * 20, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(0.2f);
        rb.velocity = Vector2.zero;
        CollideForward.transform.GetChild(0).gameObject.SetActive(false);
        CollideForward.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        rb.gravityScale = Settings.BossGravity;
        Collide = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseCollide_2()
    {
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        for(int i = 0; i < 2; i++)
        {
            var NewPosition = (Vector2)PlayerPosition + new Vector2(-4.53f, 1.25f);
            if (!Physics2D.OverlapArea(Check.LeftUpPo + NewPosition, Check.RightDownPo + NewPosition, Check.Ground))
            {
                transform.position = NewPosition;
            }
            else if (!Physics2D.OverlapArea(Check.LeftUpPo + NewPosition + new Vector2(9.06f, 0), Check.RightDownPo + NewPosition + new Vector2(9.06f, 0), Check.Ground))
            {
                transform.position = NewPosition + new Vector2(9.06f, 0);
            }
            CollideForward.SetActive(true);
            if (PlayerPosition.x - transform.position.x >= 0)//右
            {
                IsCollideLeft = false;
                CollideForward.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (PlayerPosition.x - transform.position.x < 0)//左
            {
                IsCollideLeft = true;
                CollideForward.transform.localScale = new Vector3(-1, 1, 1);
            }
            yield return new WaitForSeconds(0.8f);
            if (!IsCollideLeft)//右
            {
                rb.AddForce(Vector2.right * 30, ForceMode2D.Impulse);
            }
            else if (IsCollideLeft)//左
            {
                rb.AddForce(Vector2.left * 30, ForceMode2D.Impulse);
            }
            CollideForward.transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            rb.velocity = Vector2.zero;
            CollideForward.transform.GetChild(0).gameObject.SetActive(false);
            CollideForward.SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }
        rb.gravityScale = Settings.BossGravity;
        Collide = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseCollide_3()
    {
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        for (int i = 0; i < 2; i++)
        {
            var NewPosition = (Vector2)PlayerPosition + new Vector2(-4.53f, 1.25f);
            if (!Physics2D.OverlapArea(Check.LeftUpPo + NewPosition, Check.RightDownPo + NewPosition, Check.Ground))
            {
                transform.position = NewPosition;
            }
            else if (!Physics2D.OverlapArea(Check.LeftUpPo + NewPosition + new Vector2(9.06f, 0), Check.RightDownPo + NewPosition + new Vector2(9.06f, 0), Check.Ground))
            {
                transform.position = NewPosition + new Vector2(9.06f, 0);
            }
            CollideForward.SetActive(true);
            if (PlayerPosition.x - transform.position.x >= 0)//右
            {
                IsCollideLeft = false;
                CollideForward.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (PlayerPosition.x - transform.position.x < 0)//左
            {
                IsCollideLeft = true;
                CollideForward.transform.localScale = new Vector3(-1, 1, 1);
            }
            yield return new WaitForSeconds(0.6f);
            if (!IsCollideLeft)//右
            {
                rb.AddForce(Vector2.right * 30, ForceMode2D.Impulse);
            }
            else if (IsCollideLeft)//左
            {
                rb.AddForce(Vector2.left * 30, ForceMode2D.Impulse);
            }
            CollideForward.transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            rb.velocity = Vector2.zero;
            CollideForward.transform.GetChild(0).gameObject.SetActive(false);
            CollideForward.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
        rb.gravityScale = Settings.BossGravity;
        Collide = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseCollide_4()
    {
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        for (int i = 0; i < 3; i++)
        {
            var NewPosition = (Vector2)PlayerPosition + new Vector2(-4.53f, 1.25f);
            if (!Physics2D.OverlapArea(Check.LeftUpPo + NewPosition, Check.RightDownPo + NewPosition, Check.Ground))
            {
                transform.position = NewPosition;
            }
            else if (!Physics2D.OverlapArea(Check.LeftUpPo + NewPosition + new Vector2(9.06f, 0), Check.RightDownPo + NewPosition + new Vector2(9.06f, 0), Check.Ground))
            {
                transform.position = NewPosition + new Vector2(9.06f, 0);
            }
            CollideForward.SetActive(true);
            if (PlayerPosition.x - transform.position.x >= 0)//右
            {
                IsCollideLeft = false;
                CollideForward.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (PlayerPosition.x - transform.position.x < 0)//左
            {
                IsCollideLeft = true;
                CollideForward.transform.localScale = new Vector3(-1, 1, 1);
            }
            yield return new WaitForSeconds(0.6f);
            if (!IsCollideLeft)//右
            {
                rb.AddForce(Vector2.right * 30, ForceMode2D.Impulse);
            }
            else if (IsCollideLeft)//左
            {
                rb.AddForce(Vector2.left * 30, ForceMode2D.Impulse);
            }
            CollideForward.transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            rb.velocity = Vector2.zero;
            CollideForward.transform.GetChild(0).gameObject.SetActive(false);
            CollideForward.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
        rb.gravityScale = Settings.BossGravity;
        Collide = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseCollide_5()
    {
        IsCollide5 = true;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        for (int i = 0; i < 3; i++)
        {
            var NewPosition = (Vector2)PlayerPosition + new Vector2(-4.53f, 1.25f);
            if (!Physics2D.OverlapArea(Check.LeftUpPo + NewPosition, Check.RightDownPo + NewPosition, Check.Ground))
            {
                transform.position = NewPosition;
            }
            else if (!Physics2D.OverlapArea(Check.LeftUpPo + NewPosition + new Vector2(9.06f, 0), Check.RightDownPo + NewPosition + new Vector2(9.06f, 0), Check.Ground))
            {
                transform.position = NewPosition + new Vector2(9.06f, 0);
            }
            CollideForward.SetActive(true);
            if (PlayerPosition.x - transform.position.x >= 0)//右
            {
                IsCollideLeft = false;
                CollideForward.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (PlayerPosition.x - transform.position.x < 0)//左
            {
                IsCollideLeft = true;
                CollideForward.transform.localScale = new Vector3(-1, 1, 1);
            }
            yield return new WaitForSeconds(0.6f);
            if (!IsCollideLeft)//右
            {
                rb.AddForce(Vector2.right * 30, ForceMode2D.Impulse);
            }
            else if (IsCollideLeft)//左
            {
                rb.AddForce(Vector2.left * 30, ForceMode2D.Impulse);
            }
            CollideForward.transform.GetChild(0).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            rb.velocity = Vector2.zero;
            CollideForward.transform.GetChild(0).gameObject.SetActive(false);
            CollideForward.SetActive(false);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
        rb.gravityScale = Settings.BossGravity;
        Collide = false;
        IsCollide5 = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseSummonArmy_1()
    {
        for (int i = 0; i < 1; i++)
        {
            if (BossArmyBox.transform.childCount < 5)
            {
                if (!Physics2D.OverlapArea(Check.LeftUpPo + (Vector2)transform.position + new Vector2(-3, 0), Check.RightDownPo + (Vector2)transform.position + new Vector2(-3, 0), Check.Ground))
                {
                    var NewArmy = Instantiate(BossArmy, transform.position + new Vector3(-3, 0, 0), Quaternion.identity, BossArmyBox.transform);
                    NewArmy.GetComponent<SpriteRenderer>().color = ColorManager.Instance.UpdateColor(2);
                }
                else if (!Physics2D.OverlapArea(Check.LeftUpPo + (Vector2)transform.position + new Vector2(3, 0), Check.RightDownPo + (Vector2)transform.position + new Vector2(3, 0), Check.Ground))
                {
                    var NewArmy = Instantiate(BossArmy, transform.position + new Vector3(3, 0, 0), Quaternion.identity, BossArmyBox.transform);
                    NewArmy.GetComponent<SpriteRenderer>().color = ColorManager.Instance.UpdateColor(2);
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
        SummonArmy = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseSummonArmy_2()
    {
        for(int i = 0; i < 3; i++)
        {
            if (BossArmyBox.transform.childCount < 5)
            {
                if (!Physics2D.OverlapArea(Check.LeftUpPo + (Vector2)transform.position + new Vector2(-3, 0), Check.RightDownPo + (Vector2)transform.position + new Vector2(-3, 0), Check.Ground))
                {
                    var NewArmy = Instantiate(BossArmy, transform.position + new Vector3(-3, 0, 0), Quaternion.identity, BossArmyBox.transform);
                    NewArmy.GetComponent<SpriteRenderer>().color = ColorManager.Instance.UpdateColor(2);
                }
                else if (!Physics2D.OverlapArea(Check.LeftUpPo + (Vector2)transform.position + new Vector2(3, 0), Check.RightDownPo + (Vector2)transform.position + new Vector2(3, 0), Check.Ground))
                {
                    var NewArmy = Instantiate(BossArmy, transform.position + new Vector3(3, 0, 0), Quaternion.identity, BossArmyBox.transform);
                    NewArmy.GetComponent<SpriteRenderer>().color = ColorManager.Instance.UpdateColor(2);
                }
                yield return new WaitForSeconds(0.2f);
            }
        }
        SummonArmy = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseSummonArmy_3()
    {
        for (int i = 0; i < 3; i++)
        {
            if (BossArmyBox.transform.childCount < 5)
            {
                if (!Physics2D.OverlapArea(Check.LeftUpPo + (Vector2)transform.position + new Vector2(-3, 0), Check.RightDownPo + (Vector2)transform.position + new Vector2(-3, 0), Check.Ground))
                {
                    var NewArmy = Instantiate(BossArmy, transform.position + new Vector3(-3, 0, 0), Quaternion.identity, BossArmyBox.transform);
                    NewArmy.GetComponent<SpriteRenderer>().color = ColorManager.Instance.UpdateColor(2);
                    NewArmy.GetComponent<CharacterStats>().CharacterData_Temp = Instantiate(BoostBossArmy);
                }
                else if (!Physics2D.OverlapArea(Check.LeftUpPo + (Vector2)transform.position + new Vector2(3, 0), Check.RightDownPo + (Vector2)transform.position + new Vector2(3, 0), Check.Ground))
                {
                    var NewArmy = Instantiate(BossArmy, transform.position + new Vector3(3, 0, 0), Quaternion.identity, BossArmyBox.transform);
                    NewArmy.GetComponent<SpriteRenderer>().color = ColorManager.Instance.UpdateColor(2);
                    NewArmy.GetComponent<CharacterStats>().CharacterData_Temp = Instantiate(BoostBossArmy);
                }
                yield return new WaitForSeconds(0.2f);
            }
        }
        SummonArmy = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseSummonArmy_4()
    {
        for (int i = 0; i < 3; i++)
        {
            if (BossArmyBox.transform.childCount < 5)
            {
                if (!Physics2D.OverlapArea(Check.LeftUpPo + (Vector2)transform.position + new Vector2(-3, 0), Check.RightDownPo + (Vector2)transform.position + new Vector2(-3, 0), Check.Ground))
                {
                    var NewArmy = Instantiate(BossArmy, transform.position + new Vector3(-3, 0, 0), Quaternion.identity, BossArmyBox.transform);
                    NewArmy.GetComponent<SpriteRenderer>().color = ColorManager.Instance.UpdateColor(2);
                    NewArmy.GetComponent<CharacterStats>().CharacterData_Temp = Instantiate(BoostBossArmy);
                    NewArmy.GetComponent<BossArmy>().IsLevel4 = true;
                }
                else if (!Physics2D.OverlapArea(Check.LeftUpPo + (Vector2)transform.position + new Vector2(3, 0), Check.RightDownPo + (Vector2)transform.position + new Vector2(3, 0), Check.Ground))
                {
                    var NewArmy = Instantiate(BossArmy, transform.position + new Vector3(3, 0, 0), Quaternion.identity, BossArmyBox.transform);
                    NewArmy.GetComponent<SpriteRenderer>().color = ColorManager.Instance.UpdateColor(2);
                    NewArmy.GetComponent<CharacterStats>().CharacterData_Temp = Instantiate(BoostBossArmy);
                    NewArmy.GetComponent<BossArmy>().IsLevel4 = true;
                }
                yield return new WaitForSeconds(0.2f);
            }
        }
        SummonArmy = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseSummonArmy_5()
    {
        for (int i = 0; i < 3; i++)
        {
            if (BossArmyBox.transform.childCount < 5)
            {
                if (!Physics2D.OverlapArea(Check.LeftUpPo + (Vector2)transform.position + new Vector2(-3, 0), Check.RightDownPo + (Vector2)transform.position + new Vector2(-3, 0), Check.Ground))
                {
                    var NewArmy = Instantiate(BossArmy, transform.position + new Vector3(-3, 0, 0), Quaternion.identity, BossArmyBox.transform);
                    NewArmy.GetComponent<SpriteRenderer>().color = ColorManager.Instance.UpdateColor(2);
                    NewArmy.GetComponent<CharacterStats>().CharacterData_Temp = Instantiate(BoostBossArmy);
                    NewArmy.GetComponent<BossArmy>().IsLevel4 = true;
                    NewArmy.GetComponent<BossArmy>().IsLevel5 = true;
                }
                else if (!Physics2D.OverlapArea(Check.LeftUpPo + (Vector2)transform.position + new Vector2(3, 0), Check.RightDownPo + (Vector2)transform.position + new Vector2(3, 0), Check.Ground))
                {
                    var NewArmy = Instantiate(BossArmy, transform.position + new Vector3(3, 0, 0), Quaternion.identity, BossArmyBox.transform);
                    NewArmy.GetComponent<SpriteRenderer>().color = ColorManager.Instance.UpdateColor(2);
                    NewArmy.GetComponent<CharacterStats>().CharacterData_Temp = Instantiate(BoostBossArmy);
                    NewArmy.GetComponent<BossArmy>().IsLevel4 = true;
                    NewArmy.GetComponent<BossArmy>().IsLevel5 = true;
                }
                yield return new WaitForSeconds(0.2f);
            }
        }
        SummonArmy = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseTrident_1()
    {
        TridentCount = 3;
        for(int i = 0; i < TridentCount; i++)
        {
            var NewTrident = Instantiate(trident,transform.position,Quaternion.identity,TridentBox);
            NewTrident.gameObject.transform.position = GameManager.Instance.PlayerStats.gameObject.transform.position + new Vector3(0,-0.5f,0);
            yield return new WaitForSeconds(0.5f);
        }
        Trident = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseTrident_2()
    {
        TridentCount = 5;
        for (int i = 0; i < TridentCount; i++)
        {
            var NewTrident = Instantiate(trident, transform.position, Quaternion.identity, TridentBox);
            NewTrident.gameObject.transform.position = GameManager.Instance.PlayerStats.gameObject.transform.position + new Vector3(0, -0.5f, 0);
            yield return new WaitForSeconds(0.5f);
        }
        Trident = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseTrident_3()
    {
        TridentCount = 5;
        for (int i = 0; i < TridentCount; i++)
        {
            var NewTrident = Instantiate(trident, transform.position, Quaternion.identity, TridentBox);
            NewTrident.GetComponent<Trident>().WaitTime = 0.5f;
            NewTrident.gameObject.transform.position = GameManager.Instance.PlayerStats.gameObject.transform.position + new Vector3(0, -0.5f, 0);
            yield return new WaitForSeconds(0.5f);
        }
        Trident = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseTrident_4()
    {
        Alltrident.SetActive(true);
        TridentCount = 5;
        for (int i = 0; i < TridentCount; i++)
        {
            var NewTrident = Instantiate(trident, transform.position, Quaternion.identity, TridentBox);
            NewTrident.GetComponent<Trident>().WaitTime = 0.5f;
            NewTrident.gameObject.transform.position = GameManager.Instance.PlayerStats.gameObject.transform.position + new Vector3(0, -0.5f, 0);
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1.5f);
        Alltrident.SetActive(false);
        Trident = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
    }
    private IEnumerator UseTrident_5()
    {
        Alltrident.SetActive(true);
        TridentCount = 5;
        for (int i = 0; i < TridentCount; i++)
        {
            var NewTrident = Instantiate(trident, transform.position, Quaternion.identity, TridentBox);
            NewTrident.GetComponent<Trident>().WaitTime = 0.5f;
            NewTrident.gameObject.transform.position = GameManager.Instance.PlayerStats.gameObject.transform.position + new Vector3(0, -0.5f, 0);
            yield return new WaitForSeconds(0.5f);
        }
        Trident = false;
        IsSkill = false;
        SkillTime_Count = BaseSkillTime * Boss.CharacterData_Temp.AttackRate;
        yield return new WaitForSeconds(5f);
        Alltrident.SetActive(false);
    }
}
