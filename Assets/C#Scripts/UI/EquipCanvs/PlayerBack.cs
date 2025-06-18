using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBack : MonoBehaviour
{
    public Sprite SlimeSlot;
    [Header("小队成员")]
    public GameObject Player;
    public GameObject Teamer1;
    public GameObject Teamer2;
    public GameObject Teamer3;
    private void OnEnable()
    {
        RefreshTeam();
    }
    private void RefreshTeam()
    {
        Player.GetComponent<Image>().sprite = GameManager.Instance.PlayerData.Player.SlimeSprite;
        if(GameManager.Instance.PlayerData.Teamer1 == null)
        {
            Teamer1.GetComponent<Image>().sprite = SlimeSlot;
        }
        else
        {
            Teamer1.GetComponent<Image>().sprite = GameManager.Instance.PlayerData.Teamer1.SlimeSprite;
        }
        if(GameManager.Instance.PlayerData.Teamer2 == null)
        {
            Teamer2.GetComponent<Image>().sprite = SlimeSlot;
        }
        else
        {
            Teamer2.GetComponent<Image>().sprite = GameManager.Instance.PlayerData.Teamer2.SlimeSprite;
        }
        if(GameManager.Instance.PlayerData.Teamer3 == null)
        {
            Teamer3.GetComponent<Image>().sprite = SlimeSlot;
        }
        else
        {
            Teamer3.GetComponent<Image>().sprite = GameManager.Instance.PlayerData.Teamer3.SlimeSprite;
        }
    }
}
