using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class ChooseCard : MonoBehaviour
{
    private Button button;
    public GameObject Door;
    public GameManager.Card card;
    [Header("ÊÂ¼þ¼àÌý")]
    public VoidEventSO CloseDoorEvent;
    private void Awake()
    {
        button = transform.GetChild(0).GetComponent<Button>();
        button.onClick.AddListener(ChooseThisCard);
    }
    private void OnEnable()
    {
        CloseDoorEvent.OnEventRaised += CloseDoor;
    }
    private void ChooseThisCard()
    {
        GameManager.Instance.UseCard(card);
        KeyBoardManager.Instance.StopMoveKey = false;
        OpenDoor();
        transform.parent.gameObject.SetActive(false);
    }
    private void OpenDoor()
    {
        Door.GetComponent<SpriteRenderer>().enabled = false;
        Door.GetComponent<PolygonCollider2D>().enabled = true;
    }
    private void CloseDoor()
    {
        Door.GetComponent<SpriteRenderer>().enabled = true;
        Door.GetComponent<PolygonCollider2D>().enabled = false;
    }
    private void OnDisable()
    {
        CloseDoorEvent.OnEventRaised -= CloseDoor;
    }
}
