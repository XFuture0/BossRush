using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBound : MonoBehaviour
{
    private CinemachineConfiner2D Confiner2D;
    private CinemachineVirtualCamera VirtualCamera;
    [Header("ÊÂ¼þ¼àÌý")]
    public BoundEventSO BoundEvent;
    private void Awake()
    {
        Confiner2D = GetComponent<CinemachineConfiner2D>();
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }
    private void OnEnable()
    {
        BoundEvent.OnBoundEventRaised += OnChangeBound;
    }
    public void OnChangeBound(Collider2D Bound, float Size)
    {
        VirtualCamera.m_Lens.OrthographicSize = Size;
        Confiner2D.m_BoundingShape2D = Bound;
        Confiner2D.InvalidateCache();
    }
}
