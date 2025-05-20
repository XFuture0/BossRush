using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagCanvs : MonoBehaviour
{
    public Button LeftChangeButton;
    public Button RightChangeButton;
    public List<GameObject> BagLists = new List<GameObject>();
    private int Index;
    private void Awake()
    {
        LeftChangeButton.onClick.AddListener(OnLeftChange);
        RightChangeButton.onClick.AddListener(OnRightChange);
        for (int i = 0; i < BagLists.Count; i++)
        {
            BagLists[i].SetActive(false);
        }
        BagLists[0].SetActive(true);//±³°ü³õÊ¼»¯
        Index = 0;
    }
    private void OnLeftChange()
    {
        if(Index - 1 >= 0)
        {
            for (int i = 0; i < BagLists.Count; i++)
            {
                BagLists[i].SetActive(false);
            }
            Index--;
            BagLists[Index].SetActive(true);
        }
    }
    private void OnRightChange()
    {
        if (Index + 1 < BagLists.Count)
        {
            for (int i = 0; i < BagLists.Count; i++)
            {
                BagLists[i].SetActive(false);
            }
            Index++;
            BagLists[Index].SetActive(true);
        }
    }
}
