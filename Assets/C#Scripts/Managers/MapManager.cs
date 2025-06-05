using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class MapManager : SingleTons<MapManager>
{
    public Transform MapBox;
    public List<RoomList> MapLists = new List<RoomList>();
    public LayerMask Room;
    public MapData MapData;
    public ItemList itemList;
    public GameObject TransmissionCamera;
    public GameObject CanvsBox;
    public Transform ItemBox;
    private int FinishCount;//防止房间自封闭
    [Header("地图建造属性")]
    private bool IsSetCardRoom;
    public int RandomRoomCount;//大致房间数量
    private int RoomCount;//实际房间数量
    private int CurrentRoomCount;//当前房间数量(建造中)
    [Header("建造计时器")]
    public float BuildTime;
    private bool IsBuild;
    [Header("物品列表")]
    public GameObject Coin;
    [Header("广播")]
    public VoidEventSO SaveItemEvent;
    private void Update()
    {
        if (IsBuild)
        {
            BuildTime += Time.deltaTime;
        }
    }
    public void SetNewMap() 
    {
        BuildTime = 0;
        IsBuild = true;
        IsSetCardRoom = false;
        RoomCount = UnityEngine.Random.Range((int)(RandomRoomCount * 0.8f), (int)(RandomRoomCount * 1.2f));//确定当前房间数量
        CurrentRoomCount = 0;
        ClearMap();
        var NewRoomCount = UnityEngine.Random.Range(0, MapLists[Settings.StartRoom].RoomLists.Count);
        var NewRoom = Instantiate(MapLists[Settings.StartRoom].RoomLists[NewRoomCount], MapBox);
        NewRoom.GetComponent<MapCharacrter>().Index = 0;
        MapData.Room NewRoomPoint = new MapData.Room();
        NewRoomPoint.RoomPosition = NewRoom.transform.position;
        NewRoomPoint.RoomType = RoomType.StartRoom;
        NewRoomPoint.RoomIndex = 0;
        NewRoomPoint.Index = NewRoomCount;
        NewRoomPoint.LeftDoor = NewRoom.GetComponent<MapCharacrter>().LeftDoor;
        NewRoomPoint.RightDoor = NewRoom.GetComponent<MapCharacrter>().RightDoor;//保存门的开启状态
        MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
        StartCoroutine(SetNewRoom());
    }
    private IEnumerator SetNewRoom()
    {
        Debug.Log(BuildTime);
        FinishCount = 0;
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
                else if (!MapBox.GetChild(i).gameObject.GetComponent<MapCharacrter>().CheckCanBuildRoom())
                {
                    FinishCount++;
                }
            }
        }
        if(FinishCount == MapBox.childCount)
        {
            StopAllCoroutines();
            ClearMap();
            yield return new WaitForSeconds(0.1f);
            SetNewMap();//重新建造
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
                var NewRoomType_LeftDoor = UnityEngine.Random.Range(0f,1f);
                NewRoomType_LeftDoor = ChooseRoomType(NewRoomType_LeftDoor);// 随机选择房间类型(权重)
                if (CurrentRoomCount == RoomCount - 1)
                {
                    NewRoomType_LeftDoor = MapLists.Count - 1;
                }//选择房间类型
                if (HadBuild((int)NewRoomType_LeftDoor))
                {
                    var NewRoomCount = UnityEngine.Random.Range(0, MapLists[(int)NewRoomType_LeftDoor].RoomLists.Count);
                    var NewRoom = Instantiate(MapLists[(int)NewRoomType_LeftDoor].RoomLists[NewRoomCount],MapBox);//生成新房间
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
                        if (NewRoomType_LeftDoor == Settings.BossRoom)//是否是boss房
                        {
                            MapData.Room NewRoomPoint = new MapData.Room();
                            NewRoomPoint.RoomPosition = NewRoom.transform.position;
                            NewRoomPoint.RoomType = NewRoom.GetComponent<MapCharacrter>().RoomType;
                            NewRoomPoint.RoomIndex = CurrentRoomCount;
                            NewRoomPoint.Index = NewRoomCount;
                            NewRoomPoint.LeftDoor = NewRoom.GetComponent<MapCharacrter>().LeftDoor;
                            NewRoomPoint.RightDoor = NewRoom.GetComponent<MapCharacrter>().RightDoor;//保存门的开启状态
                            MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
                        }
                        if (NewRoomType_LeftDoor != Settings.BossRoom)
                        { 
                            MapData.Room NewRoomPoint = new MapData.Room();
                            NewRoomPoint.RoomPosition = NewRoom.transform.position;
                            NewRoomPoint.RoomType = NewRoom.GetComponent<MapCharacrter>().RoomType;
                            NewRoomPoint.RoomIndex = CurrentRoomCount;
                            NewRoomPoint.Index = NewRoomCount;
                            NewRoomPoint.LeftDoor = NewRoom.GetComponent<MapCharacrter>().LeftDoor;
                            NewRoomPoint.RightDoor = NewRoom.GetComponent<MapCharacrter>().RightDoor;//保存门的开启状态
                            MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
                        }
                    }
                    else if (!CheckOverride(NewRoom.GetComponent<MapCharacrter>().polygonCollider, NewRoom.transform.position))
                    {
                        Destroy(NewRoom);
                    }
                }
                break;
            case DoorType.RightDoor:
                var NewRoomType_RightDoor = UnityEngine.Random.Range(0f,1f);
                NewRoomType_RightDoor = ChooseRoomType(NewRoomType_RightDoor);// 随机选择房间类型(权重)
                if (CurrentRoomCount == RoomCount - 1)
                {
                    NewRoomType_RightDoor = MapLists.Count - 1;
                }
                if(HadBuild((int)NewRoomType_RightDoor))
                {
                    var NewRoomCount = UnityEngine.Random.Range(0, MapLists[(int)NewRoomType_RightDoor].RoomLists.Count);
                    var NewRoom = Instantiate(MapLists[(int)NewRoomType_RightDoor].RoomLists[NewRoomCount], MapBox);//生成新房间
                    NewRoom.layer = 0;
                    var NewLeftDoorCount = UnityEngine.Random.Range(0, NewRoom.GetComponent<MapCharacrter>().LeftDoor.Count);//新的门
                    if (NewRoom.GetComponent<MapCharacrter>().LeftDoor.Count == 0)
                    {
                        Destroy(NewRoom);
                        yield break;
                    }
                    var LeftCenterOffect = NewRoom.transform.position - NewRoom.GetComponent<MapCharacrter>().LeftDoor[NewLeftDoorCount].DoorObject.transform.position;//与左门的偏移
                    NewRoom.transform.position = DoorPosition + LeftCenterOffect + new Vector3(4, 0, 0);//确定新房间的位置
                    yield return new WaitForSeconds(0.02f);
                    if (CheckOverride(NewRoom.GetComponent<MapCharacrter>().polygonCollider, NewRoom.transform.position))//是否出现覆盖
                    {
                        CurrentRoomCount++;//当前房间数量加1
                        NewRoom.layer = 13;
                        CurrentRoom.RightDoor[CurrentRoomDoorCount].DoorObject.transform.GetChild(0).gameObject.SetActive(false);
                        CurrentRoom.RightDoor[CurrentRoomDoorCount].DoorOpen = true;
                        MapData.RoomLists[CurrentRoom.Index].RightDoor = CurrentRoom.RightDoor;//保存门的开启状态
                        NewRoom.GetComponent<MapCharacrter>().LeftDoor[NewLeftDoorCount].DoorObject.transform.GetChild(0).gameObject.SetActive(false);
                        NewRoom.GetComponent<MapCharacrter>().LeftDoor[NewLeftDoorCount].DoorOpen = true;
                        NewRoom.GetComponent<MapCharacrter>().Index = CurrentRoomCount;
                        if (NewRoomType_RightDoor == Settings.BossRoom)//是否是boss房
                        {
                            MapData.Room NewRoomPoint = new MapData.Room();
                            NewRoomPoint.RoomPosition = NewRoom.transform.position;
                            NewRoomPoint.RoomType = NewRoom.GetComponent<MapCharacrter>().RoomType;
                            NewRoomPoint.RoomIndex = CurrentRoomCount;
                            NewRoomPoint.Index = NewRoomCount;
                            NewRoomPoint.LeftDoor = NewRoom.GetComponent<MapCharacrter>().LeftDoor;
                            NewRoomPoint.RightDoor = NewRoom.GetComponent<MapCharacrter>().RightDoor;//保存门的开启状态
                            MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
                        }
                        if (NewRoomType_RightDoor != Settings.BossRoom)
                        {
                            MapData.Room NewRoomPoint = new MapData.Room();
                            NewRoomPoint.RoomPosition = NewRoom.transform.position;
                            NewRoomPoint.RoomType = NewRoom.GetComponent<MapCharacrter>().RoomType;
                            NewRoomPoint.RoomIndex = CurrentRoomCount;
                            NewRoomPoint.Index = NewRoomCount;
                            NewRoomPoint.LeftDoor = NewRoom.GetComponent<MapCharacrter>().LeftDoor;
                            NewRoomPoint.RightDoor = NewRoom.GetComponent<MapCharacrter>().RightDoor;//保存门的开启状态
                            MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
                        }
                    }
                    else if (!CheckOverride(NewRoom.GetComponent<MapCharacrter>().polygonCollider, NewRoom.transform.position))
                    {
                        Destroy(NewRoom);
                    }
                }
                break;
            default:
                break;
        }
        yield return null;
    }
    private int ChooseRoomType(float RandomNumber)
    {
        if(RandomNumber >= 0 && RandomNumber < 0.1f)
        {
            return Settings.CardRoom;
        }
        if(RandomNumber >= 0.1f && RandomNumber < 0.3f)
        {
            return Settings.TransmissionTowerRoom;
        }
        if(RandomNumber >= 0.3f && RandomNumber <= 1)
        {
            return Settings.NormalRoom;
        }
        return 0;
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
    private bool HadBuild(int roomType)
    {
        if(roomType == Settings.CardRoom && IsSetCardRoom)
        {
            return false;
        }
        return true;
    }
    public void SetRoomData()
    {
        for (int i = 0; i < MapBox.childCount; i++)
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
        for (int i = 0; i < MapBox.childCount; i++)
        {
            Destroy(MapBox.GetChild(i).gameObject);
        }
        MapData.RoomLists.Clear();
        //清除原有地图
    }
    public void AccessRoom(Vector3 RoomPosition)//房间已通过
    {
        foreach (var room in MapData.RoomLists)
        {
            if(room.RoomPosition == RoomPosition)
            {
                room.IsAccess = true;
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
                var NewItem = Instantiate(Coin,item.ItemPosition,Quaternion.identity,ItemBox);
                NewItem.GetComponent<Coin>().Thisitem.Index = item.Index;
                break;
        }
    }
}
