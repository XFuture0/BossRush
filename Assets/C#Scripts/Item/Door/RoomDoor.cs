using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomDoor : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D boxCollider;
    public DoorType doorType;
    private RoomType roomType;
    public LayerMask Room;
    public GameObject DoorSprite;
    [Header("ÊÂ¼þ¼àÌý")]
    public VoidEventSO OpenDoorEvent;
    public VoidEventSO CloseDoorEvent;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        roomType = transform.parent.parent.gameObject.GetComponent<MapCharacrter>().RoomType;
    }
    private void Update()
    {
        if (DoorSprite.activeSelf)
        {
            DoorSprite.GetComponent<Tilemap>().color = ColorManager.Instance.UpdateColor(2);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag  == "Player")
        {
            SceneChangeManager.Instance.ChangeRoom(doorType,transform.position);
        }
    }
    private void OnEnable()
    {
        OpenDoorEvent.OnEventRaised += OnOpenDoor;
        CloseDoorEvent.OnEventRaised += OnCloseDoor;
    }
    private void OnCloseDoor()
    {
        anim.SetBool("Open", false);
    }
    private void OnOpenDoor()
    {
        anim.SetBool("Open", true);
    }
    private void OnDisable()
    {
        OpenDoorEvent.OnEventRaised -= OnOpenDoor;
        CloseDoorEvent.OnEventRaised -= OnCloseDoor;
    }
    private void OpenDoor()
    {
        boxCollider.enabled = false;
    }
    private void CloseDoor()
    {
        boxCollider.enabled = true;
    }
}
