using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
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
    [Header("����")]
    public PlayerData PlayerData;//��һ�������
    public CharacterData PlayerCharacterData;//�����������
    public CharacterData BossCharacterData;//Boss��������
    public BossSkillList BossSkillData;//Boss��������
    public ChooseCardList CardListData;//���ƻ�������
    public Plot PlotData;//�����������
    protected override void Awake()
    {
        base.Awake();
        formatter = new BinaryFormatter();
    }
    public void Save(int index)
    {
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
        PlayerData = GameManager.Instance.PlayerData;
        if (!File.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerData.txt"))
        {
            File.Create(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerData.txt").Dispose();
        }
        PlayerDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerData.txt", FileMode.Open);
        var Json = JsonUtility.ToJson(PlayerData);
        formatter.Serialize(PlayerDataFile, Json);
        PlayerDataFile.Close();
        //��һ�������
        PlayerCharacterData = GameManager.Instance.PlayerStats.CharacterData_Temp;
        if (!File.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerCharacterData.txt"))
        {
            File.Create(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerCharacterData.txt").Dispose();
        }
        PlayerCharacterDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerCharacterData.txt", FileMode.Open);
        Json = JsonUtility.ToJson(PlayerCharacterData);
        formatter.Serialize(PlayerCharacterDataFile, Json);
        PlayerCharacterDataFile.Close();
        //�����������
        BossCharacterData = GameManager.Instance.BossStats.CharacterData_Temp;
        if (!File.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/BossCharacterData.txt"))
        {
            File.Create(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/BossCharacterData.txt").Dispose();
        }
        BossCharacterDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/BossCharacterData.txt", FileMode.Open);
        Json = JsonUtility.ToJson(BossCharacterData);
        formatter.Serialize(BossCharacterDataFile, Json);
        BossCharacterDataFile.Close();
        //Boss��������
        BossSkillData = GameManager.Instance.BossSkillList;
        if (!File.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/BossSkillData.txt"))
        {
            File.Create(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/BossSkillData.txt").Dispose();
        }
        BossSkillDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/BossSkillData.txt", FileMode.Open);
        Json = JsonUtility.ToJson(BossSkillData);
        formatter.Serialize(BossSkillDataFile, Json);
        BossSkillDataFile.Close();
        //Boss��������
        CardListData = CardManager.Instance.CardList;
        if (!File.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/CardListData.txt"))
        {
            File.Create(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/CardListData.txt").Dispose();
        }
        CardListDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/CardListData.txt", FileMode.Open);
        Json = JsonUtility.ToJson(CardListData);
        formatter.Serialize(CardListDataFile, Json);
        CardListDataFile.Close();
        //���ƻ�������
        PlotData = PlotManager.Instance.ThisRoomPlot;
        if (!File.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlotData.txt"))
        {
            File.Create(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlotData.txt").Dispose();
        }
        PlotDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlotData.txt", FileMode.Open);
        Json = JsonUtility.ToJson(PlotData);
        formatter.Serialize(PlotDataFile, Json);
        PlotDataFile.Close();
        //�����������
    }
    public void Load(int index)
    {
        Index = index;
        if (Directory.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index))
        {
            PlayerDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(PlayerDataFile).ToString(), PlayerData);
            GameManager.Instance.PlayerData = PlayerData;
            PlayerDataFile.Close();
            //��һ�������
            PlayerCharacterDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerCharacterData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(PlayerCharacterDataFile).ToString(), PlayerCharacterData);
            GameManager.Instance.PlayerStats.CharacterData_Temp = PlayerCharacterData;
            PlayerCharacterDataFile.Close();
            //�����������
            BossCharacterDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/BossCharacterData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(BossCharacterDataFile).ToString(), BossCharacterData);
            GameManager.Instance.BossStats.CharacterData_Temp = BossCharacterData;
            BossCharacterDataFile.Close();
            //Boss��������
            BossSkillDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/BossSkillData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(BossSkillDataFile).ToString(), BossSkillData);
            GameManager.Instance.BossSkillList = BossSkillData;
            BossSkillDataFile.Close();
            //Boss��������
            CardListDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/CardListData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(CardListDataFile).ToString(), CardListData);
            CardManager.Instance.CardList = CardListData;
            CardListDataFile.Close();
            //���ƻ�������
            PlotDataFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlotData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(PlotDataFile).ToString(), PlotData);
            PlotManager.Instance.ThisRoomPlot = PlotData;
            PlotDataFile.Close();
            //�����������
        }
        if (!Directory.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index))
        {
            GameManager.Instance.PlayerData.CurrentRoomCount = 0;
            GameManager.Instance.PlayerData.CurrentScene = BaseScene;
            GameManager.Instance.PlayerData.StartGame = false;
            PlayerEquipManager.Instance.ChangeWeapon(0);
            PlayerEquipManager.Instance.ChangeHat(0);
            PlayerEquipManager.Instance.ChangeCharacter(0);
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
