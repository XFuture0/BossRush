using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraGem : MonoBehaviour
{
    public GemType ThisGemType;
    public float GemBonus;
    public GameObject RKey;
    private bool IsPlayer;
    private void OnEnable()
    {
        RefreshBonus();
    }
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
    private void RefreshBonus()
    {
        switch (ThisGemType)
        {
            case GemType.ShootGem:
                GemBonus = UnityEngine.Random.Range(0f, 1f);
                break;
            case GemType.DamageGem:
                GemBonus = UnityEngine.Random.Range(0f, 1f);
                break;
            case GemType.SpeedGem:
                GemBonus = UnityEngine.Random.Range(0f, 0.2f);
                break;
            case GemType.BiggerGem:
                GemBonus = UnityEngine.Random.Range(0f, 1f);
                break;
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
