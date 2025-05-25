using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GlobalData", menuName = "Data/GlobalData")]
public class GlobalData : ScriptableObject
{
    [System.Serializable]
    public class SaveTime
    {
        public int Index;
        public string Time;
    }
    public float AudioVolume;//音量
    public SaveTime SaveTime1;//保存时间1
    public SaveTime SaveTime2;//保存时间2
    public SaveTime SaveTime3;//保存时间3
}
