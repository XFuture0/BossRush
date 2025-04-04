using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [HideInInspector] public bool IsProtected;
    public CharacterData CharacterData;
    public CharacterData CharacterData_Temp;
    private void Awake()
    {
        if (CharacterData != null)
        {
            CharacterData_Temp = Instantiate(CharacterData);
        }
    }
}
