using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndCanvs : MonoBehaviour
{
    public FadeCanvs Fadecanvs;
    public GameObject Startcanvs;
    public Button ReturnButton;
    private void Awake()
    {
        ReturnButton.onClick.AddListener(EndGame);
    }
    private void OnEnable()
    {
        KeyBoardManager.Instance.StopMoveKey = true;
        SceneChangeManager.Instance.Boss.GetComponent<BossController>().IsStopBoss = true;
    }
    private void EndGame()
    {
        SceneChangeManager.Instance.EndGame();
        gameObject.SetActive(false);
    }
}
