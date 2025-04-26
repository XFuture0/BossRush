using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
public class DataManager : SingleTons<DataManager>
{
    public int Index;
    [Header("Êý¾Ý")]
    public PlayerData PlayerData;
    private BinaryFormatter formatter;
    private FileStream ThisFile;
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
        if(!Directory.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData" + "/Save" + index);
        }
        if (!File.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerSaveData.txt"))
        {
            ThisFile = File.Create(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerSaveData.txt");
        }
        else
        {
            ThisFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerSaveData.txt", FileMode.Open);
        }
        var Json = JsonUtility.ToJson(PlayerData);
        formatter.Serialize(ThisFile, Json);
        ThisFile.Close();
    }
    public void Load(int index)
    {
        Index = index;
        if (Directory.Exists(Application.persistentDataPath + "/SaveData" + "/Save" + index))
        {
            ThisFile = File.Open(Application.persistentDataPath + "/SaveData" + "/Save" + index + "/PlayerSaveData.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite(formatter.Deserialize(ThisFile).ToString(), PlayerData);
            GameManager.Instance.PlayerStats.gameObject.transform.position = PlayerData.PlayerPosition;
            ThisFile.Close();
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
    }
}
