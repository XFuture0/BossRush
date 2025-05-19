using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    [Header("ÊÂ¼þ¼àÌý")]
    public Vector3EventSO ChangeMiniMapPositionEvent;
    private void OnEnable()
    {
        ChangeMiniMapPositionEvent.OnVector3EventRaised += ChangeMiniMapPosition;
    }
    public void ChangeMiniMapPosition(Vector3 MapPosition)
    {
        transform.position = new Vector3(MapPosition.x - 15,MapPosition.y + 8,transform.position.z);
    }
    private void OnDisable()
    {
        ChangeMiniMapPositionEvent.OnVector3EventRaised -= ChangeMiniMapPosition;
    }
}
