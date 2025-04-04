using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New BossShootList", menuName = "List/BossShootList")]
public class BossShootList : ScriptableObject
{
    public List<Vector3> ShootList = new List<Vector3>();
}
