using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapCharacrter : MonoBehaviour
{
    [System.Serializable]
    public class Door
    {
        public GameObject DoorObject;
        public int DoorIndex;
        public bool DoorOpen;
    }
    public int Index;
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
    public List<Door> LeftDoor = new List<Door>();//�����
    public List<Door> RightDoor = new List<Door>();//�Ҳ���
    public void BuildNewRoom()
    {
        var doorcount = Random.Range(0f, 1f);
        int CurRoomDoor = 0;
        if(doorcount <= 0.5f && RightDoor.Count != 0)
        {
            CurRoomDoor = UnityEngine.Random.Range(0,RightDoor.Count);
            StartCoroutine(MapManager.Instance.BuildNewRoom((Vector2)transform.position, DoorType.RightDoor, RightDoor[CurRoomDoor].DoorObject.transform.position,this,CurRoomDoor));
        }
        else if(doorcount > 0.5f && LeftDoor.Count != 0)
        {
            CurRoomDoor = UnityEngine.Random.Range(0, LeftDoor.Count);
            StartCoroutine(MapManager.Instance.BuildNewRoom((Vector2)transform.position, DoorType.LeftDoor, LeftDoor[CurRoomDoor].DoorObject.transform.position,this,CurRoomDoor));
        }
    }
    public bool CheckCanBuildRoom()//���ٽ���ʱ��
    {
        DoorCount = 0;
        for(int i = 0;i < DoorBox.childCount; i++)
        {
            switch (DoorBox.GetChild(i).GetComponent<RoomDoor>().doorType)
            {
                case DoorType.LeftDoor:
                    if (Physics2D.OverlapPoint(DoorBox.GetChild(i).position + new Vector3(-6, 0, 0), SceneChangeManager.Instance.Room))
                    {
                        DoorCount++;
                    }
                    break;
                case DoorType.RightDoor:
                    if (Physics2D.OverlapPoint(DoorBox.GetChild(i).position + new Vector3(6, 0, 0), SceneChangeManager.Instance.Room))
                    {
                        DoorCount++;
                    }
                    break;
            }
        }
        if(DoorCount == DoorBox.childCount)
        {
            return false;
        }
        return true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
      if(other.tag == "Player" && gameObject.layer == 13)
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
