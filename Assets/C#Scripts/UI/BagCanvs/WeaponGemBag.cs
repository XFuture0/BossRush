using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGemBag : MonoBehaviour
{
    public GameObject FreeGemSlot;
    public GameObject EmptyGemSlot;
    public Transform WeaponGemSlotBox;
    private void OnEnable()
    {
        for (int i = 0; i < GameManager.Instance.PlayerData.FreeWeaponSlotCount; i++)
        {
            Instantiate(FreeGemSlot, WeaponGemSlotBox);
        }
        for (int i = 0; i < GameManager.Instance.PlayerData.EmptyWeaponSlotCount; i++)
        {
            Instantiate(EmptyGemSlot, WeaponGemSlotBox);
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < WeaponGemSlotBox.childCount; i++)
        {
            Destroy(WeaponGemSlotBox.GetChild(i).gameObject);
        }
    }
}
