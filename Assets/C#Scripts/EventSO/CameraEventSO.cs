using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Event/CameraEventSO")]
public class CameraEventSO : ScriptableObject
{
    public UnityAction<Camera> OnCameraEventRaised;
    public void RaiseCameraEvent(Camera camera)
    {
        OnCameraEventRaised?.Invoke(camera);
    }
}
