using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackColor : MonoBehaviour
{
    private Tilemap tilemap;
    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }
    private void Update()
    {
        if(tilemap.color != ColorManager.Instance.UpdateColor(2))
        {
            tilemap.color = ColorManager.Instance.UpdateColor(2);
        }
    }
}
