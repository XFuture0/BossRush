using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCanvs : MonoBehaviour
{
    public GameObject MiniMap;
    public GameObject AllMiniMap;
    public Camera MiniCamera;
    public RenderTexture MiniMapRender;
    private void Update()
    {
        if (KeyBoardManager.Instance.GetKeyDown_Tab())
        {
            if (MiniMap.activeSelf)
            {
                MiniMap.SetActive(false);
                AllMiniMap.SetActive(true);
                MiniCamera.fieldOfView = 168f;
            }
            else
            {
                MiniMap.SetActive(true);
                AllMiniMap.SetActive(false);
                MiniCamera.fieldOfView = 152f;
            }
        }
    }
}
