using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New ItemList", menuName = "List/ItemList")]

public class ItemList : ScriptableObject
{
    [System.Serializable]
    public class Item
    {
        public ItemType ItemType;
        public Vector3 ItemPosition;
        public int Index;
    }
    public List<Item> ItemLists = new List<Item>();
}
