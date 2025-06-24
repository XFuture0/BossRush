using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraGem : MonoBehaviour
{
    public GemType ThisGemType;
    public float GemBonus;
    public GameObject RKey;
    private bool IsPlayer;
    private void Update()
    {
        if(IsPlayer && KeyBoardManager.Instance.GetKeyDown_R())
        {
            IsPlayer = false;
            var NewExtraGem = new ExtraGemData.ExtraGem();
            NewExtraGem.GemType = ThisGemType;
            NewExtraGem.GemBonus = GemBonus;
            GameManager.Instance.PlayerData.ExtraGemData.ExtraGemList.Add(NewExtraGem);
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            IsPlayer = true;
            RKey.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            IsPlayer = false;
            RKey.SetActive(false);
        }
    }
}
