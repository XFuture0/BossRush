using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvs : MonoBehaviour
{
    public GameObject SettingCanvs;
    public GameObject SaveCanvs;
    public Button LoadButton;
    public Button SettingButton;
    public Button QuitButton;
    private void Awake()
    {
        LoadButton.onClick.AddListener(OnLoad);
        SettingButton.onClick.AddListener(OnSetting);
        QuitButton.onClick.AddListener(OnQuit);
    }
    private void OnLoad()
    {
        SaveCanvs.SetActive(true);
        gameObject.SetActive(false);
    }
    private void OnSetting()
    {
        SettingCanvs.SetActive(true);
        gameObject.SetActive(false);
    }
    private void OnQuit()
    {
        Application.Quit();
    }
}
