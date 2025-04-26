using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCanvs : MonoBehaviour
{
    public GameObject PlayerSlot;
    public Button StartButton;
    public Vector3 PositionToGo;
    private void Awake()
    {
        StartButton.onClick.AddListener(OnStartButton);
    }
    private void OnStartButton()
    {
        DataManager.Instance.Save(DataManager.Instance.Index);
        KeyBoardManager.Instance.StopMoveKey = false;
        SceneChangeManager.Instance.StartGame();
        gameObject.SetActive(false);
    }
}
