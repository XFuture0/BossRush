using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    private void Update()
    {
        if(GameManager.Instance.PlayerStats.gameObject.transform.position.y >= transform.position.y + 0.5f)
        {
            boxCollider.enabled = true;
        }
        else
        {
            boxCollider.enabled = false;
        }
    }
}
