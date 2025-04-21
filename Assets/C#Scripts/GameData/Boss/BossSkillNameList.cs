using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New BossSkillNameList", menuName = "List/BossSkillNameList")]

public class BossSkillNameList : ScriptableObject
{
    [System.Serializable]
    public class BossSkillName
    {
        public string Name;
        [TextArea]
        public string Description_1;
        [TextArea]
        public string Description_2;
        [TextArea]
        public string Description_3;
        [TextArea]
        public string Description_4;
        [TextArea]
        public string Description_5;
    }

    public List<BossSkillName> BossSkillNames = new List<BossSkillName>();
}
