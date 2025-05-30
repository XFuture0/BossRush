using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveCanvs : MonoBehaviour
{
    public Button ReturnButton;
    public GameObject MainaMenuCanvs;
    public List<SaveSlot> saveSlots = new List<SaveSlot>();
    private void Awake()
    {
        RefreshIndex();
        ReturnButton.onClick.AddListener(OnReturnButton);
    }
    private void RefreshIndex()
    {
        for(int i = 1; i <= saveSlots.Count; i++)
        {
            saveSlots[i-1].Index = i;
        }
    }
    private void OnReturnButton()
    {
        MainaMenuCanvs.SetActive(true);
        gameObject.SetActive(false);
    }
}
