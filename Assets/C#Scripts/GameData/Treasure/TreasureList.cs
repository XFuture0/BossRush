using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New TreasureList", menuName = "List/TreasureList")]
public class TreasureList : ScriptableObject
{
    [System.Serializable]
    public class Treasure
    {
        public GameObject ItemObject;
        public float Probability;
    }
    public List<Treasure> TreasureLists = new List<Treasure>();
}
