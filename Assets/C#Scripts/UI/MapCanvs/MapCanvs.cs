using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCanvs : MonoBehaviour
{
    public GameObject MiniMap;
    public Camera MiniCamera;
    public GameObject BagCanvs;
    private void Update()
    {
        if (KeyBoardManager.Instance.GetKeyDown_Tab())
        {
            if (MiniMap.activeSelf)
            {
                MiniMap.SetActive(false);
                BagCanvs.SetActive(true);
                MiniCamera.fieldOfView = 177f;
            }
            else
            {
                MiniMap.SetActive(true);
                BagCanvs.SetActive(false);
                MiniCamera.fieldOfView = 152f;
            }
        }
    }
}
