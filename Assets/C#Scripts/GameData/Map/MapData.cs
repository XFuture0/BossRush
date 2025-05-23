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
    }
    public List<Room> RoomLists = new List<Room>();
}
