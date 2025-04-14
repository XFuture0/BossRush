using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Hat List", menuName = "List/Hat List")]
public class HatList : ScriptableObject
{
    [System.Serializable]
    public class HatData
    {
        public string HatName;
    }
    public List<HatData> HatDatas = new List<HatData>();
}
