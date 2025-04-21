using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoChangeColor : MonoBehaviour
{
    public int ColorIndex;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(spriteRenderer.color != ColorManager.Instance.UpdateColor(ColorIndex))
        {
            spriteRenderer.color = ColorManager.Instance.UpdateColor(ColorIndex);
        }
    }
}
