using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Weapon List", menuName = "List/Weapon List")]
public class WeaponList : ScriptableObject
{
    [System.Serializable]
    public class WeaponData
    {
        public string WeaponName;
        public Sprite WeaponSprite;
        public Sprite BulletSprite;
        
        public int AttackPower;
        [TextArea]
        public string Description;
    }
    public List<WeaponData> WeaponDatas = new List<WeaponData>();
}
