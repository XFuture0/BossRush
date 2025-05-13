using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wardrobe : MonoBehaviour
{
    private RTip R_Tip;
    private void Awake()
    {
        R_Tip = GetComponent<RTip>();
    }
    private void Update()
    {
        if (R_Tip.IsPlayer && KeyBoardManager.Instance.GetKeyDown_R())
        {
            PlayerEquipManager.Instance.OpenEquipCanvs(0);
        }
    }
}
