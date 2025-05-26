using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVS : MonoBehaviour
{
    public List<CharacterItem> PlayerChList = new List<CharacterItem>();
    public List<CharacterItem> BossChList = new List<CharacterItem>();
    private void OnEnable()
    {
        foreach  (var item in PlayerChList)
        {
            item.SetPlayerCharacter();
        }
        foreach (var item in BossChList)
        {
            item.SetBossCharacter();
        }
    }
}
