using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New MapList", menuName = "List/MapList")]
public class MapData : ScriptableObject
{
    [System.Serializable]
    public class Room 
    {
        public bool IsAccess;//�Ƿ���ͨ��
        public bool IsFind;//�Ƿ��ѷ���
        public Vector3 RoomPosition;
        public RoomType RoomType;
        public int RoomIndex;
        public int Index;
        public List<MapCharacrter.Door> LeftDoor = new List<MapCharacrter.Door>();//�����
        public List<MapCharacrter.Door> RightDoor = new List<MapCharacrter.Door>();//�Ҳ���
    }
    public List<Room> RoomLists = new List<Room>();
}
