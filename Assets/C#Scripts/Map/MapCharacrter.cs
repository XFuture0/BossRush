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
    public GameObject TreasureBox;
    public PolygonCollider2D polygonCollider;
    public GameObject CurPlayer;
    public GameObject NoFind;
    public GameObject MonsterBox;
    public LayerMask DropItem;
    private int DoorCount;
    private bool IsAccess;
    public MiniRoomCanvs MiniRoomCanvs;
    [Header("房间物品")]
    public GameObject CardPaper;
    public Transform DoorBox;
    public List<Door> LeftDoor = new List<Door>();//左侧门
    public List<Door> RightDoor = new List<Door>();//右侧门
    public List<Monster> Monsters = new List<Monster>();
    public Vector3 SetPosition;
    [Header("小地图信息")]
    private bool HaveCoin;
    private bool HaveHeart;
    private bool HaveSheild;
    private bool HaveGem;
    private bool HaveTreasureBox;
    private bool HaveExtraGem;
    public GameObject TransmissionMini;
    public GameObject ShopMini;
    public GameObject BossMini;
    public GameObject StartRoomMini;
    public GameObject CoinMini;
    public GameObject HeartMini;
    public GameObject SheildMini;
    public GameObject GemMini;
    public GameObject TreasureBoxMini;
    public GameObject ExtraGemMini;
    [Header("事件监听")]
    public VoidEventSO GetItemEvent;
    private void OnEnable()
    {
        GetItemEvent.OnEventRaised += CheckMapMini;
    }
    private void OnDisable()
    {
        GetItemEvent.OnEventRaised -= CheckMapMini;
    }
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
            RefreshMapMini();
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
        RefreshMapMini();
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
            Instantiate(TreasureBox,transform.position + SetPosition,Quaternion.identity);
            MapManager.Instance.AccessRoom(transform.position);
            GameManager.Instance.BossDeadEvent.RaiseEvent();
        }
    }
    public void RefreshMapMini()
    {
        switch (RoomType)
        {
            case RoomType.StartRoom:
                MiniRoomCanvs.SetMiniRoom(StartRoomMini);
                break;
            case RoomType.NormalRoom:
                break;
            case RoomType.CardRoom:
                break;
            case RoomType.TransmissionTowerRoom:
                MiniRoomCanvs.SetMiniRoom(TransmissionMini);
                break;
            case RoomType.ShopRoom:
                MiniRoomCanvs.SetMiniRoom(ShopMini);
                break;
            case RoomType.BossRoom:
                MiniRoomCanvs.SetMiniRoom(BossMini);
                break;
        }
    }
    private void CheckMapMini()
    {
        HaveCoin = false;
        HaveHeart = false;
        HaveSheild = false;
        HaveGem = false;
        HaveTreasureBox = false;
        var RoomPoLeftUp = gameObject.transform.position + new Vector3(-30, 16, 0);
        var RoomPoRightDown = gameObject.transform.position;
        var Result = Physics2D.OverlapAreaAll(RoomPoLeftUp, RoomPoRightDown,DropItem);
        if(Result != null)
        {
            foreach (var item in Result)
            {
                switch (item.GetComponent<DroppedItems>().Thisitem.ItemType) 
                {
                    case ItemType.Coin:
                        HaveCoin = true;
                        MiniRoomCanvs.SetMiniRoom(CoinMini);
                        break;
                    case ItemType.HeartItem:
                        HaveHeart = true;
                        MiniRoomCanvs.SetMiniRoom(HeartMini);
                        break;
                    case ItemType.ShieldItem:
                        HaveSheild = true;
                        MiniRoomCanvs.SetMiniRoom(SheildMini);
                        break;
                    case ItemType.GemItem:
                        HaveGem = true;
                        MiniRoomCanvs.SetMiniRoom(GemMini);
                        break;
                    case ItemType.TreasureBox:
                        HaveTreasureBox = true;
                        MiniRoomCanvs.SetMiniRoom(TreasureBoxMini);
                        break;
                    case ItemType.ShootGem:
                        HaveExtraGem = true;
                        MiniRoomCanvs.SetMiniRoom(ExtraGemMini);
                        break;
                    case ItemType.DamageGem:
                        HaveExtraGem = true;
                        MiniRoomCanvs.SetMiniRoom(ExtraGemMini);
                        break;
                    case ItemType.SpeedGem:
                        HaveExtraGem = true;
                        MiniRoomCanvs.SetMiniRoom(ExtraGemMini);
                        break;
                    case ItemType.BiggerGem:
                        HaveExtraGem = true;
                        MiniRoomCanvs.SetMiniRoom(ExtraGemMini);
                        break;
                }
            }
            if (!HaveCoin)
            {
                MiniRoomCanvs.RemoveMiniRoom(CoinMini);
            }
            if (!HaveHeart)
            {
                MiniRoomCanvs.RemoveMiniRoom(HeartMini);
            }
            if (!HaveSheild)
            {
                MiniRoomCanvs.RemoveMiniRoom(SheildMini);
            }
            if (!HaveGem)
            {
                MiniRoomCanvs.RemoveMiniRoom(GemMini);
            }
            if (!HaveTreasureBox)
            {
                MiniRoomCanvs.RemoveMiniRoom(TreasureBoxMini);
            }
            if (!HaveExtraGem)
            {
                MiniRoomCanvs.RemoveMiniRoom(ExtraGemMini);
            }
        }
    }
}
