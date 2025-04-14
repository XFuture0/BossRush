using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Character List", menuName = "List/Character List")]

public class CharacterList : ScriptableObject
{
    [System.Serializable]
    public class CharacterData
    {
        public string CharacterName;
        public float BaseHealth;
        public float BaseAttackPower;
        public float BaseSpeed;
    }
    public List<CharacterData> CharacterDatas = new List<CharacterData>();

}
