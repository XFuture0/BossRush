using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingCanvs : MonoBehaviour
{
    public Button ReturnButton;
    public Slider VolumeSlider;
    private void Awake()
    {
        ReturnButton.onClick.AddListener(OnReturnButton);
        VolumeSlider.onValueChanged.AddListener(OnVolumeChange);
    }
    private void OnEnable()
    {
        VolumeSlider.value = GameManager.Instance.GlobalData.AudioVolume;
    }
    private void OnReturnButton()
    {
        gameObject.SetActive(false);
    }
    private void OnVolumeChange(float Value)
    {
        GameManager.Instance.GlobalData.AudioVolume = Value;
        DataManager.Instance.SaveGlobal();
        AudioManager.Instance.SetMainAudioVolume();
    }
}
