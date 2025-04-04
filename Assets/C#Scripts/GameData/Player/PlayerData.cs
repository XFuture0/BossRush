using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New PlayerData", menuName = "Data/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float Speed;
    public float JumpForce;
    public float JumpDownSpeed_Max;
}
