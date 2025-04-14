using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BossController : MonoBehaviour
{
    private Rigidbody2D rb;
    private BossCheck Check;
    private bool IsSkill;
    public bool IsStopBoss;
    private float SkillLevel;
    private SkillType Skill;
    public BossSkillList SkillList;
    private Vector3 PlayerPosition;
    private CharacterStats Boss;
    [Header("技能列表")]
    public bool CrashDown;
    [Header("技能效果")]
    [Header("技能计时器")]
    private float SkillTime_Count;
    [Header("行走计时器")]
    private float WalkTime_Count = -2;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Check = GetComponent<BossCheck>();
        Boss = GetComponent<CharacterStats>();
    }
    private void Start()
    {
        Invoke("DestoryIng", 0.01f);
    }
    private void OnEnable()
    {
        SkillTime_Count = 0.2f;
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
        if (SkillTime_Count < 0 && !IsSkill && !IsStopBoss)
        {
            IsSkill = true;
            ChangeSkill();
        }
        PlayerPosition = GameManager.Instance.PlayerStats.gameObject.transform.position;
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
            case SkillType.CrashDown:
                CrashDown = true;
              //  StartCoroutine(UseCrashDown());
                break;
            default:
                break;
        }
    }
    private void FixedUpdate()
    {
        CanWalk();
    }
    private void CanWalk()
    {
        if(WalkTime_Count > -1)
        {
            WalkTime_Count -= Time.deltaTime;
        }
        if(WalkTime_Count <= 0)
        {
            WalkTime_Count = Random.Range(1f, 3f);
            Walk();
        }
    }
    private void Walk()
    {
        var ForceRotation = new Vector2(0, 0);
        var Walkspeed = Random.Range(Boss.CharacterData_Temp.Speed * 0.8f,Boss.CharacterData_Temp.Speed * 1.5f);
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
    private IEnumerator UseCrashDown()
    {
        var NewCrashDownPosition = new Vector3(0, 0, 0);
        yield return null;
    }
}
