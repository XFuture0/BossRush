using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniRoomCanvs : MonoBehaviour
{
    public Canvas canvs;
    public List<MiniRoomSlot> miniRoomSlots = new List<MiniRoomSlot>();
    public Transform MiniRoomPanel;
    private void Awake()
    {
        canvs = GetComponent<Canvas>();
        foreach (var camera in Camera.allCameras)
        {
            if(camera.gameObject.tag == "MiniCamera")
            {
                canvs.worldCamera = camera;
            }
        };
    }
    public void SetMiniRoom(GameObject MiniImage)
    {
        foreach (var slot in miniRoomSlots)
        {
            if(slot.SlotName == MiniImage.GetComponent<MiniRoomSlot>().SlotName)
            {
                return;
            }
        }
        miniRoomSlots.Add(MiniImage.GetComponent<MiniRoomSlot>());
        Instantiate(MiniImage,MiniRoomPanel);
    }
    public bool FindMiniSlot(string mininame)
    {
        foreach (var slot in miniRoomSlots)
        {
            if(slot.SlotName == mininame)
            {
                return true;
            }
        }
        return false;
    }
}
