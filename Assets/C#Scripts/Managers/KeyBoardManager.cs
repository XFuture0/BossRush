using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardManager : SingleTons<KeyBoardManager>
{
    public bool StopAnyKey;
    public bool StopMoveKey;
    public bool GetKeyDown_Space()
    {
        if (!StopAnyKey && !StopMoveKey)
        {
            return Input.GetKeyDown(KeyCode.Space);
        }
        return false;
    }
    public float GetHorizontalRaw()
    {
        if (!StopAnyKey && !StopMoveKey)
        {
            return Input.GetAxisRaw("Horizontal");
        }
        return 0f;
    }
    public bool GetKeyDown_J()
    {
        if (!StopAnyKey && !StopMoveKey)
        {
            return Input.GetKeyDown(KeyCode.J);
        }
        return false;
    }
    public bool GetKeyDown_R()
    {
        if (!StopAnyKey && !StopMoveKey)
        {
            return Input.GetKeyDown(KeyCode.R);
        }
        return false;
    }
}

