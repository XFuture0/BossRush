using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopCanvs : MonoBehaviour
{
    public Button SettingButton;
    public Button ContinueButton;
    public Button ReturnMainMenuButton;
    public GameObject SettingCanvs;
    public GameObject MainMenuCanvs;
    private void Awake()
    {
        SettingButton.onClick.AddListener(OnSettingButton);
        ContinueButton.onClick.AddListener(OnContinueButton);
        ReturnMainMenuButton.onClick.AddListener(OnReturnMainMenuButton);
    }
    private void OnEnable()
    {
        Time.timeScale = 0;
    }
    private void OnSettingButton()
    {
        SettingCanvs.SetActive(true);
    }
    private void OnContinueButton()
    {
        gameObject.SetActive(false);
    }
    private void OnReturnMainMenuButton()
    {
        GameManager.Instance.PlayerStats.gameObject.GetComponent<PlayerController>().StopPlayer();
        GameManager.Instance.BossStats.gameObject.SetActive(false);
        MapManager.Instance.ClearMap();
        SceneChangeManager.Instance.DeleteCurrentScene();
        MainMenuCanvs.SetActive(true);
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
