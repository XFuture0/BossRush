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
    public GameObject CurPlayer;
    public GameObject NoFind;
    public GameObject TransmissionTower;
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
            if(RoomType == RoomType.TransmissionTowerRoom)
            {
                TransmissionTower.SetActive(true);
            }
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
        if (RoomType == RoomType.TransmissionTowerRoom)
        {
            TransmissionTower.SetActive(true);
        }
    }
}
