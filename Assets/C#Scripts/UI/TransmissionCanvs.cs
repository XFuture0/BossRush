using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransmissionCanvs : MonoBehaviour
{
    public Button ReturnButton;
    private void Awake()
    {
        ReturnButton.onClick.AddListener(OnReturnButton);
    }
    private void OnReturnButton()
    {
        MapManager.Instance.CloseTransmission();
    }
}
