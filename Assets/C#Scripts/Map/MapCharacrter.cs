using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCharacrter : MonoBehaviour
{
    public float Width;//������
    public float Height;//����߶�
    public RoomType RoomType;//��������
    public float Size;//�����ӽǴ�С
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
