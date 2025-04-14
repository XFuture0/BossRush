using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
[CreateAssetMenu(fileName ="New CharacterData",menuName ="Data/CharacterData")]
public class CharacterData : ScriptableObject
{
    public float MaxHealth;//总生命值
    public float NowHealth;//当前生命值
    public float Speed;//移速
    public float AttackPower;//攻击力
    public float InvincibleTime;//无敌时间
    public float HealCount;//治疗量
    public float AutoHealTime;//自动回复时间
    public float AutoHealCount;//自动回复量
    public float CriticalDamageRate;//暴击率
    public float CriticalDamageBonus;//暴击加成
    public float DodgeRate;//闪避率
    public float AttackRate;//攻击频率
}
