using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCanvs : MonoBehaviour
{
    public Button StartButton;
    public SceneData NextScene;
    public Vector3 PositionToGo;
    private void Awake()
    {
        StartButton.onClick.AddListener(OnStartButton);
    }
    private void OnStartButton()
    {
        KeyBoardManager.Instance.StopMoveKey = false;
        SceneChangeManager.Instance.ChangeScene(NextScene);
        gameObject.SetActive(false);
    }
}
