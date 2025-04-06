using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New BossSkillNameList", menuName = "List/BossSkillNameList")]

public class BossSkillNameList : ScriptableObject
{
    public List<string> BossSkillName = new List<string>();
}
