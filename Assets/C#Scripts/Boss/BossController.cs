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
    [Header("技能列表")]
    [Header("技能效果")]
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
        Invoke("DestoryIng", 0.01f);
    }
    private void OnEnable()
    {
        SkillTime_Count = 0.2f;
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
            default:
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameManager.Instance.Attack(gameObject.GetComponent<CharacterStats>(),other.GetComponent<CharacterStats>());
        }
    }
}
