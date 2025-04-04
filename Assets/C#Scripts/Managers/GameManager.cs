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
    [Header("�㲥")]
    public BoundEventSO BoundEvent;
    [Header("�¼�����")]
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
