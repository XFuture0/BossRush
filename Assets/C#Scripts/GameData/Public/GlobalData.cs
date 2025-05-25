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
    public float AudioVolume;//����
    public SaveTime SaveTime1;//����ʱ��1
    public SaveTime SaveTime2;//����ʱ��2
    public SaveTime SaveTime3;//����ʱ��3
}
