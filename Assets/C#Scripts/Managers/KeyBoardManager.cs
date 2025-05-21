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
    public float GetHorizontalRaw_MiNi()
    {
        if (!StopAnyKey)
        {
            return Input.GetAxisRaw("Horizontal");
        }
        return 0f;
    }
    public float GetVerticalRaw_MiNi()
    {
        if (!StopAnyKey)
        {
            return Input.GetAxisRaw("Vertical");
        }
        return 0f;
    }
    public bool GetKeyDown_R()
    {
        if (!StopAnyKey && !StopMoveKey)
        {
            return Input.GetKeyDown(KeyCode.R);
        }
        return false;
    }
    public bool GetKey_Mouse0()
    {
        if (!StopAnyKey)
        {
            return Input.GetKey(KeyCode.Mouse0);
        }
        return false;
    }
    public bool GetKeyDown_Shift()
    {
        if (!StopAnyKey && !StopMoveKey)
        {
            return Input.GetKeyDown(KeyCode.LeftShift);
        }
        return false;
    }
    public bool GetKey_Shift()
    {
        if (!StopAnyKey && !StopMoveKey)
        {
            return Input.GetKey(KeyCode.LeftShift);
        }
        return false;
    }
    public bool GetKeyDown_F()
    {
        if (!StopAnyKey && !StopMoveKey)
        {
            return Input.GetKeyDown(KeyCode.F);
        }
        return false;
    }
    public bool GetKey_F()
    {
        if (!StopAnyKey && !StopMoveKey)
        {
            return Input.GetKey(KeyCode.F);
        }
        return false;
    }
    public bool GetKeyDown_Tab()
    {
        if (!StopAnyKey)
        {
            return Input.GetKeyDown(KeyCode.Tab);
        }
        return false;
    }
}

