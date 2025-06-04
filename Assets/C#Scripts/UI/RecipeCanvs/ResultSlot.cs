using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSlot : MonoBehaviour
{
    public Image ResultData;
    public void SetResult(Sprite sprite)
    {
        ResultData.sprite = sprite;
    }
    private void Update()
    {
        if (ResultData.sprite == null)
        {
            ResultData.color = Color.clear;
        }
        else
        {
            ResultData.color = Color.white;
        }
    }
}
