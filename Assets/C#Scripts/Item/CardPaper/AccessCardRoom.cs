using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessCardRoom : MonoBehaviour
{
    public bool IsPlayer;
    private void Update()
    {
        if(IsPlayer && KeyBoardManager.Instance.GetKeyDown_R())
        {
            IsPlayer = false;
            MapManager.Instance.AccessRoom(Physics2D.OverlapPoint(SceneChangeManager.Instance.Player.transform.position,SceneChangeManager.Instance.Room).gameObject.transform.position);
            DataManager.Instance.Save(DataManager.Instance.Index);//´æµµ
            SceneChangeManager.Instance.OpenDoorEvent.RaiseEvent();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            IsPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            IsPlayer = false;
        }
    }
}
