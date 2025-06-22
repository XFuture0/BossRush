using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamBag : MonoBehaviour
{
    public GameObject FreeGemSlot;
    public GameObject EmptyGemSlot;
    public Transform GemSlotBox;
    [Header("∂”ŒÈ–≈œ¢")]
    public GameObject TeamSlot;
    public GameObject TeamSlot1;
    public GameObject TeamSlot2;
    public GameObject TeamSlot3;
    public GameObject GemBag;
    private void OnEnable()
    {
        CheckSlime();
    }
    private void CheckSlime()
    {
        if(GameManager.Instance.PlayerData.Player != null)
        {
            TeamSlot3.SetActive(true);
            TeamSlot3.GetComponent<TeamerSlot>().RefreshGem(GameManager.Instance.PlayerData.PlayerWeaponSlotCount);
        }
        if(GameManager.Instance.PlayerData.Teamer1 != null)
        {
            TeamSlot2.SetActive(true);
            TeamSlot2.GetComponent<TeamerSlot>().RefreshGem(GameManager.Instance.PlayerData.Teamer1WeaponSlotCount);
        }
        if(GameManager.Instance.PlayerData.Teamer2 != null)
        {
            TeamSlot1.SetActive(true);
            TeamSlot1.GetComponent<TeamerSlot>().RefreshGem(GameManager.Instance.PlayerData.Teamer2WeaponSlotCount);
        }
        if(GameManager.Instance.PlayerData.Teamer3 != null)
        {
            TeamSlot.SetActive(true);
            TeamSlot.GetComponent<TeamerSlot>().RefreshGem(GameManager.Instance.PlayerData.Teamer3WeaponSlotCount);
        }
        for(int i = 0;i < GameManager.Instance.PlayerData.FreeWeaponSlotCount; i++)
        {
            Instantiate(FreeGemSlot,GemSlotBox);
        }
        for(int i = 0;i < GameManager.Instance.PlayerData.EmptyWeaponSlotCount; i++)
        {
            Instantiate(EmptyGemSlot,GemSlotBox);
        }
    }
    private void OnDisable()
    {
        TeamSlot.SetActive(false);
        TeamSlot1.SetActive(false);
        TeamSlot2.SetActive(false);
        TeamSlot3.SetActive(false);
        for(int i = 0;i < GemSlotBox.childCount; i++)
        {
            Destroy(GemSlotBox.GetChild(i).gameObject);
        }
    }
}
