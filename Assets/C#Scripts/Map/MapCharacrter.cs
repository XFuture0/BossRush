using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCharacrter : MonoBehaviour
{
    public float Width;//房间宽度
    public float Height;//房间高度
    public RoomType RoomType;//房间类型
    public DoorType DoorType;//房间门类型
    public void BuildNewRoom()
    {
        StartCoroutine(MapManager.Instance.BuildNewRoom(Width,Height,(Vector2)transform.position,RoomType,DoorType));
    }
}
