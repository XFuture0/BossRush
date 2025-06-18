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
    public bool StartGame;//游戏开始
    public Vector3 PlayerPosition;//玩家位置
    public RoomType RoomType;//玩家当前房间
    public SceneData CurrentScene;//当前场景
    public HatList.HatData HatData;//帽子数据
    public CharacterList.CharacterData CharacterData;//特质数据
    public WeaponList.WeaponData WeaponData;//武器数据
    public ColorData.Colordata CurrentColor;//颜色数据
    [Header("小队成员")]
    public SlimeData Player;
    public SlimeData Teamer1;
    public SlimeData Teamer2;
    public SlimeData Teamer3;
}
