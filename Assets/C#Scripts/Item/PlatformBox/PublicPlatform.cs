using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicPlatform : MonoBehaviour
{
    public PolygonCollider2D polygonCollider;
    private void Update()
    {
        if (GameManager.Instance.PlayerStats.gameObject.transform.position.y >= transform.position.y)
        {
            polygonCollider.enabled = true;
        }
        else
        {
            polygonCollider.enabled = false;
        }
    }
}
