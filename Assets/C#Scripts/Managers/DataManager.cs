using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine.Rendering;
public class DataManager : SingleTons<DataManager>
{
    public SceneData BaseScene;//��ʼ����
    public int Index;
    private BinaryFormatter formatter;
    private FileStream PlayerDataFile;//��һ��������ļ�
    private FileStream PlayerCharacterDataFile;//������������ļ�
    private FileStream BossCharacterDataFile;//Boss���������ļ�
    private FileStream BossSkillDataFile;//Boss���������ļ�
    private FileStream CardListDataFile;//���ƻ��������ļ�
    private FileStream PlotDataFile;//������������ļ�
    private FileStream MapDataFile;//��ͼ���������ļ�
    private FileStream FruitDataFile;//ˮ�������ļ�
    private FileStream PlayerRoomDataFile;//��ҷ��������ļ�
    private FileStream JuiceDataFile;//��֭�����ļ�
    private FileStream ItemListDataFile;//���������ļ�
    private FileStream GlobalDataFile;//ȫ�������ļ�
    [Header("����")]
    public PlayerData PlayerData;//��һ�������
    public CharacterData PlayerCharacterData;//�����������
    public CharacterData BossCharacterData;//Boss��������
    public BossSkillList BossSkillData;//Boss��������
    public ChooseCardList CardListData;//���ƻ�������
    public Plot PlotData;//�����������
    public MapData MapData;//��ͼ��������
    public FruitData FruitData;//ˮ������
    public PlayerRoomData PlayerRoomData;//��ҷ�������
    public JuiceData JuiceData;//��֭����
    public ItemList ItemListData;//��������
    public GlobalData  GlobalData;//ȫ������
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
        //���ļ���
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
        //ȫ�ֻ�������
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
            //ȫ�ֻ�������
        }
    }
    public void Save(int index)
    {
        MapManager.Instance.SaveItemList();
        if (!Directory.Exists(Application.persistentDataPath + "/SaveData"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData");
        }
        //���ļ���
        if(!Directory.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData" + "/Save" + index);
        }
        //�浵�ļ���
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
        //��һ�������
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
        //�����������
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
        //Boss��������
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
        //Boss��������
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
        //���ƻ�������
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
        //�����������
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
        //��ͼ��������
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
        //ˮ����������
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
        //��ҷ����������
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
        //��֭��������
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
        //���߻�������
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
            //��һ�������
            PlayerCharacterDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerCharacterData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(PlayerCharacterDataFile).ToString(), PlayerCharacterData);
            Json = JsonUtility.ToJson(PlayerCharacterData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), GameManager.Instance.PlayerStats.CharacterData_Temp);
            PlayerCharacterDataFile.Close();
            //�����������
            BossCharacterDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/BossCharacterData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(BossCharacterDataFile).ToString(), BossCharacterData);
            Json = JsonUtility.ToJson(BossCharacterData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), GameManager.Instance.BossStats.CharacterData_Temp);
            BossCharacterDataFile.Close();
            //Boss��������
            BossSkillDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/BossSkillData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(BossSkillDataFile).ToString(), BossSkillData);
            Json = JsonUtility.ToJson(BossSkillData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), GameManager.Instance.BossSkillList);
            BossSkillDataFile.Close();
            //Boss��������
            CardListDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/CardListData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(CardListDataFile).ToString(), CardListData);
            Json = JsonUtility.ToJson(CardListData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), CardManager.Instance.CardList);
            CardListDataFile.Close();
            //���ƻ�������
            PlotDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlotData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(PlotDataFile).ToString(), PlotData);
            Json = JsonUtility.ToJson(PlotData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), PlotManager.Instance.ThisRoomPlot);
            PlotDataFile.Close();
            //�����������
            MapDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/MapData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(MapDataFile).ToString(), MapData);
            Json = JsonUtility.ToJson(MapData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), MapManager.Instance.MapData);
            MapDataFile.Close();
            //��ͼ��������
            FruitDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/FruitData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(FruitDataFile).ToString(), FruitData);
            Json = JsonUtility.ToJson(FruitData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), FruitManager.Instance.Fruitdata);
            FruitDataFile.Close();
            //��ͼ��������
            PlayerRoomDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerRoomData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(PlayerRoomDataFile).ToString(), PlayerRoomData);
            Json = JsonUtility.ToJson(PlayerRoomData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), SceneChangeManager.Instance.PlayerRoomData);
            PlayerRoomDataFile.Close();
            //��ҷ����������
            JuiceDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/JuiceData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(JuiceDataFile).ToString(), JuiceData);
            Json = JsonUtility.ToJson(JuiceData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), FruitManager.Instance.Juicedata);
            JuiceDataFile.Close();
            //��֭��������
            ItemListDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/ItemListData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(ItemListDataFile).ToString(), ItemListData);
            Json = JsonUtility.ToJson(ItemListData);
            JsonUtility.FromJsonOverwrite(Json.ToString(), MapManager.Instance.itemList);
            ItemListDataFile.Close();
            //���߻�������
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
            //�ָ����ݳ�ʼ��(�ж���ǰ�浵Ϊ��ʱʹ��)
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
        //�����ǰ�浵
    }
}
