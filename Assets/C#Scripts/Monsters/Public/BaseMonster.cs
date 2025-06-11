using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static MapData;

public class BaseMonster : MonoBehaviour
{
    private CharacterStats ThisStats;
    public MapCharacrter.Monster ThisMonster;
    [Header("ÊÂ¼þ¼àÌý")]
    public VoidEventSO ClearMonsterEvent;
    private void Awake()
    {
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
        if(ThisStats.CharacterData_Temp.NowHealth <= 0)
        {
            Destroy(gameObject);
        }
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
        if(Physics2D.OverlapPoint(SceneChangeManager.Instance.Player.transform.position, SceneChangeManager.Instance.Room))
        {
            Physics2D.OverlapPoint(SceneChangeManager.Instance.Player.transform.position, SceneChangeManager.Instance.Room).gameObject.GetComponent<MapCharacrter>().DeleteMonster(ThisMonster);
        }
    }
}
