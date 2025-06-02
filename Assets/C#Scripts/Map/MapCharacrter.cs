using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapCharacrter : MonoBehaviour
{
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
    public List<GameObject> LeftDoor = new List<GameObject>();//�����
    public List<GameObject> RightDoor = new List<GameObject>();//�Ҳ���
    public void BuildNewRoom()
    {
        var doorcount = Random.Range(0,DoorBox.childCount);
        DoorType NewDoorType = DoorBox.transform.GetChild(doorcount).gameObject.GetComponent<RoomDoor>().doorType;
        Vector3 NewDoorPosition = DoorBox.transform.GetChild(doorcount).gameObject.transform.position;
        StartCoroutine(MapManager.Instance.BuildNewRoom((Vector2)transform.position,NewDoorType,NewDoorPosition, DoorBox.transform.GetChild(doorcount).GetChild(0).gameObject));
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
