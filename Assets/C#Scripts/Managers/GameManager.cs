using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingleTons<GameManager>
{
    public CharacterStats PlayerStats;
    public CharacterStats BossStats;
    public Collider2D MainBounds;
    public GameObject BossBound;
    [Header("¹ã²¥")]
    public BoundEventSO BoundEvent;
    [Header("ÊÂ¼þ¼àÌý")]
    public BoundEventSO GetBoundEvent;
    private void OnEnable()
    {
        PlayerStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        BossStats = GameObject.Find("Boss").GetComponent<CharacterStats>();
        GetBoundEvent.OnBoundEventRaised += OnGetBound;
    }

    private void OnGetBound(Collider2D target)
    {
        BossBound = target.gameObject;
    }

    public void ReturnMainBounds()
    {
        BoundEvent.BoundRaiseEvent(MainBounds);
    }
}
