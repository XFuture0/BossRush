using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveCanvs : MonoBehaviour
{
    public List<SaveSlot> saveSlots = new List<SaveSlot>();
    private void Awake()
    {
        RefreshIndex();
    }
    private void RefreshIndex()
    {
        for(int i = 1; i <= saveSlots.Count; i++)
        {
            saveSlots[i-1].Index = i;
        }
    }
}
