using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerDashTemp : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color DashColor;
    public float BaseAlpha;
    public float ChangeSpeed;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        DashColor.a = BaseAlpha;
    }
    private void Update()
    {
        if(DashColor.r != ColorManager.Instance.UpdateColor(2).r || DashColor.g != ColorManager.Instance.UpdateColor(2).g || DashColor.b != ColorManager.Instance.UpdateColor(2).b)
        {
            DashColor = new Color(ColorManager.Instance.UpdateColor(2).r, ColorManager.Instance.UpdateColor(2).g, ColorManager.Instance.UpdateColor(2).b,DashColor.a);
        }
        UpdateAlpha();
        if(DashColor.a <= 0.01)
        {
            GameManager.Instance.PlayerStats.gameObject.GetComponent<PlayerController>().PlayerDashPool.ReturnObject(this, GameManager.Instance.PlayerStats.gameObject.GetComponent<PlayerController>().DashPool);
        }
    }
    private void UpdateAlpha()
    {
        DashColor.a = math.lerp(DashColor.a, 0, ChangeSpeed);
        spriteRenderer.color = DashColor;
    }
}
