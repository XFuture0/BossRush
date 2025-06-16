using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapCharacrter : MonoBehaviour
{
    [System.Serializable]
    public class Monster
    {
        public GameObject MonsterObject;
        public Vector3 MonsterPosition;
    }
    [System.Serializable]
    public class Door
    {
        public GameObject DoorObject;
        public int DoorIndex;
        public bool DoorOpen;
    }
    public int Index;
    public RoomType RoomType;//房间类型
    public float Size;//房间视角大小
    public PolygonCollider2D polygonCollider;
    public GameObject CurPlayer;
    public GameObject NoFind;
    public GameObject MonsterBox;
    private int DoorCount;
    private bool IsAccess;
    public MiniRoomCanvs MiniRoomCanvs;
    [Header("房间物品")]
    public GameObject RoomMini;
    public GameObject CardPaper;
    public Transform DoorBox;
    public List<Door> LeftDoor = new List<Door>();//左侧门
    public List<Door> RightDoor = new List<Door>();//右侧门
    public List<Monster> Monsters = new List<Monster>();
    public Vector3 BossPosition;
    private void Update()
    {
        if (RoomType == RoomType.NormalRoom)
        {
            CheckFinishRoom();
        }
    }
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
            if(RoomMini != null)
            {
                MiniRoomCanvs.SetMiniRoom(RoomMini);
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
        if (RoomMini != null)
        {
            MiniRoomCanvs.SetMiniRoom(RoomMini);
        }
    }
    public void AccessRoom()
    {
        IsAccess = true;
        if (RoomType == RoomType.CardRoom)
        {
            Invoke("DesCardPaper", 0.1f);
        }
    }
    private void DesCardPaper()
    {
        Destroy(CardPaper);
    }
    public void SetMonster()
    {
        MonsterBox.SetActive(true);
    }
    public void CheckFinishRoom()
    {
        if(MonsterBox.transform.childCount == 0 && RoomType == RoomType.NormalRoom && !IsAccess)
        {
            SceneChangeManager.Instance.OpenDoorEvent.RaiseEvent();
            MapManager.Instance.AccessRoom(transform.position);
            GameManager.Instance.BossDeadEvent.RaiseEvent();
        }
    }
}
