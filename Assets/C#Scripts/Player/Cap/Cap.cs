using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cap : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (spriteRenderer.sprite != GameManager.Instance.PlayerData.HatData.HatImage)
        {
            spriteRenderer.sprite = GameManager.Instance.PlayerData.HatData.HatImage;
        }
    }
}
