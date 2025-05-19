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
    public GameObject CurPlayer;
    public GameObject NoFind;
    public void BuildNewRoom()
    {
        StartCoroutine(MapManager.Instance.BuildNewRoom(Width,Height,(Vector2)transform.position,RoomType));
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
      if(other.tag == "Player")
      {
            GameManager.Instance.OnBoundEvent(polygonCollider,Size);
            CurPlayer.SetActive(true);
            NoFind.SetActive(false);
      }   
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            CurPlayer.SetActive(false);
        }
    }
    public void FindRoom()
    {
        NoFind.SetActive(false);
    }
}
