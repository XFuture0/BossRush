using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndCanvs : MonoBehaviour
{
    public FadeCanvs Fadecanvs;
    public GameObject Startcanvs;
    public Button ReturnButton;
    public SceneData CurrentScene;
    public SceneData NextScene;
    public Text ScoreText;
    private void Awake()
    {
        ReturnButton.onClick.AddListener(EndGame);
    }
    private void OnEnable()
    {
        KeyBoardManager.Instance.StopAnyKey = true;
        SceneChangeManager.Instance.Boss.GetComponent<BossController>().IsStopBoss = true;
    }
    private void EndGame()
    {
        SceneChangeManager.Instance.EndGame(CurrentScene,NextScene);
        gameObject.SetActive(false);
    }
}
