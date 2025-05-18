using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : MonoBehaviour
{
    private Animator anim;
    private BoxCollider2D boxCollider;
    public DoorType doorType;
    private RoomType roomType;
    public LayerMask Room;
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
        CheckRoom();
    }
    private void CheckRoom()
    {
        switch (doorType) 
        {
            case DoorType.LeftUpDoor:
                if (Physics2D.OverlapCircle((Vector2)transform.position - new Vector2(5, 0), 0.1f, Room))
                {
                    var ray1 = Physics2D.OverlapCircle((Vector2)transform.position - new Vector2(5,0),0.1f,Room);
                    if(ray1.gameObject.transform.position.y != transform.parent.parent.position.y)
                    {
                        transform.GetChild(0).gameObject.SetActive(false);
                    }
                    else if(ray1.gameObject.transform.position.y == transform.parent.parent.position.y)
                    {
                        transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
                else if (!Physics2D.OverlapCircle((Vector2)transform.position - new Vector2(5, 0), 0.1f, Room))
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                }
                break;
            case DoorType.LeftDownDoor:
                if (Physics2D.OverlapCircle((Vector2)transform.position - new Vector2(5, 0), 0.1f, Room))
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                }
                else if (!Physics2D.OverlapCircle((Vector2)transform.position - new Vector2(5, 0), 0.1f, Room))
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                }
                break;
            case DoorType.RightUpDoor:
               if (Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(5, 0), 0.1f, Room))
               {
                    var ray1 = Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(5, 0), 0.1f, Room);
                    if (ray1.gameObject.transform.position.y != transform.parent.parent.position.y)
                    {
                        transform.GetChild(0).gameObject.SetActive(false);
                    }
                    else if (ray1.gameObject.transform.position.y == transform.parent.parent.position.y)
                    {
                        transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
               else if (!Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(5, 0), 0.1f, Room))
               {
                    transform.GetChild(0).gameObject.SetActive(true);
               }
               break;
            case DoorType.RightDownDoor:
                if (Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(5, 0), 0.1f, Room))
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                }
                else if (!Physics2D.OverlapCircle((Vector2)transform.position + new Vector2(5, 0), 0.1f, Room))
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                }
                break;
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
