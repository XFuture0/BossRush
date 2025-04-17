using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseBossItem : MonoBehaviour
{
    [Header("�¼�����")]
    public VoidEventSO BossDeadEvent;
    private void OnEnable()
    {
        BossDeadEvent.OnEventRaised += OnBossDead;   
    }

    private void OnBossDead()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        BossDeadEvent.OnEventRaised -= OnBossDead;
    }
}
