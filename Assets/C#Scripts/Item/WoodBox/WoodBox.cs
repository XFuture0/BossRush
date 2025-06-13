using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBox : MonoBehaviour
{
    private CharacterStats characterStats;
    private bool IsDestory;
    private void Awake()
    {
        characterStats = GetComponent<CharacterStats>();
    }
    private void Update()
    {
        if(characterStats.CharacterData_Temp.NowHealth  <= 0 && !IsDestory)
        {
            IsDestory  = true;
            Destorying();
        }
    }
    private void Destorying()
    {
        Destroy(gameObject);
    }
}
