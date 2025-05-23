using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapCharacrter : MonoBehaviour
{
    public float Width;//������
    public float Height;//����߶�
    public RoomType RoomType;//��������
    public float Size;//�����ӽǴ�С
    public PolygonCollider2D polygonCollider;
    public GameObject CurPlayer;
    public GameObject NoFind;
    private int DoorCount;
    [Header("������Ʒ")]
    public GameObject TransmissionTower;
    public GameObject CardPaper;
    public Transform DoorBox;
    public void BuildNewRoom()
    {
        StartCoroutine(MapManager.Instance.BuildNewRoom(Width,Height,(Vector2)transform.position,RoomType));
    }
    public bool CheckCanBuildRoom()//���ٽ���ʱ��
    {
        DoorCount = 0;
        for(int i = 0;i < DoorBox.childCount; i++)
        {
            switch (DoorBox.GetChild(i).GetComponent<RoomDoor>().doorType)
            {
                case DoorType.LeftUpDoor:
                    if(Physics2D.OverlapPoint(DoorBox.GetChild(i).position + new Vector3(-6, 0, 0), SceneChangeManager.Instance.Room))
                    {
                        DoorCount++;
                    }
                    break;
                case DoorType.LeftDownDoor:
                    if (Physics2D.OverlapPoint(DoorBox.GetChild(i).position + new Vector3(-6, 0, 0), SceneChangeManager.Instance.Room))
                    {
                        DoorCount++;
                    }
                    break;
                case DoorType.RightUpDoor:
                    if (Physics2D.OverlapPoint(DoorBox.GetChild(i).position + new Vector3(6, 0, 0), SceneChangeManager.Instance.Room))
                    {
                        DoorCount++;
                    }
                    break;
                case DoorType.RightDownDoor:
                    if (Physics2D.OverlapPoint(DoorBox.GetChild(i).position + new Vector3(6, 0, 0), SceneChangeManager.Instance.Room))
                    {
                        DoorCount++;
                    }
                    break;
            }
        }
        if(DoorCount == 4)
        {
            return false;
        }
        return true;
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
    public void AccessRoom()
    {
        if (RoomType == RoomType.CardRoom)
        {
            Destroy(CardPaper);
        }
    }
}
