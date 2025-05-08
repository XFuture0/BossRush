using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    public int RoomCount;//总房间数
    public int CurrentRoomCount;//当前房间数
    public float JumpForce;
    public float JumpDownSpeed_Max;
    public int CoinCount;//金币数
    public int ScoreCount;//得分数
}
