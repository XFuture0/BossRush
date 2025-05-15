using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingleTons<MapManager>
{
    public Transform MapBox;
    public List<GameObject> MapLists = new List<GameObject>();
    public LayerMask Room;
    [Header("地图建造属性")]
    public int RandomRoomCount;//大致房间数量
    private int RoomCount;//实际房间数量
    private int CurrentRoomCount;//当前房间数量(建造中)
    private void Start()
    {
        SetNewMap();
    }
    public void SetNewMap()
    {
        RoomCount = UnityEngine.Random.Range((int)(RandomRoomCount * 0.8f), (int)(RandomRoomCount * 1.2f));//确定当前房间数量
        CurrentRoomCount = 0;
        for(int i = 0; i < MapBox.childCount; i++)
        {
            Destroy(MapBox.GetChild(i).gameObject);
        }//清除原有地图
        Instantiate(MapLists[Settings.StartRoom], MapBox);
        StartCoroutine(SetNewRoom());
    }
    private IEnumerator SetNewRoom()
    {
        for(int i = 0; i < MapBox.childCount; i++)
        {
            if(CurrentRoomCount < RandomRoomCount)
            {
                var NewRoomType = UnityEngine.Random.Range(0, 100) % MapLists.Count;
                MapBox.GetChild(i).gameObject.GetComponent<MapCharacrter>().BuildNewRoom();
            }
        }
        if(CurrentRoomCount < RandomRoomCount)
        {
           yield return new WaitForSeconds(1f);
           StartCoroutine(SetNewRoom());//迭代
        }
    }
    public IEnumerator BuildNewRoom(float Width,float Height,Vector2 RoomCenter,RoomType RoomType,DoorType DoorType)
    {
        var SetRotation = UnityEngine.Random.Range(0, 100) % 6;
        switch (SetRotation) 
        {
            case Settings.WestNorth:
                var NewRoomType_WestNorth = UnityEngine.Random.Range(0, 100) % MapLists.Count;
                var NewRoomPositionX_WestNorth = RoomCenter.x - Width * 0.5f - MapLists[NewRoomType_WestNorth].GetComponent<MapCharacrter>().Width * 0.5f;
                var NewRoomPositionY_WestNorth = RoomCenter.y + Height * 0.5f;
                var NewRoomPosition_WestNorth = new Vector3(NewRoomPositionX_WestNorth - 15, NewRoomPositionY_WestNorth + 8, 0);//修改生成点
                var LeftUpPo_WestNorth = new Vector2(NewRoomPosition_WestNorth.x - MapLists[NewRoomType_WestNorth].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_WestNorth.y + MapLists[NewRoomType_WestNorth].GetComponent<MapCharacrter>().Height * 0.5f);
                var RightDownPo_WestNorth = new Vector2(NewRoomPosition_WestNorth.x + MapLists[NewRoomType_WestNorth].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_WestNorth.y - MapLists[NewRoomType_WestNorth].GetComponent<MapCharacrter>().Height * 0.5f);
                NewRoomPosition_WestNorth = new Vector3(NewRoomPositionX_WestNorth, NewRoomPositionY_WestNorth, 0);//确定生成点
                if (!Physics2D.OverlapArea(LeftUpPo_WestNorth, RightDownPo_WestNorth, Room))//是否出现覆盖
                {
                    Instantiate(MapLists[NewRoomType_WestNorth], NewRoomPosition_WestNorth, Quaternion.identity,MapBox);//生成新房间
                    CurrentRoomCount++;//当前房间数量加1
                }
                break;
            case Settings.WestCenter:
                var NewRoomType_WestCenter = UnityEngine.Random.Range(0, 100) % MapLists.Count;
                var NewRoomPositionX_WestCenter = RoomCenter.x - Width * 0.5f - MapLists[NewRoomType_WestCenter].GetComponent<MapCharacrter>().Width * 0.5f;
                var NewRoomPositionY_WestCenter = RoomCenter.y;
                var NewRoomPosition_WestCenter = new Vector3(NewRoomPositionX_WestCenter - 15, NewRoomPositionY_WestCenter + 8, 0);//修改生成点
                var LeftUpPo_WestCenter = new Vector2(NewRoomPosition_WestCenter.x - MapLists[NewRoomType_WestCenter].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_WestCenter.y + MapLists[NewRoomType_WestCenter].GetComponent<MapCharacrter>().Height * 0.5f);
                var RightDownPo_WestCenter = new Vector2(NewRoomPosition_WestCenter.x + MapLists[NewRoomType_WestCenter].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_WestCenter.y - MapLists[NewRoomType_WestCenter].GetComponent<MapCharacrter>().Height * 0.5f);
                NewRoomPosition_WestCenter = new Vector3(NewRoomPositionX_WestCenter, NewRoomPositionY_WestCenter, 0);//确定生成点
                if (!Physics2D.OverlapArea(LeftUpPo_WestCenter, RightDownPo_WestCenter, Room))//是否出现覆盖
                {
                    Instantiate(MapLists[NewRoomType_WestCenter], NewRoomPosition_WestCenter, Quaternion.identity, MapBox);//生成新房间
                    CurrentRoomCount++;//当前房间数量加1
                }
                break;
            case Settings.WestSouth:
                var NewRoomType_WestSouth = UnityEngine.Random.Range(0, 100) % MapLists.Count;
                var NewRoomPositionX_WestSouth = RoomCenter.x - Width * 0.5f - MapLists[NewRoomType_WestSouth].GetComponent<MapCharacrter>().Width * 0.5f;
                var NewRoomPositionY_WestSouth = RoomCenter.y - Height * 0.5f;
                var NewRoomPosition_WestSouth = new Vector3(NewRoomPositionX_WestSouth - 15, NewRoomPositionY_WestSouth + 8, 0);//修改生成点
                var LeftUpPo_WestSouth = new Vector2(NewRoomPosition_WestSouth.x - MapLists[NewRoomType_WestSouth].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_WestSouth.y + MapLists[NewRoomType_WestSouth].GetComponent<MapCharacrter>().Height * 0.5f);
                var RightDownPo_WestSouth = new Vector2(NewRoomPosition_WestSouth.x + MapLists[NewRoomType_WestSouth].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_WestSouth.y - MapLists[NewRoomType_WestSouth].GetComponent<MapCharacrter>().Height * 0.5f);
                NewRoomPosition_WestSouth = new Vector3(NewRoomPositionX_WestSouth, NewRoomPositionY_WestSouth, 0);//确定生成点
                if (!Physics2D.OverlapArea(LeftUpPo_WestSouth, RightDownPo_WestSouth, Room))//是否出现覆盖
                {
                    Instantiate(MapLists[NewRoomType_WestSouth], NewRoomPosition_WestSouth, Quaternion.identity, MapBox);//生成新房间
                    CurrentRoomCount++;//当前房间数量加1
                }
                break;
            case Settings.EastNorth:
                var NewRoomType_EastNorth = UnityEngine.Random.Range(0, 100) % MapLists.Count;
                var NewRoomPositionX_EastNorth = RoomCenter.x + Width * 0.5f + MapLists[NewRoomType_EastNorth].GetComponent<MapCharacrter>().Width * 0.5f;
                var NewRoomPositionY_EastNorth = RoomCenter.y + Height * 0.5f;
                var NewRoomPosition_EastNorth = new Vector3(NewRoomPositionX_EastNorth - 15, NewRoomPositionY_EastNorth + 8, 0);//修改生成点
                var LeftUpPo_EastNorth = new Vector2(NewRoomPosition_EastNorth.x - MapLists[NewRoomType_EastNorth].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_EastNorth.y + MapLists[NewRoomType_EastNorth].GetComponent<MapCharacrter>().Height * 0.5f);
                var RightDownPo_EastNorth = new Vector2(NewRoomPosition_EastNorth.x + MapLists[NewRoomType_EastNorth].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_EastNorth.y - MapLists[NewRoomType_EastNorth].GetComponent<MapCharacrter>().Height * 0.5f);
                NewRoomPosition_EastNorth = new Vector3(NewRoomPositionX_EastNorth, NewRoomPositionY_EastNorth, 0);//确定生成点
                if (!Physics2D.OverlapArea(LeftUpPo_EastNorth, RightDownPo_EastNorth, Room))//是否出现覆盖
                {
                    Instantiate(MapLists[NewRoomType_EastNorth], NewRoomPosition_EastNorth, Quaternion.identity, MapBox);//生成新房间
                    CurrentRoomCount++;//当前房间数量加1
                }
                break;
            case Settings.EastCenter:
                var NewRoomType_EastCenter = UnityEngine.Random.Range(0, 100) % MapLists.Count;
                var NewRoomPositionX_EastCenter = RoomCenter.x + Width * 0.5f + MapLists[NewRoomType_EastCenter].GetComponent<MapCharacrter>().Width * 0.5f;
                var NewRoomPositionY_EastCenter = RoomCenter.y;
                var NewRoomPosition_EastCenter = new Vector3(NewRoomPositionX_EastCenter - 15, NewRoomPositionY_EastCenter + 8, 0);//修改生成点
                var LeftUpPo_EastCenter = new Vector2(NewRoomPosition_EastCenter.x - MapLists[NewRoomType_EastCenter].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_EastCenter.y + MapLists[NewRoomType_EastCenter].GetComponent<MapCharacrter>().Height * 0.5f);
                var RightDownPo_EastCenter = new Vector2(NewRoomPosition_EastCenter.x + MapLists[NewRoomType_EastCenter].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_EastCenter.y - MapLists[NewRoomType_EastCenter].GetComponent<MapCharacrter>().Height * 0.5f);
                NewRoomPosition_EastCenter = new Vector3(NewRoomPositionX_EastCenter, NewRoomPositionY_EastCenter, 0);//确定生成点
                if (!Physics2D.OverlapArea(LeftUpPo_EastCenter, RightDownPo_EastCenter, Room))//是否出现覆盖
                {
                    Instantiate(MapLists[NewRoomType_EastCenter], NewRoomPosition_EastCenter, Quaternion.identity, MapBox);//生成新房间
                    CurrentRoomCount++;//当前房间数量加1
                }
                break;
            case Settings.EastSouth:
                var NewRoomType_EastSouth = UnityEngine.Random.Range(0, 100) % MapLists.Count;
                var NewRoomPositionX_EastSouth = RoomCenter.x + Width * 0.5f + MapLists[NewRoomType_EastSouth].GetComponent<MapCharacrter>().Width * 0.5f;
                var NewRoomPositionY_EastSouth = RoomCenter.y + Height * 0.5f;
                var NewRoomPosition_EastSouth = new Vector3(NewRoomPositionX_EastSouth - 15, NewRoomPositionY_EastSouth + 8, 0);//修改生成点
                var LeftUpPo_EastSouth = new Vector2(NewRoomPosition_EastSouth.x - MapLists[NewRoomType_EastSouth].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_EastSouth.y + MapLists[NewRoomType_EastSouth].GetComponent<MapCharacrter>().Height * 0.5f);
                var RightDownPo_EastSouth = new Vector2(NewRoomPosition_EastSouth.x + MapLists[NewRoomType_EastSouth].GetComponent<MapCharacrter>().Width * 0.5f, NewRoomPosition_EastSouth.y - MapLists[NewRoomType_EastSouth].GetComponent<MapCharacrter>().Height * 0.5f);
                NewRoomPosition_EastSouth = new Vector3(NewRoomPositionX_EastSouth, NewRoomPositionY_EastSouth, 0);//确定生成点
                if (!Physics2D.OverlapArea(LeftUpPo_EastSouth, RightDownPo_EastSouth, Room))//是否出现覆盖
                {
                    Instantiate(MapLists[NewRoomType_EastSouth], NewRoomPosition_EastSouth, Quaternion.identity, MapBox);//生成新房间
                    CurrentRoomCount++;//当前房间数量加1
                }
                break;
            default:
                break;
        }
        yield return null;
    }
}
