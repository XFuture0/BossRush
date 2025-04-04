using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBound : MonoBehaviour
{
    private CinemachineConfiner2D Confiner2D;
    [Header("ÊÂ¼þ¼àÌý")]
    public BoundEventSO BoundEvent;
    private void Awake()
    {
        Confiner2D = GetComponent<CinemachineConfiner2D>();
        BoundEvent.OnBoundEventRaised += OnChangeBound;
    }
    public void OnChangeBound(Collider2D Bound)
    {
        Confiner2D.m_BoundingShape2D = Bound.GetComponent<Collider2D>();
        Confiner2D.InvalidateCache();
    }
}
