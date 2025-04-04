using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BossController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BossCheck Check;
    private bool IsSkill;
    private float SkillLevel;
    private SkillType Skill;
    public BossSkillList SkillList;
    public BossShootList ShootList;
    public GameObject Impact;
    public GameObject BossShoot;
    public GameObject Laser;
    public GameObject BossArmy;
    private Vector3 PlayerPosition;
    [Header("技能列表")]
    public bool Collide;
    public bool Strike;
    public bool Strike_Impact;
    public bool Shoot;
    public bool Ground_Laser;
    public bool Sky_Laser;
    public bool Ground_Trident;
    public bool Sky_Trident;
    public bool Evoke_Army;
    [Header("技能效果")]
    public float Collide_Time;
    private float Collide_Time_Count;
    public float Collide_Speed;
    private float Collide_Speed_Count;
    private bool IsStrike_Up;
    private bool IsStrike_Down;
    public float JumpForce;
    public float Strike_JumpForwardSpeed;
    public float Strike_Impact_JumpForwardSpeed;
    private bool Strike_Impact_Down;
    private bool ShootDown;
    private bool Sky_LaserDown;
    private bool Ground_TridentDown;
    private bool Sky_TridentDown;
    [Header("技能计时器")]
    public float SkillTime;
    private float SkillTime_Count;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Check = GetComponent<BossCheck>();
    }
    private void Start()
    {
        SkillTime_Count = SkillTime;
    }
    private void Update()
    {
        if (SkillTime_Count > -2)
        {
            SkillTime_Count -= Time.deltaTime;
        }
        if (SkillTime_Count < 0 && !IsSkill)
        {
            IsSkill = true;
            ChangeSkill();
        }
        if (IsStrike_Down && Check.IsGround)
        {
            Strike_End();
        }
        if(Strike_Impact_Down && Check.IsGround)
        {
            Strike_ImpactEnd();
        }
        if(ShootDown && Check.IsGround)
        {
            Shoot_End();
        }
        if(Sky_LaserDown && Check.IsGround)
        {
            Sky_LaserEnd();
        }
        if(Ground_TridentDown && Check.IsGround)
        {
            Ground_TridentEnd();
        }
        if(Sky_TridentDown && Check.IsGround)
        {
            Sky_TridentEnd();
        }
        PlayerPosition = GameManager.Instance.PlayerStats.gameObject.transform.position;
    }
    private void FixedUpdate()
    {
        if (Collide)
        {
            Collide_End();
        }
        if (Strike)
        {
            StrikeForward();
        }
    }
    private void ChangeSkill()
    {
        for (int i = 0;i < SkillList.BossSkills.Count; i++)
        {
            if (SkillList.BossSkills[i].IsOpen)
            {
                SkillLevel = Random.Range(0f, 1f);
                if (SkillLevel < SkillList.BossSkills[i].SkillProbability)
                {
                    Skill = SkillList.BossSkills[i].Type;
                    break;
                }
            }
        }
        switch (Skill)
        {
            case SkillType.Collide:
                UseCollide();
                break;
            case SkillType.Strike:
                StartCoroutine(UseStrike());
                Strike = true;
                break;
            case SkillType.Strike_Impact:
                StartCoroutine(UseStrike_Impact());
                Strike_Impact = true;
                break;
            case SkillType.Shoot:
                Shoot = true;
                StartCoroutine(UseShoot());
                break;
            case SkillType.Ground_Laser:
                StartCoroutine(UseGround_Laser());
                Ground_Laser = true;
                break;
            case SkillType.Sky_Laser:
                Sky_Laser = true;
                StartCoroutine(UseSky_Laser());
                break;
            case SkillType.Ground_Trident:
                StartCoroutine(UseGround_Trident());
                Ground_Trident = true;
                break;
            case SkillType.Sky_Trident:
                StartCoroutine(UseSky_Trident());
                Sky_Trident = true;
                break;
            case SkillType.Evoke_Army:
                StartCoroutine(UseEvoke_Army());
                Evoke_Army = true;
                break;
            default:
                break;
        }
    }
    private void UseCollide()
    {
        if (PlayerPosition.x >= GameManager.Instance.BossBound.transform.GetChild(2).position.x)//右边
        {
            Collide_Time_Count = Collide_Time;
            Collide_Speed_Count = Collide_Speed;
            Collide = true;
            transform.position = GameManager.Instance.BossBound.transform.GetChild(0).position;
        }
        else if (PlayerPosition.x < GameManager.Instance.BossBound.transform.GetChild(2).position.x)//左边
        {
            Collide_Time_Count = Collide_Time;
            Collide_Speed_Count = -Collide_Speed;
            Collide = true;
            transform.position = GameManager.Instance.BossBound.transform.GetChild(1).position;
        }
    }
    private void Collide_End()
    {
        if(Collide_Time_Count >= 0)
        {
            Collide_Time_Count -= Time.deltaTime;
            rb.velocity = new Vector2(Collide_Speed_Count * Time.deltaTime, rb.velocity.y);
        }
        if (Collide_Time_Count < 0)
        {
            Collide = false;
            IsSkill = false;
            rb.velocity = Vector2.zero;
            SkillTime_Count = SkillTime;
        }
    }
    private IEnumerator UseStrike()
    {
        Strike_Up();
        yield return new WaitForSeconds(0.45f);
        Strike_Hold();
        yield return new WaitForSeconds(1f);
        Strike_Down();
    }
    private void Strike_Up()
    {
        IsStrike_Up = true;
        rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }
    private void StrikeForward()
    {
        if (IsStrike_Up)
        {
            if (transform.position.x - PlayerPosition.x > 0.5f)//右边
            {
                rb.velocity = new Vector2(-Strike_JumpForwardSpeed * Time.deltaTime, rb.velocity.y);
            }
            else if (transform.position.x - PlayerPosition.x < -0.5)//左边
            {
                rb.velocity = new Vector2(Strike_JumpForwardSpeed * Time.deltaTime, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }
    private void Strike_Hold()
    {
        rb.velocity = new Vector2(rb.velocity.x,0);
        rb.gravityScale = 0f;
    }
    private void Strike_Down()
    {
        IsStrike_Up = false;
        IsStrike_Down = true;
        rb.velocity = Vector2.zero;
        rb.gravityScale = Settings.BossGravity;
    }
    private void Strike_End()
    {
        IsStrike_Down = false;
        IsSkill = false;
        Strike = false;
        SkillTime_Count = SkillTime;
    }
    private IEnumerator UseStrike_Impact()
    {
        Strike_Impact_Up();
        yield return new WaitForSeconds(0.45f);
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.3f);
        rb.gravityScale = Settings.BossGravity;
        Strike_Impact_Down = true;
    }
    private void Strike_Impact_Up()
    {
        rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        if (transform.position.x - PlayerPosition.x > 0)//右边
        {
            rb.velocity = new Vector2(-Strike_Impact_JumpForwardSpeed * Time.deltaTime, rb.velocity.y);
        }
        else if (transform.position.x - PlayerPosition.x < 0)//左边
        {
            rb.velocity = new Vector2(Strike_Impact_JumpForwardSpeed * Time.deltaTime, rb.velocity.y);
        }
    }
    private void Strike_ImpactEnd()
    {
        var ImpactOne = Instantiate(Impact, transform.position + new Vector3(2,-0.25f,0), Quaternion.identity);
        ImpactOne.transform.localScale = new Vector3(-1.5f, 1.5f, 1);
        var ImpactTwo = Instantiate(Impact, transform.position + new Vector3(-2,-0.25f,0), Quaternion.identity);
        ImpactTwo.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        Strike_Impact_Down = false;
        IsSkill = false;
        Strike_Impact = false;
        rb.velocity = Vector2.zero;
        SkillTime_Count = SkillTime;
    }
    private IEnumerator UseShoot()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        transform.position = new Vector3(25.48f,7.7f,0);
        for(int i = 0;i < 12; i++)
        {
            var NewShoot = Instantiate(BossShoot, ShootList.ShootList[i], Quaternion.identity);
            NewShoot.GetComponent<BossShoot>().Boss = this.transform;
        }
        yield return new WaitForSeconds(0.5f);
        ShootDown = true;
        for (int i = 12; i < ShootList.ShootList.Count; i++)
        {
            var NewShoot = Instantiate(BossShoot, ShootList.ShootList[i], Quaternion.identity);
            NewShoot.GetComponent<BossShoot>().Boss = this.transform;
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 12; i++)
        {
            var NewShoot = Instantiate(BossShoot, ShootList.ShootList[i], Quaternion.identity);
            NewShoot.GetComponent<BossShoot>().Boss = this.transform;
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 12; i < ShootList.ShootList.Count; i++)
        {
            var NewShoot = Instantiate(BossShoot, ShootList.ShootList[i], Quaternion.identity);
            NewShoot.GetComponent<BossShoot>().Boss = this.transform;
        }
        yield return new WaitForSeconds(0.5f);
        rb.gravityScale = Settings.BossGravity;
    }
    private void Shoot_End()
    {
        Shoot = false;
        IsSkill = false;
        ShootDown = false;
        SkillTime_Count = SkillTime;
    }
    private IEnumerator UseGround_Laser()
    {
        if (PlayerPosition.x >= GameManager.Instance.BossBound.transform.GetChild(2).position.x)//右边
        {
            transform.position = GameManager.Instance.BossBound.transform.GetChild(0).position;
            yield return new WaitForSeconds(1f);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.transform.localScale = new Vector3(-2f, 1.25f, 1);
        }
        else if (PlayerPosition.x < GameManager.Instance.BossBound.transform.GetChild(2).position.x)//左边
        {
            transform.position = GameManager.Instance.BossBound.transform.GetChild(1).position;
            yield return new WaitForSeconds(1f);
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.transform.localScale = new Vector3(2f, 1.25f, 1);
        }
        yield return new WaitForSeconds(1.5f);
        transform.GetChild(0).gameObject.SetActive(false);
        Ground_Laser = false;
        IsSkill = false;
        SkillTime_Count = SkillTime;
    }
    private IEnumerator UseSky_Laser()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        transform.position = new Vector3(25.48f, 7.7f, 0);
        yield return new WaitForSeconds(0.7f);
        Sky_LaserDown = true;
        transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        transform.GetChild(1).gameObject.SetActive(false);
        rb.gravityScale = Settings.BossGravity;
    }
    private void Sky_LaserEnd()
    {
        Sky_LaserDown = false;
        Sky_Laser = false;
        IsSkill = false;
        SkillTime_Count = SkillTime;
    }
    private IEnumerator UseGround_Trident()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        transform.position = new Vector3(25.48f, 7.7f, 0);
        transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        rb.gravityScale = Settings.BossGravity;
        Ground_TridentDown = true;
        transform.GetChild(2).gameObject.SetActive(false);
    }
    private void Ground_TridentEnd()
    {
        Ground_TridentDown = false;
        Ground_Trident = false;
        IsSkill = false;
        SkillTime_Count = SkillTime;
    }
    private IEnumerator UseSky_Trident()
    {
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;
        transform.position = new Vector3(25.48f, 7.7f, 0);
        transform.GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        transform.GetChild(3).gameObject.SetActive(false);
        rb.gravityScale = Settings.BossGravity;
        Sky_TridentDown = true;
    }
    private void Sky_TridentEnd()
    {
        Sky_Trident = false;
        Sky_TridentDown = false;
        IsSkill = false;
        SkillTime_Count = SkillTime;
    }
    private IEnumerator UseEvoke_Army()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(BossArmy, transform.position + new Vector3(1.5f, 0, 0), Quaternion.identity);
        Instantiate(BossArmy, transform.position + new Vector3(-1.5f, 0, 0), Quaternion.identity);
        Evoke_Army = false;
        IsSkill = false;
        SkillTime_Count = SkillTime;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log(1);
        }
    }
}
