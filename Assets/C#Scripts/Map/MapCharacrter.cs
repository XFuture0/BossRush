using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCharacrter : MonoBehaviour
{
    public float Width;//房间宽度
    public float Height;//房间高度
    public RoomType RoomType;//房间类型
    public float Size;//房间视角大小
    public PolygonCollider2D polygonCollider;
    public void BuildNewRoom()
    {
        StartCoroutine(MapManager.Instance.BuildNewRoom(Width,Height,(Vector2)transform.position));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
      if(other.tag == "Player")
      {
            GameManager.Instance.OnBoundEvent(polygonCollider,Size);
            SceneChangeManager.Instance.OpenDoorEvent.RaiseEvent();
      }   
    }
}
