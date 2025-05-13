using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    public List<ItemSlot> ItemSlots = new List<ItemSlot>();
    private void Start()
    {
        RefreshIndex();
    }
    private void RefreshIndex()
    {
        for (int i = 0; i < ItemSlots.Count; i++)
        {
            ItemSlots[i].itemData.Index = i;
            ItemSlots[i].CheckOpen();
        }
    }
}
