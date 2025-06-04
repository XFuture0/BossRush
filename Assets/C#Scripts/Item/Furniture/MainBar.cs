using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBar : MonoBehaviour
{
    private bool IsPlayer;
    public VoidEventSO ReloadPlayerRoomEvent;
    public GameObject RKey;
    private void OnEnable()
    {
        ReloadPlayerRoomEvent.OnEventRaised += OnReloadPlayerRoom;
    }
    private void Update()
    {
        if(IsPlayer && KeyBoardManager.Instance.GetKeyDown_R())
        {
            RecipeManager.Instance.OpenRecipeCanvs();
        }
    }
    private void OnReloadPlayerRoom()
    {
        for(int i = 0; i< 3; i++)
        {
            transform.GetChild(i).GetComponent<Beer>().ReloadBeer(i + 1);
        }
    }
    private void OnDisable()
    {
        ReloadPlayerRoomEvent.OnEventRaised -= OnReloadPlayerRoom;
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
