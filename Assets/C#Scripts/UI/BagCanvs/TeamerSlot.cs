using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamerSlot : MonoBehaviour
{
    public Image PlayerImage;
    public Image[] GemSlots;
    [Header("±¦Ê¯")]
    public GameObject WeaponGem;
    public GameObject ShootGem;
    public GameObject DamageGem;
    public GameObject SpeedGem;
    public GameObject BiggerGem;
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
    public void RefreshGem(int Count,ExtraGemData extraGemData)
    {
        for(int i = 0; i < Count + extraGemData.ExtraGemList.Count; i++)
        {
            if(i < Count)
            {
                Instantiate(WeaponGem, GemSlots[i].transform);
            }
            else if(i >= Count)
            {
                switch(extraGemData.ExtraGemList[i - Count].GemType)
                {
                    case GemType.ShootGem:
                        var NewShootGem = Instantiate(ShootGem, GemSlots[i].transform);
                        NewShootGem.GetComponent<WeaponGemDrag>().ThisExtraGem.GemBonus = extraGemData.ExtraGemList[i - Count].GemBonus;
                        break;
                    case GemType.DamageGem:
                        var NewDamageGem = Instantiate(DamageGem, GemSlots[i].transform);
                        NewDamageGem.GetComponent<WeaponGemDrag>().ThisExtraGem.GemBonus = extraGemData.ExtraGemList[i - Count].GemBonus;
                        break;
                    case GemType.SpeedGem:
                        var NewSpeedGem = Instantiate(SpeedGem, GemSlots[i].transform);
                        NewSpeedGem.GetComponent<WeaponGemDrag>().ThisExtraGem.GemBonus = extraGemData.ExtraGemList[i - Count].GemBonus;
                        break;
                    case GemType.BiggerGem:
                        var NewBiggerGem = Instantiate(BiggerGem, GemSlots[i].transform);
                        NewBiggerGem.GetComponent<WeaponGemDrag>().ThisExtraGem.GemBonus = extraGemData.ExtraGemList[i - Count].GemBonus;
                        break;
                }
            }
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
