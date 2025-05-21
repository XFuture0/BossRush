using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomCanvs : MonoBehaviour
{
    public Canvas roomcanvs;
    public Button ChangeButton;
    public GameObject TransmissionTower;
    public GameObject TransmissionTowerMini;
    [Header("ÊÂ¼þ¼àÌý")]
    public CameraEventSO SetCameraEvent;
    public VoidEventSO CloseCameraEvent;
    private void Awake()
    {
        ChangeButton.onClick.AddListener(OnChangeButton);
    }
    private void OnChangeButton()
    {
        if (TransmissionTowerMini.activeSelf)
        {
            MapManager.Instance.TransmissionRoom(TransmissionTower.transform.position);
        }
    }
    private void OnEnable()
    {
        SetCameraEvent.OnCameraEventRaised += OnSetCamera;
        CloseCameraEvent.OnEventRaised += OnCloseCamera;
    }
    private void OnCloseCamera()
    {
        ChangeButton.gameObject.SetActive(false);
    }

    private void OnSetCamera(Camera camera)
    {
        roomcanvs.worldCamera = camera;
        ChangeButton.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        SetCameraEvent.OnCameraEventRaised -= OnSetCamera;
        CloseCameraEvent.OnEventRaised -= OnCloseCamera;
    }
}
