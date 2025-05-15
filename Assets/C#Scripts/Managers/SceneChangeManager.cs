using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    public void ChangeRoom()
    {
        StartCoroutine(OnChangeRoom());
    }
    private IEnumerator OnChangeRoom()
    {
        GameManager.Instance.Boss().NowHealth = GameManager.Instance.Boss().MaxHealth;
        MapManager.Instance.SetNewMap();//创建新地图
        DataManager.Instance.Save(DataManager.Instance.Index);//存档
        KeyBoardManager.Instance.StopAnyKey = true;
        if (GameManager.Instance.PlayerData.CurrentRoomCount < GameManager.Instance.PlayerData.RoomCount)
        {
            GameManager.Instance.PlayerData.CurrentRoomCount++;
        }
        else
        {
            EndCanvs.SetActive(true);
            GameManager.Instance.PlayerData.CurrentRoomCount = 0;
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
        KeyBoardManager.Instance.StopMoveKey = false;
        Boss.GetComponent<BossController>().IsStopBoss = false;
        PlotManager.Instance.SetRoomPlotText();
    }
    public void StartGame(SceneData CurrentScene, SceneData NextScene)
    {
        StartCoroutine(OnChangeScene(CurrentScene, NextScene));
    }
    private IEnumerator OnStartGame()
    {
        GameManager.Instance.Boss().NowHealth = GameManager.Instance.Boss().MaxHealth;
        GameManager.Instance.PlayerData.StartGame = true;
        MapManager.Instance.SetNewMap();//创建新地图
        DataManager.Instance.Save(DataManager.Instance.Index);//存档
        KeyBoardManager.Instance.StopAnyKey = true;
        GameManager.Instance.PlayerData.CurrentRoomCount = 1;
        Fadecanvs.FadeIn();
        yield return new WaitForSeconds(0.1f);
        ColorManager.Instance.ChangeColor();
        GameManager.Instance.RefreshPlayer();
        PlayerEquipManager.Instance.UseHat(GameManager.Instance.PlayerData.HatData.Index);
        GameManager.Instance.RefreshBoss();
        GameManager.Instance.RefreshBossSkill();
        GameManager.Instance.BossActive = true;
        Boss.GetComponent<BossController>().IsStopBoss = true;
        yield return new WaitForSeconds(0.5f);
        Fadecanvs.FadeOut();
        Door.SetActive(true);
        Door.GetComponent<Door>().SetDoor();
        ScoreManager.Instance.StartGetScore();
        yield return new WaitForSeconds(0.5f);
        KeyBoardManager.Instance.StopAnyKey = false;
        KeyBoardManager.Instance.StopMoveKey = false;
        GameManager.Instance.PlayerStats.gameObject.GetComponent<PlayerController>().ContinuePlayer();
        Boss.GetComponent<BossController>().IsStopBoss = false;
        PlotManager.Instance.ThisRoomPlot.CurrentIndex = 0;
        PlotManager.Instance.SetRoomPlotText();
    }
    public void LoadGame()
    {
        StartCoroutine(OnChangeScene(GameManager.Instance.PlayerData.CurrentScene));
    }
    public void ChangeScene(SceneData CurrentScene,SceneData NextScene)
    {
        StartCoroutine(OnChangeScene(CurrentScene,NextScene));
    }
    private IEnumerator OnChangeScene(SceneData CurrentScene, SceneData NextScene)
    {
        KeyBoardManager.Instance.StopAnyKey = true;
        GameManager.Instance.PlayerStats.gameObject.GetComponent<PlayerController>().StopPlayer();
        if (NextScene.SceneName != "BossMap")
        {
            Fadecanvs.FadeIn();
        }
        yield return new WaitForSeconds(0.1f);
        SceneManager.UnloadSceneAsync(CurrentScene.SceneName);
        SceneManager.LoadSceneAsync(NextScene.SceneName, LoadSceneMode.Additive);
        GameManager.Instance.PlayerStats.gameObject.transform.position = NextScene.ToPosition;
        GameManager.Instance.PlayerData.CurrentScene = NextScene;
        if(CurrentScene.SceneName == "BossMap")
        {
            GameManager.Instance.PlayerData.StartGame = false;
        }
        DataManager.Instance.Save(DataManager.Instance.Index);//存档
        if (NextScene.SceneName == "BossMap")
        {
            yield return new WaitForSeconds(0.02f);
            StartCoroutine(OnStartGame());
        }
        else if(NextScene.SceneName != "BossMap")
        {
            yield return new WaitForSeconds(0.2f);
            Fadecanvs.FadeOut();
            yield return new WaitForSeconds(0.5f);
            GameManager.Instance.PlayerStats.gameObject.GetComponent<PlayerController>().ContinuePlayer();
        }
        KeyBoardManager.Instance.StopAnyKey = false;
    }
    private IEnumerator OnChangeScene(SceneData CurrentScene)
    {
        KeyBoardManager.Instance.StopAnyKey = true;
        GameManager.Instance.PlayerStats.gameObject.GetComponent<PlayerController>().StopPlayer();
        if (!GameManager.Instance.PlayerData.StartGame)
        {
            Fadecanvs.FadeIn();
            yield return new WaitForSeconds(0.1f);
        }
        SceneManager.LoadSceneAsync(CurrentScene.SceneName, LoadSceneMode.Additive);
        GameManager.Instance.PlayerStats.gameObject.transform.position = CurrentScene.ToPosition;
        if (!GameManager.Instance.PlayerData.StartGame)
        {
            Fadecanvs.FadeOut();
            yield return new WaitForSeconds(0.5f);
        }
        GameManager.Instance.PlayerStats.gameObject.GetComponent<PlayerController>().ContinuePlayer();
        KeyBoardManager.Instance.StopAnyKey = false;
        if(GameManager.Instance.PlayerData.StartGame)
        {
            yield return new WaitForSeconds(0.02f);
            StartCoroutine(OnChangeRoom());
        }
    }
    public void ShowRoomCount(int count)
    {
        GameManager.Instance.PlayerData.RoomCount = count;
    }
    public void EndGame(SceneData CurrentScene,SceneData NextScene)
    {
        ChangeScene(CurrentScene,NextScene);
        StartCoroutine(Ending());
    }
    private IEnumerator Ending() 
    {
        GameManager.Instance.PlayerStats.CharacterData_Temp = Instantiate(GameManager.Instance.PlayerStats.CharacterData);
        yield return new WaitForSeconds(0.1f);
        Boss.SetActive(false);
        GameManager.Instance.BossDeadEvent.RaiseEvent();
    }
    public void OpenStartCanvs()
    {
        Startcanvs.SetActive(true);
    }
}
