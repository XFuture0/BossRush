using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New RoomList", menuName = "List/RoomList")]
public class RoomList : ScriptableObject
{
    public List<GameObject> RoomLists = new List<GameObject>();
}
