using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New CharacterData",menuName ="Data/CharacterData")]
public class CharacterData : ScriptableObject
{
    public float MaxHealth;
    public float NowHealth;
    public float AttackPower;//攻击力
    public float InvincibleTime;//无敌时间
    public float HealCount;//治疗量
}
