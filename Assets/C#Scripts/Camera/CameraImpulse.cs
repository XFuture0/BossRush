using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraImpulse : MonoBehaviour
{
    public CinemachineImpulseSource ImpulseSource;
    [Header("ÊÂ¼þ¼àÌý")]
    public VoidEventSO ImpulseEvent;
    private void OnEnable()
    {
        ImpulseEvent.OnEventRaised += ImpulseSource.GenerateImpulse;
    }
}
