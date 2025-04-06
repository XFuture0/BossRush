using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [HideInInspector] public bool IsProtected;
    public CharacterData CharacterData;
    public CharacterData CharacterData_Temp;
    public bool Invincible;
    public float InvincibleTime_Count = 0;
    private void Awake()
    {
        if (CharacterData != null)
        {
            CharacterData_Temp = Instantiate(CharacterData);
        }
    }
    private void Update()
    {
        if (InvincibleTime_Count > -2)
        {
            InvincibleTime_Count -= Time.deltaTime;
        }
        if(InvincibleTime_Count < 0)
        {
            Invincible = false;
        }
    }
}
