using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (spriteRenderer.color != ColorManager.Instance.UpdateColor(2))
        {
            spriteRenderer.color = ColorManager.Instance.UpdateColor(2);
        }
    }
}
