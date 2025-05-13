using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCanvs : MonoBehaviour
{
    public SceneData CurrentScene;
    public SceneData NextScene;
    public Button StartButton;
    public Button ReturnButton;
    private void Awake()
    {
        StartButton.onClick.AddListener(OnStartButton);
        ReturnButton.onClick.AddListener(OnReturnButton);
    }
    private void OnStartButton()
    {
        KeyBoardManager.Instance.StopAnyKey = false;
        SceneChangeManager.Instance.StartGame(CurrentScene,NextScene);
        gameObject.SetActive(false);
    }
    private void OnReturnButton()
    {
        KeyBoardManager.Instance.StopAnyKey = false;
        gameObject.SetActive(false);
    }
}
