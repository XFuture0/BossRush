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
    public GameObject Startcanvs;
    public FadeCanvs Fadecanvs;
    public GameObject EndCanvs;
    public int HatIndex;
    [Header("·¿¼äÊý")]
    public int CurrentRoomCount;
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
        yield return new WaitForSeconds(0.1f);
        ColorManager.Instance.ChangeColor();
        Player.transform.position = new Vector3(-20.64f, -0.44f, 0);
        Boss.transform.position = new Vector3(-15f, 0.97f, 0);
        Boss.SetActive(true);
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
        ScoreManager.Instance.StartGetScore();
        CurrentRoomCount = 1;
        Fadecanvs.FadeIn();
        yield return new WaitForSeconds(0.1f);
        CardManager.Instance.RefreshCard();
        ColorManager.Instance.ChangeColor();
        GameManager.Instance.RefreshPlayer();
        PlayerEquipManager.Instance.ChangeHat(HatIndex);
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
    public void LoadGame()
    {
        Startcanvs.SetActive(true);
    }
    public void ShowRoomCount(int count)
    {
        RoomCount = count;
    }
    public void EndGame()
    {
        StartCoroutine(Ending());
    }
    private IEnumerator Ending() 
    {
        Fadecanvs.FadeIn();
        Startcanvs.SetActive(true);
        GameManager.Instance.PlayerStats.CharacterData_Temp = Instantiate(GameManager.Instance.PlayerStats.CharacterData);
        yield return new WaitForSeconds(0.5f);
        Boss.SetActive(false);
        GameManager.Instance.BossDeadEvent.RaiseEvent();
        Fadecanvs.FadeOut();
    }
}
