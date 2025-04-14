using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChangeManager : SingleTons<SceneChangeManager>
{
    public GameObject Door;
    public GameObject Player;
    public GameObject Boss;
    private Vector3 PlayerPosition;
    public BossCanvs Bosscanvs;
    public FadeCanvs Fadecanvs;
    public GameObject EndCanvs;
    [Header("·¿¼äÊý")]
    private int CurrentRoomCount;
    public int RoomCount;
    public void ChangeScene()
    {
        StartCoroutine(OnChangeScene());
    }
    private IEnumerator OnChangeScene()
    {
        KeyBoardManager.Instance.StopAnyKey = true;
        if (CurrentRoomCount < RoomCount)
        {
            CurrentRoomCount++;
        }
        else
        {
            EndCanvs.SetActive(true);
            yield break;
        }
        Fadecanvs.FadeIn();
        ColorManager.Instance.ChangeColor();
        GameManager.Instance.RefreshBossSkill();
        GameManager.Instance.AddBossHealth();
        GameManager.Instance.BossActive = true;
        Boss.GetComponent<BossController>().IsStopBoss = true;
        yield return new WaitForSeconds(0.5f);
        Fadecanvs.FadeOut();
        Door.SetActive(true);
        Door.GetComponent<Door>().SetDoor();
        yield return new WaitForSeconds(0.5f);
        KeyBoardManager.Instance.StopAnyKey = false;
        Boss.GetComponent<BossController>().IsStopBoss = false;
    }
    public void StartGame()
    {
        StartCoroutine(OnStartGame());
    }
    private IEnumerator OnStartGame()
    {
        KeyBoardManager.Instance.StopAnyKey = true;
        CurrentRoomCount = 1;
        Fadecanvs.FadeIn();
        ColorManager.Instance.ChangeColor();
        GameManager.Instance.RefreshPlayer();
        GameManager.Instance.RefreshBoss();
        GameManager.Instance.RefreshBossSkill();
        GameManager.Instance.BossActive = true;
        Boss.GetComponent<BossController>().IsStopBoss = true;
        yield return new WaitForSeconds(0.5f);
        Fadecanvs.FadeOut();
        Door.SetActive(true);
        Door.GetComponent<Door>().SetDoor();
        yield return new WaitForSeconds(0.5f);
        KeyBoardManager.Instance.StopAnyKey = false;
        KeyBoardManager.Instance.StopMoveKey = false;
        Boss.GetComponent<BossController>().IsStopBoss = false;
    }
    public void ShowRoomCount(int count)
    {
        RoomCount = count;
    }
}
