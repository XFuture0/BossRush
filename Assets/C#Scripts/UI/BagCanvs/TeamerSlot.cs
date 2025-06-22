using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamerSlot : MonoBehaviour
{
    public Image PlayerImage;
    public Image[] GemSlots;
    public GameObject WeaponGem;
    private void OnEnable()
    {
        RefreshImage();
    }
    private void RefreshImage()
    {
        var index = transform.GetSiblingIndex();
        switch (index)
        {
            case 0:
                PlayerImage.sprite = GameManager.Instance.PlayerData.Teamer3.SlimeSprite;
                break;
            case 1:
                PlayerImage.sprite = GameManager.Instance.PlayerData.Teamer2.SlimeSprite;
                break;
            case 2:
                PlayerImage.sprite = GameManager.Instance.PlayerData.Teamer1.SlimeSprite;
                break;
            case 3:
                PlayerImage.sprite = GameManager.Instance.PlayerData.Player.SlimeSprite;
                break;
        }
    }
    public void RefreshGem(int Count)
    {
        for(int i = 0; i < Count; i++)
        {
            Instantiate(WeaponGem, GemSlots[i].transform);
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < GemSlots.Length; i++)
        {
            if (GemSlots[i].transform.childCount > 1)
            {
                Destroy(GemSlots[i].transform.GetChild(1).gameObject);
            }
        }
    }
}
