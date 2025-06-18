using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
[CreateAssetMenu(fileName = "New SlimeData", menuName = "Data/SlimeData")]
public class SlimeData : ScriptableObject
{
    public Sprite SlimeSprite;
    public AnimatorController SlimeAnim;
    public int Index;
    public string WeaponName;
    public AnimatorController WeaponAnim;
    public AnimatorController BulletAnim;
    public int AttackPower;
    public float Speed;//移动速度
    public float HeartCount;//生命值
    public float ShieldCount;//护盾值
}
