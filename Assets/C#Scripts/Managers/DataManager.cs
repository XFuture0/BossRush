using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine.Rendering;
public class DataManager : SingleTons<DataManager>
{
    public SceneData BaseScene;//初始场景
    public int Index;
    private BinaryFormatter formatter;
    private FileStream PlayerDataFile;//玩家基本数据文件
    private FileStream PlayerCharacterDataFile;//玩家属性数据文件
    private FileStream BossCharacterDataFile;//Boss属性数据文件
    private FileStream BossSkillDataFile;//Boss技能数据文件
    private FileStream CardListDataFile;//卡牌基本数据文件
    private FileStream PlotDataFile;//剧情基本数据文件
    private FileStream MapDataFile;//地图基本数据文件
    private FileStream FruitDataFile;//水果数据文件
    private FileStream PlayerRoomDataFile;//玩家房间数据文件
    private FileStream JuiceDataFile;//果汁数据文件
    private FileStream ItemListDataFile;//道具数据文件
    private FileStream GlobalDataFile;//全局数据文件
    [Header("数据")]
    public PlayerData PlayerData;//玩家基本数据
    public CharacterData PlayerCharacterData;//玩家属性数据
    public CharacterData BossCharacterData;//Boss属性数据
    public BossSkillList BossSkillData;//Boss技能数据
    public ChooseCardList CardListData;//卡牌基本数据
    public Plot PlotData;//剧情基本数据
    public MapData MapData;//地图基本数据
    public FruitData FruitData;//水果数据
    public PlayerRoomData PlayerRoomData;//玩家房间数据
    public JuiceData JuiceData;//果汁数据
    public ItemList ItemListData;//道具数据
    public GlobalData  GlobalData;//全局数据
    protected override void Awake()
    {
        base.Awake();
        formatter = new BinaryFormatter();
    }
    private void Start()
    {
        LoadGlobal();
    }
    public void SaveGlobal()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/SaveData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData");
        }
        //总文件夹
        var Json = JsonUtility.ToJson(GameManager.Instance.GlobalData);
        JsonUtility.FromJsonOverwrite(Json.ToString(), GlobalData);
        if (!File.Exists(Application.persistentDataPath + "/SaveData" + "/GlobalData.txt"))
        {
            File.Create(Application.persistentDataPath + "/SaveData" + "/GlobalData.txt").Dispose();
        }
        GlobalDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/GlobalData.txt", FileMode.Open);
        Json = JsonUtility.ToJson(GlobalData);
        formatter.Serialize(GlobalDataFile, Json);
        GlobalDataFile.Close();
        //全局基本数据
    }
    private void LoadGlobal()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData" + "/GlobalData.txt"))
        {
            GlobalDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/GlobalData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(GlobalDataFile).ToString(), GlobalData);
            var Json = JsonUtility.ToJson(GlobalData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), GameManager.Instance.GlobalData);
            GlobalDataFile.Close();
            AudioManager.Instance.SetMainAudioVolume();
            //全局基本数据
        }
    }
    public void Save(int index)
    {
        MapManager.Instance.SaveItemList();
        if (!Directory.Exists(Application.persistentDataPath + "/SaveData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData");
        }
        //总文件夹
        if(!Directory.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData" + "/Save" + index);
        }
        //存档文件夹
        var Json = JsonUtility.ToJson(GameManager.Instance.PlayerData);
        JsonUtility.FromJsonOverwrite(Json.ToString(), PlayerData);
        if (!File.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerData.txt"))
        {
            File.Create(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerData.txt").Dispose();
        }
        PlayerDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerData.txt", FileMode.Open);
        Json = JsonUtility.ToJson(PlayerData);
        formatter.Serialize(PlayerDataFile, Json);
        PlayerDataFile.Close();
        //玩家基本数据
        Json = JsonUtility.ToJson(GameManager.Instance.PlayerStats.CharacterData_Temp);
        JsonUtility.FromJsonOverwrite(Json.ToString(), PlayerCharacterData);
        if (!File.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerCharacterData.txt"))
        {
            File.Create(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerCharacterData.txt").Dispose();
        }
        PlayerCharacterDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerCharacterData.txt", FileMode.Open);
        Json = JsonUtility.ToJson(PlayerCharacterData);
        formatter.Serialize(PlayerCharacterDataFile, Json);
        PlayerCharacterDataFile.Close();
        //玩家属性数据
        Json = JsonUtility.ToJson(GameManager.Instance.BossStats.CharacterData_Temp);
        JsonUtility.FromJsonOverwrite(Json.ToString(), BossCharacterData);
        if (!File.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/BossCharacterData.txt"))
        {
            File.Create(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/BossCharacterData.txt").Dispose();
        }
        BossCharacterDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/BossCharacterData.txt", FileMode.Open);
        Json = JsonUtility.ToJson(BossCharacterData);
        formatter.Serialize(BossCharacterDataFile, Json);
        BossCharacterDataFile.Close();
        //Boss属性数据
        Json = JsonUtility.ToJson(GameManager.Instance.BossSkillList);
        JsonUtility.FromJsonOverwrite(Json.ToString(), BossSkillData);
        if (!File.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/BossSkillData.txt"))
        {
            File.Create(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/BossSkillData.txt").Dispose();
        }
        BossSkillDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/BossSkillData.txt", FileMode.Open);
        Json = JsonUtility.ToJson(BossSkillData);
        formatter.Serialize(BossSkillDataFile, Json);
        BossSkillDataFile.Close();
        //Boss技能数据
        Json = JsonUtility.ToJson(CardManager.Instance.CardList);
        JsonUtility.FromJsonOverwrite(Json.ToString(), CardListData);
        if (!File.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/CardListData.txt"))
        {
            File.Create(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/CardListData.txt").Dispose();
        }
        CardListDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/CardListData.txt", FileMode.Open);
        Json = JsonUtility.ToJson(CardListData);
        formatter.Serialize(CardListDataFile, Json);
        CardListDataFile.Close();
        //卡牌基本数据
        Json = JsonUtility.ToJson(PlotManager.Instance.ThisRoomPlot);
        JsonUtility.FromJsonOverwrite(Json.ToString(), PlotData);
        if (!File.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlotData.txt"))
        {
            File.Create(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlotData.txt").Dispose();
        }
        PlotDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlotData.txt", FileMode.Open);
        Json = JsonUtility.ToJson(PlotData);
        formatter.Serialize(PlotDataFile, Json);
        PlotDataFile.Close();
        //剧情基本数据
        Json = JsonUtility.ToJson(MapManager.Instance.MapData);
        JsonUtility.FromJsonOverwrite(Json.ToString(), MapData);
        if (!File.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/MapData.txt"))
        {
            File.Create(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/MapData.txt").Dispose();
        }
        MapDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/MapData.txt", FileMode.Open);
        Json = JsonUtility.ToJson(MapData);
        formatter.Serialize(MapDataFile, Json);
        MapDataFile.Close();
        //地图基本数据
        Json = JsonUtility.ToJson(FruitManager.Instance.Fruitdata);
        JsonUtility.FromJsonOverwrite(Json.ToString(), FruitData);
        if (!File.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/FruitData.txt"))
        {
            File.Create(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/FruitData.txt").Dispose();
        }
        FruitDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/FruitData.txt", FileMode.Open);
        Json = JsonUtility.ToJson(FruitData);
        formatter.Serialize(FruitDataFile, Json);
        FruitDataFile.Close();
        //水果基本数据
        Json = JsonUtility.ToJson(SceneChangeManager.Instance.PlayerRoomData);
        JsonUtility.FromJsonOverwrite(Json.ToString(), PlayerRoomData);
        if (!File.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerRoomData.txt"))
        {
            File.Create(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerRoomData.txt").Dispose();
        }
        PlayerRoomDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerRoomData.txt", FileMode.Open);
        Json = JsonUtility.ToJson(PlayerRoomData);
        formatter.Serialize(PlayerRoomDataFile, Json);
        PlayerRoomDataFile.Close();
        //玩家房间基本数据
        Json = JsonUtility.ToJson(FruitManager.Instance.Juicedata);
        JsonUtility.FromJsonOverwrite(Json.ToString(), JuiceData);
        if (!File.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/JuiceData.txt"))
        {
            File.Create(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/JuiceData.txt").Dispose();
        }
        JuiceDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/JuiceData.txt", FileMode.Open);
        Json = JsonUtility.ToJson(JuiceData);
        formatter.Serialize(JuiceDataFile, Json);
        JuiceDataFile.Close();
        //果汁基本数据
        Json = JsonUtility.ToJson(MapManager.Instance.itemList);
        JsonUtility.FromJsonOverwrite(Json.ToString(), ItemListData);
        if (!File.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/ItemListData.txt"))
        {
            File.Create(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/ItemListData.txt").Dispose();
        }
        ItemListDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/ItemListData.txt", FileMode.Open);
        Json = JsonUtility.ToJson(ItemListData);
        formatter.Serialize(ItemListDataFile, Json);
        ItemListDataFile.Close();
        //道具基本数据
    }
    public void Load(int index)
    {
        Index = index;
        if (Directory.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index))
        {
            PlayerDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(PlayerDataFile).ToString(), PlayerData);
            var Json = JsonUtility.ToJson(PlayerData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), GameManager.Instance.PlayerData);
            PlayerDataFile.Close();
            //玩家基本数据
            PlayerCharacterDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerCharacterData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(PlayerCharacterDataFile).ToString(), PlayerCharacterData);
            Json = JsonUtility.ToJson(PlayerCharacterData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), GameManager.Instance.PlayerStats.CharacterData_Temp);
            PlayerCharacterDataFile.Close();
            //玩家属性数据
            BossCharacterDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/BossCharacterData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(BossCharacterDataFile).ToString(), BossCharacterData);
            Json = JsonUtility.ToJson(BossCharacterData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), GameManager.Instance.BossStats.CharacterData_Temp);
            BossCharacterDataFile.Close();
            //Boss属性数据
            BossSkillDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/BossSkillData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(BossSkillDataFile).ToString(), BossSkillData);
            Json = JsonUtility.ToJson(BossSkillData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), GameManager.Instance.BossSkillList);
            BossSkillDataFile.Close();
            //Boss技能数据
            CardListDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/CardListData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(CardListDataFile).ToString(), CardListData);
            Json = JsonUtility.ToJson(CardListData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), CardManager.Instance.CardList);
            CardListDataFile.Close();
            //卡牌基本数据
            PlotDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlotData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(PlotDataFile).ToString(), PlotData);
            Json = JsonUtility.ToJson(PlotData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), PlotManager.Instance.ThisRoomPlot);
            PlotDataFile.Close();
            //剧情基本数据
            MapDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/MapData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(MapDataFile).ToString(), MapData);
            Json = JsonUtility.ToJson(MapData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), MapManager.Instance.MapData);
            MapDataFile.Close();
            //地图基本数据
            FruitDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/FruitData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(FruitDataFile).ToString(), FruitData);
            Json = JsonUtility.ToJson(FruitData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), FruitManager.Instance.Fruitdata);
            FruitDataFile.Close();
            //地图基本数据
            PlayerRoomDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerRoomData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(PlayerRoomDataFile).ToString(), PlayerRoomData);
            Json = JsonUtility.ToJson(PlayerRoomData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), SceneChangeManager.Instance.PlayerRoomData);
            PlayerRoomDataFile.Close();
            //玩家房间基本数据
            JuiceDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/JuiceData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(JuiceDataFile).ToString(), JuiceData);
            Json = JsonUtility.ToJson(JuiceData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), FruitManager.Instance.Juicedata);
            JuiceDataFile.Close();
            //果汁基本数据
            ItemListDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/ItemListData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(ItemListDataFile).ToString(), ItemListData);
            Json = JsonUtility.ToJson(ItemListData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), MapManager.Instance.itemList);
            ItemListDataFile.Close();
            //道具基本数据
        }
        if (!Directory.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index))
        {
            GameManager.Instance.PlayerData.CurrentRoomCount = 0;
            GameManager.Instance.PlayerData.CurrentScene = BaseScene;
            GameManager.Instance.PlayerData.StartGame = false;
            GameManager.Instance.PlayerData.CoinCount = 0;
            GameManager.Instance.PlayerData.JumpForce = 18;
            PlayerEquipManager.Instance.ChangeWeapon(0);
            PlayerEquipManager.Instance.ChangeHat(0);
            PlayerEquipManager.Instance.ChangeCharacter(0);
            ColorManager.Instance.ReSetColor();
            MapManager.Instance.ClearMap();
            MapManager.Instance.ClearItemList();
            FruitManager.Instance.ClearFruit();
            SceneChangeManager.Instance.ClearPlayerRoom();
            //恢复数据初始化(判定当前存档为空时使用)
        }
        SceneChangeManager.Instance.LoadGame();
    }
    public void Delete(int index)
    {
        if (Directory.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index))
        {
            foreach (var file in Directory.GetFiles(Application.persistentDataPath + "/SaveData" + "/Save" + index))
            {
                File.Delete(file);
            }
            Directory.Delete(Application.persistentDataPath + "/SaveData" + "/Save" + index);
        }
        //清除当前存档
    }
}
