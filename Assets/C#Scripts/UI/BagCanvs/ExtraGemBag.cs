using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraGemBag : MonoBehaviour
{
    public GameObject EmptyGemSlot;
    public Transform ExtraGemSlotBox;
    [Header("±¦Ê¯ÁÐ±í")]
    public GameObject ShootGem;
    public GameObject DamageGem;
    public GameObject SpeedGem;
    public GameObject BiggerGem;
    public GameObject WeaponGem;
    private void OnEnable()
    {
        foreach (var ExtraGem in GameManager.Instance.PlayerData.ExtraGemData.ExtraGemList)
        {
            switch (ExtraGem.GemType) 
            {
                case GemType.ShootGem:
                    var NewShootGem = Instantiate(ShootGem, ExtraGemSlotBox);
                    NewShootGem.transform.GetChild(1).gameObject.GetComponent<WeaponGemDrag>().ThisExtraGem = ExtraGem;
                    break;
                case GemType.DamageGem:
                    var NewDamageGem = Instantiate(DamageGem, ExtraGemSlotBox);
                    NewDamageGem.transform.GetChild(1).gameObject.GetComponent<WeaponGemDrag>().ThisExtraGem = ExtraGem;
                    break;
                case GemType.SpeedGem:
                    var NewSpeedGem = Instantiate(SpeedGem, ExtraGemSlotBox);
                    NewSpeedGem.transform.GetChild(1).gameObject.GetComponent<WeaponGemDrag>().ThisExtraGem = ExtraGem;
                    break;
                case GemType.BiggerGem:
                    var NewBiggerGem = Instantiate(BiggerGem, ExtraGemSlotBox);
                    NewBiggerGem.transform.GetChild(1).gameObject.GetComponent<WeaponGemDrag>().ThisExtraGem = ExtraGem;
                    break;
            }
        }
        for (int i = 0; i < GameManager.Instance.PlayerData.ExtraGemData.EmptyGemSlotCount; i++)
        {
            Instantiate(EmptyGemSlot, ExtraGemSlotBox);
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < ExtraGemSlotBox.childCount; i++)
        {
            Destroy(ExtraGemSlotBox.GetChild(i).gameObject);
        }
    }
}
