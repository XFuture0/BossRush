using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class SceneChangeManager : SingleTons<SceneChangeManager>
{
    private bool IsSetRoomData;
    public bool IsSetPosition;
    public GameObject Door;
    public GameObject Player;
    public GameObject Boss;
    private Vector3 PlayerPosition;
    public BossCanvs Bosscanvs;
    public GameObject Startcanvs;
    public FadeCanvs Fadecanvs;
    public GameObject EndCanvs;
    public LayerMask Room;
    [Header("广播")]
    public VoidEventSO OpenDoorEvent;
    public VoidEventSO CloseDoorEvent;
    public Vector3EventSO ChangeBossSkillEvent;
    public Vector3EventSO ChangeMiniMapPositionEvent;
    public void ChangeMap()
    {
        StartCoroutine(OnChangeMap());
    }
    private IEnumerator OnChangeMap()
    {
        Fadecanvs.FadeIn();
        GameManager.Instance.PlayerStats.gameObject.GetComponent<PlayerController>().StopPlayer();
        KeyBoardManager.Instance.StopAnyKey = true;
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.Boss().NowHealth = GameManager.Instance.Boss().MaxHealth;
        if (!IsSetRoomData)
        {
            MapManager.Instance.SetNewMap();//创建新地图
            GameManager.Instance.PlayerData.RoomType = RoomType.StartRoom;
            ColorManager.Instance.ChangeColor();
        }
        yield return new WaitForSeconds(5f);
        DataManager.Instance.Save(DataManager.Instance.Index);//存档
        if (!IsSetRoomData)
        {
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
        }
        else if (IsSetRoomData)
        {
            ColorManager.Instance.SetColorData(GameManager.Instance.PlayerData.CurrentColor);
        }
        if (!IsSetPosition)
        {
            Player.transform.position = new Vector3(-20.64f, -0.44f, 0);//地图切换回到初始点
        }
        IsSetPosition = false;
        yield return new WaitForSeconds(0.5f);
        Fadecanvs.FadeOut();
        if (!IsSetRoomData)
        {
            MapManager.Instance.AccessRoom(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position);
        }
        MapManager.Instance.FindRoom(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position);
        IsSetRoomData = false;
        Door.SetActive(true);
        Door.GetComponent<Door>().SetDoor();
        ChangeMiniMapPositionEvent.RaiseVector3Event(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position);//改变小地图相机位置
        yield return new WaitForSeconds(1f);
        if (!IsSetRoomData)
        {
            ColorManager.Instance.SetColorText();
        }
        OpenDoorEvent.RaiseEvent();
        SetChangeRoom();
        GameManager.Instance.PlayerStats.gameObject.GetComponent<PlayerController>().ContinuePlayer();
        KeyBoardManager.Instance.StopAnyKey = false;
        KeyBoardManager.Instance.StopMoveKey = false;
    }
    public void StartGame(SceneData CurrentScene, SceneData NextScene)
    {
        StartCoroutine(OnChangeScene(CurrentScene, NextScene));
    }
    private IEnumerator OnStartGame()
    {
        Fadecanvs.FadeIn();
        KeyBoardManager.Instance.StopAnyKey = true;
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.Boss().NowHealth = GameManager.Instance.Boss().MaxHealth;
        GameManager.Instance.PlayerData.StartGame = true;
        GameManager.Instance.RefreshPlayer();
        PlayerEquipManager.Instance.UseHat(GameManager.Instance.PlayerData.HatData.Index);
        GameManager.Instance.RefreshBoss();
        ScoreManager.Instance.StartGetScore();
        if (!IsSetRoomData)
        {
            MapManager.Instance.SetNewMap();//创建新地图
            GameManager.Instance.PlayerData.RoomType = RoomType.StartRoom;
            ColorManager.Instance.ChangeColor();
        }
        else if (IsSetRoomData)
        {
            ColorManager.Instance.SetColorData(GameManager.Instance.PlayerData.CurrentColor);
        }
        IsSetRoomData = false;
        yield return new WaitForSeconds(5f);
        DataManager.Instance.Save(DataManager.Instance.Index);//存档
        GameManager.Instance.PlayerData.CurrentRoomCount = 1;
        yield return new WaitForSeconds(0.5f);
        Fadecanvs.FadeOut();
        Door.SetActive(true);
        Door.GetComponent<Door>().SetDoor();
        ChangeMiniMapPositionEvent.RaiseVector3Event(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position);//改变小地图相机位置
        yield return new WaitForSeconds(1f);
        if (!IsSetRoomData)
        {
            ColorManager.Instance.SetColorText();
        }
        OpenDoorEvent.RaiseEvent();
        MapManager.Instance.AccessRoom(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position);
        MapManager.Instance.FindRoom(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position);
        KeyBoardManager.Instance.StopAnyKey = false;
        KeyBoardManager.Instance.StopMoveKey = false;
        GameManager.Instance.PlayerStats.gameObject.GetComponent<PlayerController>().ContinuePlayer();
        PlotManager.Instance.ThisRoomPlot.CurrentIndex = 0;
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
            StartCoroutine(OnStartGame());
        }
        else if(NextScene.SceneName != "BossMap")
        {
            yield return new WaitForSeconds(0.2f);
            Fadecanvs.FadeOut();
            yield return new WaitForSeconds(0.5f);
            GameManager.Instance.PlayerStats.gameObject.GetComponent<PlayerController>().ContinuePlayer();
            KeyBoardManager.Instance.StopAnyKey = false;
        }
        yield return null;
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
        if (GameManager.Instance.PlayerData.StartGame)
        {
            IsSetPosition = true;
        }
        if (!GameManager.Instance.PlayerData.StartGame)
        {
            Fadecanvs.FadeOut();
            yield return new WaitForSeconds(0.5f);
        }
        GameManager.Instance.PlayerStats.gameObject.GetComponent<PlayerController>().ContinuePlayer();
        KeyBoardManager.Instance.StopAnyKey = false;
        if(GameManager.Instance.PlayerData.StartGame)
        {
            MapManager.Instance.SetRoomData();
            GameManager.Instance.PlayerStats.gameObject.transform.position = GameManager.Instance.PlayerData.PlayerPosition;
            IsSetRoomData = true;
            StartCoroutine(OnChangeMap());
        }
        yield return null;
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
        Bosscanvs.gameObject.SetActive(false);
        GameManager.Instance.PlayerCanvs.gameObject.SetActive(false);
        GameManager.Instance.PlayerStats.CharacterData_Temp = Instantiate(GameManager.Instance.PlayerStats.CharacterData);
        ColorManager.Instance.CancelColorStats();
        MapManager.Instance.ClearMap();
        yield return new WaitForSeconds(0.1f);
        Boss.SetActive(false);
        GameManager.Instance.BossDeadEvent.RaiseEvent();
    }
    public void OpenStartCanvs()
    {
        Startcanvs.SetActive(true);
    }
    public void ChangeRoom(DoorType doorType, Vector3 Position)
    {
        StartCoroutine(OnChangeRoom(doorType,Position));
    }
    private IEnumerator OnChangeRoom(DoorType doorType,Vector3 Position)
    {
        Fadecanvs.FadeIn();
        KeyBoardManager.Instance.StopAnyKey = true;
        GameManager.Instance.Boss().NowHealth = GameManager.Instance.Boss().MaxHealth;
        switch (doorType)
        {
            case DoorType.LeftUpDoor:
                Player.transform.position = Position + new Vector3(-6,0,0);
                break;
            case DoorType.LeftDownDoor:
                Player.transform.position = Position + new Vector3(-6,0,0);
                break;
            case DoorType.RightUpDoor:
                Player.transform.position = Position + new Vector3(6, 0, 0);
                break;
            case DoorType.RightDownDoor:
                Player.transform.position = Position + new Vector3(6, 0, 0);
                break;
        }
        yield return new WaitForSeconds(0.1f);
        GameManager.Instance.PlayerData.RoomType = Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.GetComponent<MapCharacrter>().RoomType;
        DataManager.Instance.Save(DataManager.Instance.Index);//存档
        yield return new WaitForSeconds(0.4f);
        ChangeMiniMapPositionEvent.RaiseVector3Event(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position);//改变小地图相机位置
        switch (GameManager.Instance.PlayerData.RoomType)
        {
            case RoomType.StartRoom:
                break;
            case RoomType.NormalRoom:
                if (!MapManager.Instance.CheckAccessRoom(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position))
                {
                    Boss.transform.position = Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position + new Vector3(-15f, 0.97f, 0);
                    GameManager.Instance.AddBossHealth();
                    Boss.SetActive(true);
                    Bosscanvs.gameObject.SetActive(true);
                    ChangeBossSkillEvent.RaiseVector3Event(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position);
                    GameManager.Instance.BossActive = true;
                    Boss.GetComponent<BossController>().IsStopBoss = true;
                }
                break;
            case RoomType.CardRoom:
                break;
            case RoomType.BossRoom:
                if (!MapManager.Instance.CheckAccessRoom(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position))
                {
                    Boss.transform.position = Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position + new Vector3(-15f, 0.97f, 0);
                    GameManager.Instance.AddBossHealth();
                    Boss.SetActive(true);
                    Bosscanvs.gameObject.SetActive(true);
                    ChangeBossSkillEvent.RaiseVector3Event(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position);
                    GameManager.Instance.BossActive = true;
                    Boss.GetComponent<BossController>().IsStopBoss = true;
                    GameManager.Instance.RefreshBossSkill();
                }
                break;
            case RoomType.TransmissionTowerRoom:
                break;
            default:
                break;
        }
        Fadecanvs.FadeOut();
        MapManager.Instance.FindRoom(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position);
        yield return new WaitForSeconds(0.5f);
        switch (GameManager.Instance.PlayerData.RoomType)
        {
            case RoomType.StartRoom:
                OpenDoorEvent.RaiseEvent();
                MapManager.Instance.AccessRoom(Physics2D.OverlapPoint(Player.transform.position,Room).gameObject.transform.position);
                break;
            case RoomType.NormalRoom:
                if (!MapManager.Instance.CheckAccessRoom(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position))
                {
                    CloseDoorEvent.RaiseEvent();
                    PlotManager.Instance.SetRoomPlotText();
                    Boss.GetComponent<BossController>().IsStopBoss = false;
                }
                break;
            case RoomType.CardRoom:
                if (!MapManager.Instance.CheckAccessRoom(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position))
                {
                    CloseDoorEvent.RaiseEvent();
                }
                break;
            case RoomType.BossRoom:
                if (!MapManager.Instance.CheckAccessRoom(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position))
                {
                    CloseDoorEvent.RaiseEvent();
                    PlotManager.Instance.SetRoomPlotText();
                    Boss.GetComponent<BossController>().IsStopBoss = false;
                }
                break;
            case RoomType.TransmissionTowerRoom:
                OpenDoorEvent.RaiseEvent();
                MapManager.Instance.AccessRoom(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position);
                break;
            default:
                break;
        }
        KeyBoardManager.Instance.StopAnyKey = false;
    }
    private void SetChangeRoom()
    {
        KeyBoardManager.Instance.StopAnyKey = true;
        GameManager.Instance.Boss().NowHealth = GameManager.Instance.Boss().MaxHealth;
        switch (GameManager.Instance.PlayerData.RoomType)
        {
            case RoomType.StartRoom:
                MapManager.Instance.AccessRoom(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position);
                OpenDoorEvent.RaiseEvent();
                break;
            case RoomType.NormalRoom:
                if (!MapManager.Instance.CheckAccessRoom(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position))
                {
                    CloseDoorEvent.RaiseEvent();
                    Bosscanvs.gameObject.SetActive(true);
                    Boss.SetActive(true);
                    Boss.transform.position = Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position + new Vector3(-15f, 0.97f, 0);
                    ChangeBossSkillEvent.RaiseVector3Event(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position);
                    GameManager.Instance.BossActive = true;
                    Boss.GetComponent<BossController>().IsStopBoss = true;
                    PlotManager.Instance.SetRoomPlotText();
                    Boss.GetComponent<BossController>().IsStopBoss = false;
                }
                else if(MapManager.Instance.CheckAccessRoom(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position))
                {
                    OpenDoorEvent.RaiseEvent();
                }
                break;
            case RoomType.CardRoom:
                if (!MapManager.Instance.CheckAccessRoom(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position))
                {
                    CloseDoorEvent.RaiseEvent();
                }
                else if (MapManager.Instance.CheckAccessRoom(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position))
                {
                    OpenDoorEvent.RaiseEvent();
                }
                break;
            case RoomType.BossRoom:
                if (!MapManager.Instance.CheckAccessRoom(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position))
                {
                    CloseDoorEvent.RaiseEvent();
                    Bosscanvs.gameObject.SetActive(true);
                    Boss.SetActive(true);
                    Boss.transform.position = Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position + new Vector3(-15f, 0.97f, 0);
                    ChangeBossSkillEvent.RaiseVector3Event(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position);
                    GameManager.Instance.BossActive = true;
                    Boss.GetComponent<BossController>().IsStopBoss = true;
                    PlotManager.Instance.SetRoomPlotText();
                    Boss.GetComponent<BossController>().IsStopBoss = false;
                    GameManager.Instance.RefreshBossSkill();
                }
                else if (MapManager.Instance.CheckAccessRoom(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position))
                {
                    Bosscanvs.gameObject.SetActive(false);
                    OpenDoorEvent.RaiseEvent();
                }
                break;
            case RoomType.TransmissionTowerRoom:
                MapManager.Instance.AccessRoom(Physics2D.OverlapPoint(Player.transform.position, Room).gameObject.transform.position);
                OpenDoorEvent.RaiseEvent();
                break;
            default:
                break;
        }
        KeyBoardManager.Instance.StopAnyKey = false;
    }
}
