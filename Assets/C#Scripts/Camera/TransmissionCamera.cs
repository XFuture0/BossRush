using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionCamera : MonoBehaviour
{
    private float InputX;
    private float InputY;
    public float CameraSpeed;
    [Header("¹ã²¥")]
    public CameraEventSO SetCameraEvent;
    public VoidEventSO CloseCameraEvent;
    private void OnEnable()
    {
        SetCameraEvent.RaiseCameraEvent(gameObject.GetComponent<Camera>());
    }
    private void OnDisable()
    {
        CloseCameraEvent.RaiseEvent();
    }
    private void Update()
    {
        InputX = KeyBoardManager.Instance.GetHorizontalRaw_MiNi();
        InputY = KeyBoardManager.Instance.GetVerticalRaw_MiNi();
        transform.position += new Vector3(InputX * Time.deltaTime * CameraSpeed, InputY * Time.deltaTime * CameraSpeed, 0);
    }
}
