using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ExtraGemList", menuName = "List/ExtraGemList")]

public class ExtraGemData : ScriptableObject
{
    [System.Serializable]
    public class ExtraGem
    { 
        public GemType GemType;
        public float GemBonus;
    }
    public int EmptyGemSlotCount;
    public List<ExtraGem> ExtraGemList = new List<ExtraGem>();
}
