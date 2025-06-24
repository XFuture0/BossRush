using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MapManager : SingleTons<MapManager>
{
    [System.Serializable]
    public class ChooseRoom
    {
        public GameObject Room;
        public int Index;
    }
    public Transform MapBox;
    public Transform CoinBox;
    public List<RoomList> MapLists = new List<RoomList>();
    public List<ChooseRoom> NewMapRoom = new List<ChooseRoom>(); 
    public LayerMask Room;
    public MapData MapData;
    public ItemList itemList;
    public GameObject TransmissionCamera;
    public GameObject CanvsBox;
    public Transform ItemBox;
    [Header("关卡难度")]
    public RoomList NormalRoomList1_3;
    public RoomList NormalRoomList4_6;
    public RoomList NormalRoomList7_9;
    [Header("地图建造属性")]
    public int RandomRoomCount;//大致房间数量
    private int RoomCount;//实际房间数量
    private int CurrentRoomCount;//当前房间数量(建造中)
    [Header("建造计时器")]
    public float BuildTime;
    private bool IsBuild;
    [Header("物品列表")]
    public GameObject Coin;
    public GameObject CardPaper;
    public GameObject Fruit;
    public GameObject HeartItem;
    public GameObject ShieldItem;
    public GameObject GemItem;
    public GameObject TreasureBox;
    public GameObject ShootGem;
    public GameObject DamageGem;
    public GameObject SpeedGem;
    public GameObject BiggerGem;
    [Header("广播")]
    public VoidEventSO SaveItemEvent;
    public VoidEventSO ClearItemEvent;
    public VoidEventSO ClearMonsterEvent;
    private void Update()
    {
        if (IsBuild)
        {
            BuildTime += Time.deltaTime;
        }
    }
    public IEnumerator SetNewMap() 
    {
        ClearMap();
        yield return new WaitForSeconds(0.5f);
        BuildTime = 0;
        IsBuild = true;
        RoomCount = UnityEngine.Random.Range((int)(RandomRoomCount * 0.8f), (int)(RandomRoomCount * 1.2f));//确定当前房间数量
        CurrentRoomCount = 0;
        var NewRoom = Instantiate(MapLists[0].RoomLists[0], MapBox);
        NewRoom.GetComponent<MapCharacrter>().Index = 0;
        MapData.Room NewRoomPoint = new MapData.Room();
        NewRoomPoint.RoomPosition = NewRoom.transform.position;
        NewRoomPoint.RoomType = RoomType.StartRoom;
        NewRoomPoint.RoomIndex = 0;
        NewRoomPoint.Index = 0;
        NewRoomPoint.LeftDoor = NewRoom.GetComponent<MapCharacrter>().LeftDoor;
        NewRoomPoint.RightDoor = NewRoom.GetComponent<MapCharacrter>().RightDoor;//保存门的开启状态
        MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
        ChooseNewRoom(RoomCount - 1);
        ReFreshShop();
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(SetNewRoom());
    }
    private IEnumerator SetNewRoom()
    {
        Debug.Log(BuildTime);
        for(int i = 0; i < MapBox.childCount; i++)
        {
            if(CurrentRoomCount < RoomCount)
            {
                var NewRoomType = UnityEngine.Random.Range(0, 100) % MapLists.Count;
                if (MapBox.GetChild(i).gameObject.GetComponent<MapCharacrter>().CheckCanBuildRoom())
                {
                    MapBox.GetChild(i).gameObject.GetComponent<MapCharacrter>().BuildNewRoom();
                    yield return new WaitForSeconds(0.08f);
                }
            }
        }
        if(BuildTime >= 3)
        {
            Debug.Log("ReBuild");
            BuildTime = 0;
            StopAllCoroutines();
            StartCoroutine(SetNewMap());//重新建造
            yield break;
        }
        if(CurrentRoomCount < RoomCount)
        {
           StartCoroutine(SetNewRoom());//迭代
        }
        if(CurrentRoomCount >= RoomCount)
        {
            IsBuild = false;
        }
    }
    public IEnumerator BuildNewRoom(Vector2 RoomCenter,DoorType doorType,Vector3 DoorPosition, MapCharacrter CurrentRoom,int CurrentRoomDoorCount)
    {
        switch (doorType) 
        {
            case DoorType.LeftDoor:
                var NewRoomCount_LeftDoor = UnityEngine.Random.Range(0,NewMapRoom.Count - 1);
                if(CurrentRoomCount == RoomCount - 1)
                {
                    NewRoomCount_LeftDoor = 0;
                }
                var NewRoom = Instantiate(NewMapRoom[NewRoomCount_LeftDoor].Room, MapBox);//生成新房间
                NewRoom.layer = 0;
                var NewRightDoorCount = UnityEngine.Random.Range(0,NewRoom.GetComponent<MapCharacrter>().RightDoor.Count);//新的门
                if(NewRoom.GetComponent <MapCharacrter>().RightDoor.Count == 0)
                {
                    Destroy(NewRoom);
                    yield break;
                }
                var RightCenterOffect = NewRoom.transform.position - NewRoom.GetComponent<MapCharacrter>().RightDoor[NewRightDoorCount].DoorObject.transform.position;//与右门的偏移
                NewRoom.transform.position = DoorPosition + RightCenterOffect + new Vector3(-4,0,0);//确定新房间的位置
                yield return new WaitForSeconds(0.02f);
                if (CheckOverride(NewRoom.GetComponent<MapCharacrter>().polygonCollider, NewRoom.transform.position))//是否出现覆盖
                {
                    CurrentRoomCount++;//当前房间数量加1
                    NewRoom.layer = 13;
                    CurrentRoom.LeftDoor[CurrentRoomDoorCount].DoorObject.transform.GetChild(0).gameObject.SetActive(false);
                    CurrentRoom.LeftDoor[CurrentRoomDoorCount].DoorOpen = true;
                    MapData.RoomLists[CurrentRoom.Index].LeftDoor = CurrentRoom.LeftDoor;//保存门的开启状态
                    NewRoom.GetComponent<MapCharacrter>().RightDoor[NewRightDoorCount].DoorObject.transform.GetChild(0).gameObject.SetActive(false);
                    NewRoom.GetComponent<MapCharacrter>().RightDoor[NewRightDoorCount].DoorOpen = true;
                    NewRoom.GetComponent<MapCharacrter>().Index = CurrentRoomCount;
                    if (CurrentRoomCount == RoomCount)//是否是boss房
                    {
                        MapData.Room NewRoomPoint = new MapData.Room();
                        NewRoomPoint.RoomPosition = NewRoom.transform.position;
                        NewRoomPoint.RoomType = NewRoom.GetComponent<MapCharacrter>().RoomType;
                        NewRoomPoint.RoomIndex = CurrentRoomCount;
                        NewRoomPoint.Index = NewMapRoom[NewRoomCount_LeftDoor].Index;
                        NewRoomPoint.LeftDoor = NewRoom.GetComponent<MapCharacrter>().LeftDoor;
                        NewRoomPoint.RightDoor = NewRoom.GetComponent<MapCharacrter>().RightDoor;//保存门的开启状态
                        MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
                        NewMapRoom.Remove(NewMapRoom[NewRoomCount_LeftDoor]);
                    }
                    if (CurrentRoomCount != RoomCount)
                    { 
                        MapData.Room NewRoomPoint = new MapData.Room();
                        NewRoomPoint.RoomPosition = NewRoom.transform.position;
                        NewRoomPoint.RoomType = NewRoom.GetComponent<MapCharacrter>().RoomType;
                        NewRoomPoint.RoomIndex = CurrentRoomCount;
                        NewRoomPoint.Index = NewMapRoom[NewRoomCount_LeftDoor].Index;
                        NewRoomPoint.LeftDoor = NewRoom.GetComponent<MapCharacrter>().LeftDoor;
                        NewRoomPoint.RightDoor = NewRoom.GetComponent<MapCharacrter>().RightDoor;//保存门的开启状态
                        MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
                        NewMapRoom.Remove(NewMapRoom[NewRoomCount_LeftDoor]);
                    }
                }
                else if (!CheckOverride(NewRoom.GetComponent<MapCharacrter>().polygonCollider, NewRoom.transform.position))
                {
                    Destroy(NewRoom);
                }
                else if(NewRoom.GetComponent<MapCharacrter>().Index == 0)
                {
                    Destroy(NewRoom);
                }
                break;
            case DoorType.RightDoor:
                var NewRoomCount_RightDoor = UnityEngine.Random.Range(0, NewMapRoom.Count - 1);
                if (CurrentRoomCount == RoomCount - 1)
                {
                    NewRoomCount_RightDoor = 0;
                }
                var NewRightRoom = Instantiate(NewMapRoom[NewRoomCount_RightDoor].Room, MapBox);//生成新房间
                NewRightRoom.layer = 0;
                var NewLeftDoorCount = UnityEngine.Random.Range(0, NewRightRoom.GetComponent<MapCharacrter>().LeftDoor.Count);//新的门
                if (NewRightRoom.GetComponent<MapCharacrter>().LeftDoor.Count == 0)
                {
                    Destroy(NewRightRoom);
                    yield break;
                }
                var LeftCenterOffect = NewRightRoom.transform.position - NewRightRoom.GetComponent<MapCharacrter>().LeftDoor[NewLeftDoorCount].DoorObject.transform.position;//与左门的偏移
                NewRightRoom.transform.position = DoorPosition + LeftCenterOffect + new Vector3(4, 0, 0);//确定新房间的位置
                yield return new WaitForSeconds(0.02f);
                if (CheckOverride(NewRightRoom.GetComponent<MapCharacrter>().polygonCollider, NewRightRoom.transform.position))//是否出现覆盖
                {
                    CurrentRoomCount++;//当前房间数量加1
                    NewRightRoom.layer = 13;
                    CurrentRoom.RightDoor[CurrentRoomDoorCount].DoorObject.transform.GetChild(0).gameObject.SetActive(false);
                    CurrentRoom.RightDoor[CurrentRoomDoorCount].DoorOpen = true;
                    MapData.RoomLists[CurrentRoom.Index].RightDoor = CurrentRoom.RightDoor;//保存门的开启状态
                    NewRightRoom.GetComponent<MapCharacrter>().LeftDoor[NewLeftDoorCount].DoorObject.transform.GetChild(0).gameObject.SetActive(false);
                    NewRightRoom.GetComponent<MapCharacrter>().LeftDoor[NewLeftDoorCount].DoorOpen = true;
                    NewRightRoom.GetComponent<MapCharacrter>().Index = CurrentRoomCount;
                    if (CurrentRoomCount == RoomCount)//是否是boss房
                    {
                        MapData.Room NewRoomPoint = new MapData.Room();
                        NewRoomPoint.RoomPosition = NewRightRoom.transform.position;
                        NewRoomPoint.RoomType = NewRightRoom.GetComponent<MapCharacrter>().RoomType;
                        NewRoomPoint.RoomIndex = CurrentRoomCount;
                        NewRoomPoint.Index = NewMapRoom[NewRoomCount_RightDoor].Index;
                        NewRoomPoint.LeftDoor = NewRightRoom.GetComponent<MapCharacrter>().LeftDoor;
                        NewRoomPoint.RightDoor = NewRightRoom.GetComponent<MapCharacrter>().RightDoor;//保存门的开启状态
                        MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
                        NewMapRoom.Remove(NewMapRoom[NewRoomCount_RightDoor]);
                    }
                    if (CurrentRoomCount != RoomCount)
                    {
                        MapData.Room NewRoomPoint = new MapData.Room();
                        NewRoomPoint.RoomPosition = NewRightRoom.transform.position;
                        NewRoomPoint.RoomType = NewRightRoom.GetComponent<MapCharacrter>().RoomType;
                        NewRoomPoint.RoomIndex = CurrentRoomCount;
                        NewRoomPoint.Index = NewMapRoom[NewRoomCount_RightDoor].Index;
                        NewRoomPoint.LeftDoor = NewRightRoom.GetComponent<MapCharacrter>().LeftDoor;
                        NewRoomPoint.RightDoor = NewRightRoom.GetComponent<MapCharacrter>().RightDoor;//保存门的开启状态
                        MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
                        NewMapRoom.Remove(NewMapRoom[NewRoomCount_RightDoor]);
                    }
                }
                else if (!CheckOverride(NewRightRoom.GetComponent<MapCharacrter>().polygonCollider, NewRightRoom.transform.position))
                {
                    Destroy(NewRightRoom);
                }
                else if (NewRightRoom.GetComponent<MapCharacrter>().Index == 0)
                {
                    Destroy(NewRightRoom);
                }
                break;
            default:
                break;
        }
        yield return null;
    }
    private bool CheckOverride(PolygonCollider2D polygonCollider2D,Vector2 RoomCenter)
    {
        for (int i = 0; i < polygonCollider2D.GetTotalPointCount(); i++)
        {
            // 发射射线
            if (Physics2D.OverlapPoint(polygonCollider2D.GetPath(0)[i] + RoomCenter, SceneChangeManager.Instance.Room))
            {
                return false;
            }
        }
        return true;
    }
    public void SetRoomData()
    {
        for (int i = MapBox.childCount - 1; i >= 0; i--)
        {
            Destroy(MapBox.GetChild(i).gameObject);
        }
        foreach (var Room in MapData.RoomLists)
        {
            Instantiate(MapLists[(int)Room.RoomType].RoomLists[Room.Index],Room.RoomPosition, Quaternion.identity, MapBox);
        }//重建地图
        CheckFindRoom();
        CheckDoorOpen();
    }
    public void ClearMap()
    {
        for (int i = MapBox.childCount - 1; i >= 0; i--)
        {
            Destroy(MapBox.GetChild(i).gameObject);
        }
        MapData.RoomLists.Clear();
        NewMapRoom.Clear();
        //清除原有地图
        for (int i = CoinBox.childCount - 1; i >= 0; i--)
        {
            Destroy(CoinBox.GetChild(i).gameObject);
        }
        itemList.ItemLists.Clear();
        //清除掉落物
    }
    public void AccessRoom(Vector3 RoomPosition)//房间已通过
    {
        foreach (var room in MapData.RoomLists)
        {
            if(room.RoomPosition == RoomPosition)
            {
                room.IsAccess = true;
                CheckFindRoom();
            }
        }
    }
    public void FindRoom(Vector3 RoomPosition)//房间已发现
    {
        foreach (var room in MapData.RoomLists)
        {
            if (room.RoomPosition == RoomPosition)
            {
                room.IsFind = true;
            }
        }
    }
    public bool GetRoom(Vector3 RoomPosition)
    {
        foreach (var room in MapData.RoomLists)
        {
            if (room.RoomPosition == RoomPosition)
            {
                if (room.IsFind)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public bool CheckAccessRoom(Vector3 RoomPosition)//检查房间是否已通过
    {
        foreach (var room in MapData.RoomLists)
        {
            if (room.RoomPosition == RoomPosition)
            {
                if (room.IsAccess)
                {
                    return true;
                }
            }
        }
        return false;
    }
    private void CheckFindRoom()//检查房间是否已找到
    {
        foreach (var room in MapData.RoomLists)
        {
            if (room.IsFind)
            {
                Physics2D.OverlapPoint(room.RoomPosition,Room).gameObject.GetComponent<MapCharacrter>().FindRoom();
            }
            if (room.IsAccess)
            {
                Physics2D.OverlapPoint(room.RoomPosition, Room).gameObject.GetComponent<MapCharacrter>().AccessRoom();
            }
        }
    }
    private void CheckDoorOpen()
    {
        foreach (var room in MapData.RoomLists)
        {
            foreach (var door in room.LeftDoor)
            {
                if (door.DoorOpen)
                {
                    MapBox.GetChild(room.RoomIndex).GetComponent<MapCharacrter>().LeftDoor[door.DoorIndex].DoorObject.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            foreach (var door in room.RightDoor)
            {
                if (door.DoorOpen)
                {
                    MapBox.GetChild(room.RoomIndex).GetComponent<MapCharacrter>().RightDoor[door.DoorIndex].DoorObject.transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
    }
    public void TransmissionRoom(Vector3 TargetPosition)
    {
        StartCoroutine(OnTransmissionRoom(TargetPosition));
    }
    private IEnumerator OnTransmissionRoom(Vector3 TargetPosition)
    {
        KeyBoardManager.Instance.StopMoveKey = true;
        CanvsBox.SetActive(true);
        TransmissionCamera.SetActive(false);
        SceneChangeManager.Instance.Fadecanvs.FadeIn();
        SceneChangeManager.Instance.Player.transform.position = TargetPosition;
        yield return new WaitForSeconds(0.1f);
        SceneChangeManager.Instance.ChangeMiniMapPositionEvent.RaiseVector3Event(Physics2D.OverlapPoint(TargetPosition,SceneChangeManager.Instance.Room).gameObject.transform.position);
        DataManager.Instance.Save(DataManager.Instance.Index);//存档
        SceneChangeManager.Instance.Fadecanvs.FadeOut();
        KeyBoardManager.Instance.StopMoveKey = false;
    }
    public void OpenTransmission(Vector3 RoomPosition)
    {
        KeyBoardManager.Instance.StopMoveKey = true;
        CanvsBox.SetActive(false);
        TransmissionCamera.transform.position = new Vector3(RoomPosition.x,RoomPosition.y,TransmissionCamera.transform.position.z);
        TransmissionCamera.SetActive(true);
    }
    public void CloseTransmission()
    {
        KeyBoardManager.Instance.StopMoveKey = false;
        CanvsBox.SetActive(true);
        TransmissionCamera.SetActive(false);
    }
    public void SaveItemList()
    {
        SaveItemEvent.RaiseEvent();
    }
    public void ClearItemList()
    {
        ClearItemEvent.RaiseEvent();
        itemList.ItemLists.Clear();
    }
    public void AddItemList(ItemList.Item Thisitem)
    {
        foreach (var item in itemList.ItemLists)
        {
            if (Thisitem.Index == item.Index)
            {
                item.ItemPosition = Thisitem.ItemPosition;
                return;
            }
        }
        ItemList.Item NewItem = new ItemList.Item();
        NewItem.ItemType = Thisitem.ItemType;
        NewItem.ItemPosition = Thisitem.ItemPosition;
        itemList.ItemLists.Add(NewItem);
        Thisitem.Index = itemList.ItemLists.Count;
        NewItem.Index = itemList.ItemLists.Count;
    }
    public void DeleteItemList(ItemList.Item Thisitem)
    {
        for(int i = 0;i < itemList.ItemLists.Count; i++)
        {
            if (Thisitem.Index == itemList.ItemLists[i].Index)
            {
                itemList.ItemLists.Remove(itemList.ItemLists[i]);
            }
        }
        for (int i = 0; i < itemList.ItemLists.Count; i++)
        {
            itemList.ItemLists[i].Index = i + 1;
        }
    }
    public void SetItemList()
    {
        for (int i = 0; i < itemList.ItemLists.Count; i++)
        {
            var item = itemList.ItemLists[i];
            CheckItem(item);
        }
    }
    private void CheckItem(ItemList.Item item)
    {
        switch(item.ItemType)
        {
            case ItemType.Coin:
                var NewCoin = Instantiate(Coin,item.ItemPosition,Quaternion.identity,ItemBox);
                NewCoin.GetComponent<DroppedItems>().Thisitem.Index = item.Index;
                break;
            case ItemType.CardPaper:
                var NewCardPaper = Instantiate(CardPaper, item.ItemPosition,Quaternion.identity,ItemBox);
                NewCardPaper.GetComponent<DroppedItems>().Thisitem.Index = item.Index;
                break;
            case ItemType.Fruit:
                var NewFruit = Instantiate(Fruit, item.ItemPosition,Quaternion.identity,ItemBox);
                NewFruit.GetComponent<DroppedItems>().Thisitem.Index = item.Index;
                break;
            case ItemType.HeartItem: 
                var NewHeartItem = Instantiate(HeartItem, item.ItemPosition,Quaternion.identity,ItemBox);
                NewHeartItem.GetComponent<DroppedItems>().Thisitem.Index = item.Index;
                break;
            case ItemType.ShieldItem:
                var NewShieldItem = Instantiate(ShieldItem, item.ItemPosition,Quaternion.identity,ItemBox);
                NewShieldItem.GetComponent<DroppedItems>().Thisitem.Index = item.Index;
                break;
            case ItemType.GemItem:
                var NewGemItem = Instantiate(GemItem, item.ItemPosition,Quaternion.identity,ItemBox);
                NewGemItem.GetComponent<DroppedItems>().Thisitem.Index = item.Index;
                break;
            case ItemType.TreasureBox:
                var NewTreasureBox = Instantiate(TreasureBox, item.ItemPosition,Quaternion.identity,ItemBox);
                NewTreasureBox.GetComponent<DroppedItems>().Thisitem.Index = item.Index;
                break;
            case ItemType.ShootGem:
                var NewShootGem = Instantiate(ShootGem, item.ItemPosition,Quaternion.identity,ItemBox);
                NewShootGem.GetComponent<DroppedItems>().Thisitem.Index = item.Index;
                break;
            case ItemType.DamageGem: 
                var NewDamageGem = Instantiate(DamageGem, item.ItemPosition,Quaternion.identity,ItemBox);
                NewDamageGem.GetComponent<DroppedItems>().Thisitem.Index = item.Index;
                break;
            case ItemType.SpeedGem: 
                var NewSpeedGem = Instantiate(SpeedGem, item.ItemPosition,Quaternion.identity,ItemBox);
                NewSpeedGem.GetComponent<DroppedItems>().Thisitem.Index = item.Index;
                break;
            case ItemType.BiggerGem:
                var NewBiggerGem = Instantiate(BiggerGem, item.ItemPosition,Quaternion.identity,ItemBox);
                NewBiggerGem.GetComponent<DroppedItems>().Thisitem.Index = item.Index;
                break;
        }
    }
    private void ChooseNewRoom(int Count)
    {
        if(GameManager.Instance.PlayerData.CurrentRoomCount < 3)
        {
            MapLists[Settings.NormalRoom] = Instantiate(NormalRoomList1_3);
        }
        else if(GameManager.Instance.PlayerData.CurrentRoomCount < 6)
        {
            MapLists[Settings.NormalRoom] = Instantiate(NormalRoomList4_6);
        }
        else
        {
            MapLists[Settings.NormalRoom] = Instantiate(NormalRoomList7_9);
        }
        var CardRoomCount = UnityEngine.Random.Range(0, MapLists[Settings.CardRoom].RoomLists.Count);
        var NewChooseCardRoom = new ChooseRoom();
        NewChooseCardRoom.Room = MapLists[Settings.CardRoom].RoomLists[CardRoomCount];
        NewChooseCardRoom.Index = CardRoomCount;
        NewMapRoom.Add(NewChooseCardRoom);
        for(int i = 0;i < 2; i++)
        {
            var TransmissionCount = UnityEngine.Random.Range(0, MapLists[Settings.TransmissionTowerRoom].RoomLists.Count);
            var NewChooseTransmissionRoom = new ChooseRoom();
            NewChooseTransmissionRoom.Room = MapLists[Settings.TransmissionTowerRoom].RoomLists[TransmissionCount];
            NewChooseTransmissionRoom.Index = TransmissionCount;
            NewMapRoom.Add(NewChooseTransmissionRoom);
        }
        var ShopRoomCount = UnityEngine.Random.Range(0, MapLists[Settings.ShopRoom].RoomLists.Count);
        var NewChooseShopRoom = new ChooseRoom();
        NewChooseShopRoom.Room = MapLists[Settings.ShopRoom].RoomLists[ShopRoomCount];
        NewChooseShopRoom.Index = ShopRoomCount;
        NewMapRoom.Add(NewChooseShopRoom);
        for (int i = 0;i < Count - 3; i++)
        {
            var NormalRoomCount = UnityEngine.Random.Range(0, MapLists[Settings.NormalRoom].RoomLists.Count);
            var NewChooseNormalRoom = new ChooseRoom();
            NewChooseNormalRoom.Room = MapLists[Settings.NormalRoom].RoomLists[NormalRoomCount];
            NewChooseNormalRoom.Index = NormalRoomCount;
            NewMapRoom.Add(NewChooseNormalRoom);
        }
        var BossRoomCount = UnityEngine.Random.Range(0, MapLists[Settings.BossRoom].RoomLists.Count);
        var NewChooseBossRoom = new ChooseRoom();
        NewChooseBossRoom.Room = MapLists[Settings.BossRoom].RoomLists[BossRoomCount];
        NewChooseBossRoom.Index = BossRoomCount;
        NewMapRoom.Add(NewChooseBossRoom);
    }
    private void ReFreshShop()
    {
        MapData.Goods1 = GemItem;
        MapData.Goods2 = ChooseExtraGem(UnityEngine.Random.Range(0, 4));
        MapData.Goods3 = HeartItem;
        MapData.Goods4 = ShieldItem;
    }
    private GameObject ChooseExtraGem(int Count)
    {
        switch (Count)
        {
            case 0:
                return ShootGem;
            case 1:
                return DamageGem;
            case 2:
                return SpeedGem;
            case 3:
                return BiggerGem;
        }
        return null;
    }
    public void ClearMonster()
    {
        ClearMonsterEvent.RaiseEvent();
    }
}
