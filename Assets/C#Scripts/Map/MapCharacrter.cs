using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapCharacrter : MonoBehaviour
{
    public RoomType RoomType;//房间类型
    public float Size;//房间视角大小
    public PolygonCollider2D polygonCollider;
    public GameObject CurPlayer;
    public GameObject NoFind;
    private int DoorCount;
    [Header("房间物品")]
    public GameObject TransmissionTower;
    public GameObject CardPaper;
    public Transform DoorBox;
    public List<GameObject> LeftDoor = new List<GameObject>();//左侧门
    public List<GameObject> RightDoor = new List<GameObject>();//右侧门
    public void BuildNewRoom()
    {
        var doorcount = Random.Range(0,DoorBox.childCount);
        DoorType NewDoorType = DoorBox.transform.GetChild(doorcount).gameObject.GetComponent<RoomDoor>().doorType;
        Vector3 NewDoorPosition = DoorBox.transform.GetChild(doorcount).gameObject.transform.position;
        StartCoroutine(MapManager.Instance.BuildNewRoom((Vector2)transform.position,NewDoorType,NewDoorPosition, DoorBox.transform.GetChild(doorcount).GetChild(0).gameObject));
    }
    public bool CheckCanBuildRoom()//减少建造时间
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
