using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeamBag : MonoBehaviour
{
    public Button LeftButton;
    public Button RightButton;
    [Header("∂”ŒÈ–≈œ¢")]
    public GameObject TeamSlot;
    public GameObject TeamSlot1;
    public GameObject TeamSlot2;
    public GameObject TeamSlot3;
    public GameObject WeaponGemBag;
    public GameObject ExtraGemBag;
    private void Awake()
    {
        LeftButton.onClick.AddListener(OnLeftButton);
        RightButton.onClick.AddListener(OnRightButton);
    }
    private void OnEnable()
    {
        CheckSlime();
    }
    private void CheckSlime()
    {
        if(GameManager.Instance.PlayerData.Player != null)
        {
            TeamSlot3.SetActive(true);
            TeamSlot3.GetComponent<TeamerSlot>().RefreshGem(GameManager.Instance.PlayerData.PlayerWeaponSlotCount,GameManager.Instance.PlayerData.PlayerExtraGemData);
        }
        if(GameManager.Instance.PlayerData.Teamer1 != null)
        {
            TeamSlot2.SetActive(true);
            TeamSlot2.GetComponent<TeamerSlot>().RefreshGem(GameManager.Instance.PlayerData.Teamer1WeaponSlotCount,GameManager.Instance.PlayerData.Teamer1ExtraGemData);
        }
        if(GameManager.Instance.PlayerData.Teamer2 != null)
        {
            TeamSlot1.SetActive(true);
            TeamSlot1.GetComponent<TeamerSlot>().RefreshGem(GameManager.Instance.PlayerData.Teamer2WeaponSlotCount, GameManager.Instance.PlayerData.Teamer2ExtraGemData);
        }
        if(GameManager.Instance.PlayerData.Teamer3 != null)
        {
            TeamSlot.SetActive(true);
            TeamSlot.GetComponent<TeamerSlot>().RefreshGem(GameManager.Instance.PlayerData.Teamer3WeaponSlotCount, GameManager.Instance.PlayerData.Teamer3ExtraGemData);
        }
        WeaponGemBag.SetActive(true);
    }
    private void OnDisable()
    {
        TeamSlot.SetActive(false);
        TeamSlot1.SetActive(false);
        TeamSlot2.SetActive(false);
        TeamSlot3.SetActive(false);
        WeaponGemBag.SetActive(false);
        ExtraGemBag.SetActive(false);
    }
    private void OnLeftButton()
    {
        WeaponGemBag.SetActive(true);
        ExtraGemBag.SetActive(false);
    }
    private void OnRightButton()
    {
        WeaponGemBag.SetActive(false);
        ExtraGemBag.SetActive(true);
    }
}
