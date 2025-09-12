using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using static MapData;

public class BaseMonster : MonoBehaviour
{
    private CharacterStats ThisStats;
    public MapCharacrter.Monster ThisMonster;
    private SpriteRenderer ThisSpriteRenderer;
    private bool IsDead;
    [Header("消失计时器")]
    private float DisappearSpeed = 5;
    [Header("事件监听")]
    public VoidEventSO ClearMonsterEvent;
    private void Awake()
    {
        ThisSpriteRenderer = GetComponent<SpriteRenderer>();
        ThisStats = GetComponent<CharacterStats>();
    }
    private void OnEnable()
    {
        ClearMonsterEvent.OnEventRaised += OnClear;
    }
    private void OnClear()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if(ThisStats.CharacterData_Temp.NowHealth <= 0 && !IsDead)
        {
            IsDead = true;
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        Destroying();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameManager.Instance.Attack(ThisStats,GameManager.Instance.PlayerStats);
        }
    }
    private void OnDisable()
    {
        ClearMonsterEvent.OnEventRaised -= OnClear;
    }
     private void Destroying()
    {
        if (IsDead)
        {
            var NewAlpha = math.lerp(ThisSpriteRenderer.color.a, 0, DisappearSpeed * Time.deltaTime);
            ThisSpriteRenderer.color = new Color(ThisSpriteRenderer.color.r, ThisSpriteRenderer.color.g, ThisSpriteRenderer.color.b, NewAlpha);
        }
        if(ThisSpriteRenderer.color.a <= 0.05f)
        {
            Destroy(gameObject);
        }
    }
}
