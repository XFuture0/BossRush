using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DataManager : SingleTons<DataManager>
{
    public void Save(Object SaveData, string SaveKey)
    {
        var jsonData = JsonUtility.ToJson(SaveData);
        PlayerPrefs.SetString(SaveKey, jsonData);
        PlayerPrefs.Save();
    }
    public void Load(Object SaveData, string SaveKey)
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(SaveKey), SaveData);
        }
    }
}
