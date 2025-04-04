using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New BossSkillList", menuName = "List/BossSkillList")]
public class BossSkillList : ScriptableObject
{
    [System.Serializable]
    public class BossSkill
    {
        public string SkillName;
        public SkillType Type;
        public float SkillProbability;
        public bool IsOpen;
    }
    public List<BossSkill> BossSkills = new List<BossSkill>();
}
