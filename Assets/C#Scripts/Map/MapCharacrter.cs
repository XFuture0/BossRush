using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCharacrter : MonoBehaviour
{
    public float Width;//������
    public float Height;//����߶�
    public RoomType RoomType;//��������
    public DoorType DoorType;//����������
    public void BuildNewRoom()
    {
        StartCoroutine(MapManager.Instance.BuildNewRoom(Width,Height,(Vector2)transform.position,RoomType,DoorType));
    }
}
