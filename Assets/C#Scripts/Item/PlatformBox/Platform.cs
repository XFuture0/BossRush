using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    private PlatformEffector2D platformEffector;
    private void Awake()
    {
        platformEffector = GetComponent<PlatformEffector2D>();
    }
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
        if (KeyBoardManager.Instance.GetKey_S())
        {
            platformEffector.colliderMask = (1 << 7) | (1 << 8);
        }
        else if (!KeyBoardManager.Instance.GetKey_S())
        {
            platformEffector.colliderMask = (1 << 3) | (1 << 7) | (1 << 8);
        }
    }
}
