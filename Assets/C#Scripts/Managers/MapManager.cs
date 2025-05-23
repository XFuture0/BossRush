using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingleTons<MapManager>
{
    public Transform MapBox;
    public List<GameObject> MapLists = new List<GameObject>();
    public LayerMask Room;
    public MapData MapData;
    public GameObject TransmissionCamera;
    public GameObject CanvsBox;
    [Header("地图建造属性")]
    private bool IsSetCardRoom;
    public int RandomRoomCount;//大致房间数量
    private int RoomCount;//实际房间数量
    private int CurrentRoomCount;//当前房间数量(建造中)
    [Header("建造计时器")]
    public float BuildTime;
    private bool IsBuild;
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
        var NewRoom = Instantiate(MapLists[Settings.StartRoom], MapBox);
        MapData.Room NewRoomPoint = new MapData.Room();
        NewRoomPoint.RoomPosition = NewRoom.transform.position;
        NewRoomPoint.RoomType = NewRoom.GetComponent<MapCharacrter>().RoomType;
        MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
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
                    yield return new WaitForSeconds(0.02f);
                }
            }
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
    public IEnumerator BuildNewRoom(float Width,float Height,Vector2 RoomCenter,RoomType roomType)
    {
        int SetRotation;
        do
        {
            SetRotation = UnityEngine.Random.Range(0, 100) % 6;
        }
        while (!FixRoomPosition(SetRotation, roomType));
        switch (SetRotation) 
        {
            case Settings.WestNorth:
                var NewRoomType_WestNorth = UnityEngine.Random.Range(1, MapLists.Count - 1);
                if(CurrentRoomCount == RoomCount - 1)
                {
                    NewRoomType_WestNorth = MapLists.Count - 1;
                }
                if (FixNextRoom(SetRotation, NewRoomType_WestNorth) && HadBuild(NewRoomType_WestNorth))
                {
                    var NewRoomPositionX_WestNorth = RoomCenter.x - Width * 0.5f - MapLists[NewRoomType_WestNorth].GetComponent<MapCharacrter>().Width * 0.5f;
                    var NewRoomPositionY_WestNorth = RoomCenter.y + Height * 0.5f;
                    var NewRoomPosition_WestNorth = new Vector3(NewRoomPositionX_WestNorth - 15, NewRoomPositionY_WestNorth + 8, 0);//修改生成点
                    var LeftUpPo_WestNorth = new Vector2(NewRoomPosition_WestNorth.x - MapLists[NewRoomType_WestNorth].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_WestNorth.y + MapLists[NewRoomType_WestNorth].GetComponent<MapCharacrter>().Height * 0.5f);
                    var RightDownPo_WestNorth = new Vector2(NewRoomPosition_WestNorth.x + MapLists[NewRoomType_WestNorth].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_WestNorth.y - MapLists[NewRoomType_WestNorth].GetComponent<MapCharacrter>().Height * 0.5f);
                    NewRoomPosition_WestNorth = new Vector3(NewRoomPositionX_WestNorth, NewRoomPositionY_WestNorth, 0);//确定生成点
                    if (!Physics2D.OverlapArea(LeftUpPo_WestNorth, RightDownPo_WestNorth, Room))//是否出现覆盖
                    {
                        if (NewRoomType_WestNorth == Settings.BossRoom)//是否是boss房
                        {
                            if (CheckRoom(LeftUpPo_WestNorth, RightDownPo_WestNorth, NewRoomType_WestNorth))
                            {
                                var NewRoom = Instantiate(MapLists[NewRoomType_WestNorth], NewRoomPosition_WestNorth, Quaternion.identity, MapBox);//生成新房间
                                CurrentRoomCount++;//当前房间数量加1
                                MapData.Room NewRoomPoint = new MapData.Room();
                                NewRoomPoint.RoomPosition = NewRoom.transform.position;
                                NewRoomPoint.RoomType = NewRoom.GetComponent<MapCharacrter>().RoomType;
                                MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
                            }
                        }
                        if (NewRoomType_WestNorth != Settings.BossRoom)
                        {
                            if (CheckRoom(LeftUpPo_WestNorth, RightDownPo_WestNorth, NewRoomType_WestNorth))
                            {
                                var NewRoom = Instantiate(MapLists[NewRoomType_WestNorth], NewRoomPosition_WestNorth, Quaternion.identity, MapBox);//生成新房间
                                CurrentRoomCount++;//当前房间数量加1
                                MapData.Room NewRoomPoint = new MapData.Room();
                                NewRoomPoint.RoomPosition = NewRoom.transform.position;
                                NewRoomPoint.RoomType = NewRoom.GetComponent<MapCharacrter>().RoomType;
                                MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
                            }
                        }
                    }
                }
                break;
            case Settings.WestCenter:
                var NewRoomType_WestCenter = UnityEngine.Random.Range(1, MapLists.Count - 1);
                if (CurrentRoomCount == RoomCount - 1)
                {
                    NewRoomType_WestCenter = MapLists.Count - 1;
                }
                if(FixNextRoom(SetRotation, NewRoomType_WestCenter) && HadBuild(NewRoomType_WestCenter))
                {
                    var NewRoomPositionX_WestCenter = RoomCenter.x - Width * 0.5f - MapLists[NewRoomType_WestCenter].GetComponent<MapCharacrter>().Width * 0.5f;
                    var NewRoomPositionY_WestCenter = RoomCenter.y;
                    var NewRoomPosition_WestCenter = new Vector3(NewRoomPositionX_WestCenter - 15, NewRoomPositionY_WestCenter + 8, 0);//修改生成点
                    var LeftUpPo_WestCenter = new Vector2(NewRoomPosition_WestCenter.x - MapLists[NewRoomType_WestCenter].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_WestCenter.y + MapLists[NewRoomType_WestCenter].GetComponent<MapCharacrter>().Height * 0.5f);
                    var RightDownPo_WestCenter = new Vector2(NewRoomPosition_WestCenter.x + MapLists[NewRoomType_WestCenter].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_WestCenter.y - MapLists[NewRoomType_WestCenter].GetComponent<MapCharacrter>().Height * 0.5f);
                    NewRoomPosition_WestCenter = new Vector3(NewRoomPositionX_WestCenter, NewRoomPositionY_WestCenter, 0);//确定生成点
                    if (!Physics2D.OverlapArea(LeftUpPo_WestCenter, RightDownPo_WestCenter, Room))//是否出现覆盖
                    {
                        if (NewRoomType_WestCenter == Settings.BossRoom)//是否是boss房
                        {
                            if (CheckRoom(LeftUpPo_WestCenter, RightDownPo_WestCenter, NewRoomType_WestCenter))
                            {
                                var NewRoom = Instantiate(MapLists[NewRoomType_WestCenter], NewRoomPosition_WestCenter, Quaternion.identity, MapBox);//生成新房间
                                CurrentRoomCount++;//当前房间数量加1
                                MapData.Room NewRoomPoint = new MapData.Room();
                                NewRoomPoint.RoomPosition = NewRoom.transform.position;
                                NewRoomPoint.RoomType = NewRoom.GetComponent<MapCharacrter>().RoomType;
                                MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
                            }
                        }
                        if (NewRoomType_WestCenter != Settings.BossRoom)
                        {
                            if (CheckRoom(LeftUpPo_WestCenter, RightDownPo_WestCenter, NewRoomType_WestCenter))
                            {
                                var NewRoom = Instantiate(MapLists[NewRoomType_WestCenter], NewRoomPosition_WestCenter, Quaternion.identity, MapBox);//生成新房间
                                CurrentRoomCount++;//当前房间数量加1
                                MapData.Room NewRoomPoint = new MapData.Room();
                                NewRoomPoint.RoomPosition = NewRoom.transform.position;
                                NewRoomPoint.RoomType = NewRoom.GetComponent<MapCharacrter>().RoomType;
                                MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
                            }
                        }
                    }
                }
                break;
            case Settings.WestSouth:
                var NewRoomType_WestSouth = UnityEngine.Random.Range(1, MapLists.Count - 1);
                if (CurrentRoomCount == RoomCount - 1)
                {
                    NewRoomType_WestSouth = MapLists.Count - 1;
                }
                if(FixNextRoom(SetRotation, NewRoomType_WestSouth) && HadBuild(NewRoomType_WestSouth))
                {
                    var NewRoomPositionX_WestSouth = RoomCenter.x - Width * 0.5f - MapLists[NewRoomType_WestSouth].GetComponent<MapCharacrter>().Width * 0.5f;
                    var NewRoomPositionY_WestSouth = RoomCenter.y - Height * 0.5f;
                    var NewRoomPosition_WestSouth = new Vector3(NewRoomPositionX_WestSouth - 15, NewRoomPositionY_WestSouth + 8, 0);//修改生成点
                    var LeftUpPo_WestSouth = new Vector2(NewRoomPosition_WestSouth.x - MapLists[NewRoomType_WestSouth].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_WestSouth.y + MapLists[NewRoomType_WestSouth].GetComponent<MapCharacrter>().Height * 0.5f);
                    var RightDownPo_WestSouth = new Vector2(NewRoomPosition_WestSouth.x + MapLists[NewRoomType_WestSouth].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_WestSouth.y - MapLists[NewRoomType_WestSouth].GetComponent<MapCharacrter>().Height * 0.5f);
                    NewRoomPosition_WestSouth = new Vector3(NewRoomPositionX_WestSouth, NewRoomPositionY_WestSouth, 0);//确定生成点
                    if (!Physics2D.OverlapArea(LeftUpPo_WestSouth, RightDownPo_WestSouth, Room))//是否出现覆盖
                    {
                        if (NewRoomType_WestSouth == Settings.BossRoom)//是否是boss房
                        {
                            if (CheckRoom(LeftUpPo_WestSouth, RightDownPo_WestSouth, NewRoomType_WestSouth))
                            {
                                var NewRoom = Instantiate(MapLists[NewRoomType_WestSouth], NewRoomPosition_WestSouth, Quaternion.identity, MapBox);//生成新房间
                                CurrentRoomCount++;//当前房间数量加1
                                MapData.Room NewRoomPoint = new MapData.Room();
                                NewRoomPoint.RoomPosition = NewRoom.transform.position;
                                NewRoomPoint.RoomType = NewRoom.GetComponent<MapCharacrter>().RoomType;
                                MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
                            }
                        }
                        if (NewRoomType_WestSouth != Settings.BossRoom)
                        {
                            if (CheckRoom(LeftUpPo_WestSouth, RightDownPo_WestSouth, NewRoomType_WestSouth))
                            {
                                var NewRoom = Instantiate(MapLists[NewRoomType_WestSouth], NewRoomPosition_WestSouth, Quaternion.identity, MapBox);//生成新房间
                                CurrentRoomCount++;//当前房间数量加1
                                MapData.Room NewRoomPoint = new MapData.Room();
                                NewRoomPoint.RoomPosition = NewRoom.transform.position;
                                NewRoomPoint.RoomType = NewRoom.GetComponent<MapCharacrter>().RoomType;
                                MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
                            }
                        }
                    }
                }
                break;
            case Settings.EastNorth:
                var NewRoomType_EastNorth = UnityEngine.Random.Range(1, MapLists.Count - 1);
                if (CurrentRoomCount == RoomCount - 1)
                {
                    NewRoomType_EastNorth = MapLists.Count - 1;
                }
                if(FixNextRoom(SetRotation, NewRoomType_EastNorth) && HadBuild(NewRoomType_EastNorth))
                {
                    var NewRoomPositionX_EastNorth = RoomCenter.x + Width * 0.5f + MapLists[NewRoomType_EastNorth].GetComponent<MapCharacrter>().Width * 0.5f;
                    var NewRoomPositionY_EastNorth = RoomCenter.y + Height * 0.5f;
                    var NewRoomPosition_EastNorth = new Vector3(NewRoomPositionX_EastNorth - 15, NewRoomPositionY_EastNorth + 8, 0);//修改生成点
                    var LeftUpPo_EastNorth = new Vector2(NewRoomPosition_EastNorth.x - MapLists[NewRoomType_EastNorth].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_EastNorth.y + MapLists[NewRoomType_EastNorth].GetComponent<MapCharacrter>().Height * 0.5f);
                    var RightDownPo_EastNorth = new Vector2(NewRoomPosition_EastNorth.x + MapLists[NewRoomType_EastNorth].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_EastNorth.y - MapLists[NewRoomType_EastNorth].GetComponent<MapCharacrter>().Height * 0.5f);
                    NewRoomPosition_EastNorth = new Vector3(NewRoomPositionX_EastNorth, NewRoomPositionY_EastNorth, 0);//确定生成点
                    if (!Physics2D.OverlapArea(LeftUpPo_EastNorth, RightDownPo_EastNorth, Room))//是否出现覆盖
                    {
                        if (NewRoomType_EastNorth == Settings.BossRoom)//是否是boss房
                        {
                            if (CheckRoom(LeftUpPo_EastNorth, RightDownPo_EastNorth, NewRoomType_EastNorth))
                            {
                                var NewRoom = Instantiate(MapLists[NewRoomType_EastNorth], NewRoomPosition_EastNorth, Quaternion.identity, MapBox);//生成新房间
                                CurrentRoomCount++;//当前房间数量加1
                                MapData.Room NewRoomPoint = new MapData.Room();
                                NewRoomPoint.RoomPosition = NewRoom.transform.position;
                                NewRoomPoint.RoomType = NewRoom.GetComponent<MapCharacrter>().RoomType;
                                MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
                            }
                        }
                        if (NewRoomType_EastNorth != Settings.BossRoom)
                        {
                            if (CheckRoom(LeftUpPo_EastNorth, RightDownPo_EastNorth, NewRoomType_EastNorth))
                            {
                                var NewRoom = Instantiate(MapLists[NewRoomType_EastNorth], NewRoomPosition_EastNorth, Quaternion.identity, MapBox);//生成新房间
                                CurrentRoomCount++;//当前房间数量加1
                                MapData.Room NewRoomPoint = new MapData.Room();
                                NewRoomPoint.RoomPosition = NewRoom.transform.position;
                                NewRoomPoint.RoomType = NewRoom.GetComponent<MapCharacrter>().RoomType;
                                MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
                            }
                        }
                    }
                }
                break;
            case Settings.EastCenter:
                var NewRoomType_EastCenter = UnityEngine.Random.Range(1, MapLists.Count - 1);
                if (CurrentRoomCount == RoomCount - 1)
                {
                    NewRoomType_EastCenter = MapLists.Count - 1;
                }
                if(FixNextRoom(SetRotation, NewRoomType_EastCenter) && HadBuild(NewRoomType_EastCenter))
                {
                    var NewRoomPositionX_EastCenter = RoomCenter.x + Width * 0.5f + MapLists[NewRoomType_EastCenter].GetComponent<MapCharacrter>().Width * 0.5f;
                    var NewRoomPositionY_EastCenter = RoomCenter.y;
                    var NewRoomPosition_EastCenter = new Vector3(NewRoomPositionX_EastCenter - 15, NewRoomPositionY_EastCenter + 8, 0);//修改生成点
                    var LeftUpPo_EastCenter = new Vector2(NewRoomPosition_EastCenter.x - MapLists[NewRoomType_EastCenter].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_EastCenter.y + MapLists[NewRoomType_EastCenter].GetComponent<MapCharacrter>().Height * 0.5f);
                    var RightDownPo_EastCenter = new Vector2(NewRoomPosition_EastCenter.x + MapLists[NewRoomType_EastCenter].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_EastCenter.y - MapLists[NewRoomType_EastCenter].GetComponent<MapCharacrter>().Height * 0.5f);
                    NewRoomPosition_EastCenter = new Vector3(NewRoomPositionX_EastCenter, NewRoomPositionY_EastCenter, 0);//确定生成点
                    if (!Physics2D.OverlapArea(LeftUpPo_EastCenter, RightDownPo_EastCenter, Room))//是否出现覆盖
                    {
                        if (NewRoomType_EastCenter == Settings.BossRoom)//是否是boss房
                        {
                            if (CheckRoom(LeftUpPo_EastCenter, RightDownPo_EastCenter, NewRoomType_EastCenter))
                            {
                                var NewRoom = Instantiate(MapLists[NewRoomType_EastCenter], NewRoomPosition_EastCenter, Quaternion.identity, MapBox);//生成新房间
                                CurrentRoomCount++;//当前房间数量加1
                                MapData.Room NewRoomPoint = new MapData.Room();
                                NewRoomPoint.RoomPosition = NewRoom.transform.position;
                                NewRoomPoint.RoomType = NewRoom.GetComponent<MapCharacrter>().RoomType;
                                MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
                            }
                        }
                        if (NewRoomType_EastCenter != Settings.BossRoom)
                        {
                            if (CheckRoom(LeftUpPo_EastCenter, RightDownPo_EastCenter, NewRoomType_EastCenter))
                            {
                                var NewRoom = Instantiate(MapLists[NewRoomType_EastCenter], NewRoomPosition_EastCenter, Quaternion.identity, MapBox);//生成新房间
                                CurrentRoomCount++;//当前房间数量加1
                                MapData.Room NewRoomPoint = new MapData.Room();
                                NewRoomPoint.RoomPosition = NewRoom.transform.position;
                                NewRoomPoint.RoomType = NewRoom.GetComponent<MapCharacrter>().RoomType;
                                MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
                            }
                        }
                    }
                }
                break;
            case Settings.EastSouth:
                var NewRoomType_EastSouth = UnityEngine.Random.Range(1, MapLists.Count - 1);
                if (CurrentRoomCount == RoomCount - 1)
                {
                    NewRoomType_EastSouth = MapLists.Count - 1;
                }
                if(FixNextRoom(SetRotation, NewRoomType_EastSouth) &&  HadBuild(NewRoomType_EastSouth))
                {
                    var NewRoomPositionX_EastSouth = RoomCenter.x + Width * 0.5f + MapLists[NewRoomType_EastSouth].GetComponent<MapCharacrter>().Width * 0.5f;
                    var NewRoomPositionY_EastSouth = RoomCenter.y - Height * 0.5f;
                    var NewRoomPosition_EastSouth = new Vector3(NewRoomPositionX_EastSouth - 15, NewRoomPositionY_EastSouth + 8, 0);//修改生成点
                    var LeftUpPo_EastSouth = new Vector2(NewRoomPosition_EastSouth.x - MapLists[NewRoomType_EastSouth].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_EastSouth.y + MapLists[NewRoomType_EastSouth].GetComponent<MapCharacrter>().Height * 0.5f);
                    var RightDownPo_EastSouth = new Vector2(NewRoomPosition_EastSouth.x + MapLists[NewRoomType_EastSouth].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_EastSouth.y - MapLists[NewRoomType_EastSouth].GetComponent<MapCharacrter>().Height * 0.5f);
                    NewRoomPosition_EastSouth = new Vector3(NewRoomPositionX_EastSouth, NewRoomPositionY_EastSouth, 0);//确定生成点
                    if (!Physics2D.OverlapArea(LeftUpPo_EastSouth, RightDownPo_EastSouth, Room))//是否出现覆盖
                    {
                        if (NewRoomType_EastSouth == Settings.BossRoom)//是否是boss房
                        {
                            if (CheckRoom(LeftUpPo_EastSouth, RightDownPo_EastSouth, NewRoomType_EastSouth))
                            {
                                var NewRoom = Instantiate(MapLists[NewRoomType_EastSouth], NewRoomPosition_EastSouth, Quaternion.identity, MapBox);//生成新房间
                                CurrentRoomCount++;//当前房间数量加1
                                MapData.Room NewRoomPoint = new MapData.Room();
                                NewRoomPoint.RoomPosition = NewRoom.transform.position;
                                NewRoomPoint.RoomType = NewRoom.GetComponent<MapCharacrter>().RoomType;
                                MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
                            }
                        }
                        else if (NewRoomType_EastSouth != Settings.BossRoom)
                        {
                            if (CheckRoom(LeftUpPo_EastSouth, RightDownPo_EastSouth, NewRoomType_EastSouth))
                            {
                                var NewRoom = Instantiate(MapLists[NewRoomType_EastSouth], NewRoomPosition_EastSouth, Quaternion.identity, MapBox);//生成新房间
                                CurrentRoomCount++;//当前房间数量加1
                                MapData.Room NewRoomPoint = new MapData.Room();
                                NewRoomPoint.RoomPosition = NewRoom.transform.position;
                                NewRoomPoint.RoomType = NewRoom.GetComponent<MapCharacrter>().RoomType;
                                MapData.RoomLists.Add(NewRoomPoint);//添加房间数据
                            }
                           
                        }
                    }
                }
                break;
            default:
                break;
        }
        yield return null;
    }
    private bool HadBuild(int roomType)
    {
        if(roomType == Settings.CardRoom && IsSetCardRoom)
        {
            return false;
        }
        return true;
    }
    private bool CheckRoom(Vector2 LeftUpPo,Vector2 RightDownPo,int roomType)
    {
        switch (roomType)
        {
            case Settings.StartRoom:
                return true;
            case Settings.NormalRoom:
                return true;
            case Settings.CardRoom:
                LeftUpPo += new Vector2(-60, 5);
                RightDownPo += new Vector2(60, -5);
                var CardRoomRays = Physics2D.OverlapAreaAll(LeftUpPo, RightDownPo, Room);
                foreach (var room in CardRoomRays)
                {
                    if (room.gameObject.GetComponent<MapCharacrter>().RoomType == RoomType.StartRoom)
                    {
                        return false;
                    }
                }
                IsSetCardRoom = true;
                return true;
            case Settings.TransmissionTowerRoom:
                LeftUpPo += new Vector2(-60, 5);
                RightDownPo += new Vector2(60, -5);
                var TransmissionRays = Physics2D.OverlapAreaAll(LeftUpPo, RightDownPo, Room);
                foreach  (var room in TransmissionRays)
                {
                    if (room.gameObject.GetComponent<MapCharacrter>().RoomType == RoomType.TransmissionTowerRoom || room.gameObject.GetComponent<MapCharacrter>().RoomType == RoomType.StartRoom)
                    {
                        return false;
                    }
                }
                return true;
            case Settings.BossRoom:
                LeftUpPo += new Vector2(-5, 5);
                RightDownPo += new Vector2(5, -5);
                var rays = Physics2D.OverlapAreaAll(LeftUpPo, RightDownPo, Room);
                if (rays.Length == 2)
                {
                    return true;
                }
                break;
            default:
                break;
        }
        return false;
    }
    public bool FixRoomPosition(int SetRotation,RoomType roomType)//防止房间无法抵达下一个房间
    {
        switch (roomType)
        {
            case RoomType.StartRoom:
                if (SetRotation == Settings.WestCenter || SetRotation == Settings.WestSouth || SetRotation == Settings.EastCenter || SetRotation == Settings.EastSouth)
                {
                    return true;
                }
                break;
            case RoomType.NormalRoom:
                return true;
            case RoomType.CardRoom:
                return true;
            case RoomType.BossRoom:
                return true;
            case RoomType.TransmissionTowerRoom:
                if (SetRotation == Settings.WestCenter || SetRotation == Settings.WestSouth || SetRotation == Settings.EastCenter || SetRotation == Settings.EastSouth)
                {
                    return true;
                }
                break;
            default:
                break;
        }
        return false;
    }
    public bool FixNextRoom(int BaseRotation,int NextRoom)//防止下一个房间无法和上一个房间相连
    {
        switch (BaseRotation)
        {
            case Settings.WestNorth:
                if (true)
                {
                    return true;
                }
            case Settings.WestCenter:
                if (true)
                {
                    return true;
                }
            case Settings.WestSouth:
                if(NextRoom == Settings.BossRoom || NextRoom == Settings.NormalRoom)
                {
                    return true;
                }
                break;
            case Settings.EastNorth:
                if (true)
                {
                    return true;
                }
            case Settings.EastCenter:
                if (true)
                {
                    return true;
                }
            case Settings.EastSouth:
                if (NextRoom == Settings.BossRoom || NextRoom == Settings.NormalRoom)
                {
                    return true;
                }
                break;
        }
        return false;
    }
    public void SetRoomData()
    {
        for (int i = 0; i < MapBox.childCount; i++)
        {
            Destroy(MapBox.GetChild(i).gameObject);
        }
        foreach (var Room in MapData.RoomLists)
        {
            Instantiate(MapLists[(int)Room.RoomType],Room.RoomPosition, Quaternion.identity, MapBox);
        }//重建地图
        CheckFindRoom();
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
}
