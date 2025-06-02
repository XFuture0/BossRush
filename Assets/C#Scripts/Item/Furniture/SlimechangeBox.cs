using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    private bool IsPlayer;
    public GameObject RKey;
    private void Update()
    {
        if (IsPlayer && KeyBoardManager.Instance.GetKeyDown_R())
        {
            PlayerEquipManager.Instance.OpenEquipCanvs(1);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            IsPlayer = true;
            RKey.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            IsPlayer = false;
            RKey.SetActive(false);
        }
    }
}
