using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New MapList", menuName = "List/MapList")]
public class MapData : ScriptableObject
{
    [System.Serializable]
    public class Room 
    {
        public bool IsAccess;//是否已通过
        public bool IsFind;//是否已发现
        public Vector3 RoomPosition;
        public RoomType RoomType;
        public int RoomIndex;
        public int Index;
        public List<MapCharacrter.Door> LeftDoor = new List<MapCharacrter.Door>();//左侧门
        public List<MapCharacrter.Door> RightDoor = new List<MapCharacrter.Door>();//右侧门
    }
    public List<Room> RoomLists = new List<Room>();
}
