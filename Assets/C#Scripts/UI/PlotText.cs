using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlotText : MonoBehaviour
{
    private RectTransform rectTransform;
    private float rectTransform_Y;
    private float FinalrectTransform_Y;
    public float Speed;
    public float FinalDistance;
    private bool IsMoving;
    private int UpIndex;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        UpIndex = 0;
    }
    private void Update()
    {
        if (IsMoving)
        {
            OnMove();
        }
        else if(IsMoving && math.abs(FinalrectTransform_Y - rectTransform.anchoredPosition.x) <= 0.1f)
        {
            IsMoving = false;
        }
        if(UpIndex == 4)
        {
            Destroy(gameObject);
        }
    }
    private void OnMove()
    {
        rectTransform_Y = math.lerp(rectTransform.anchoredPosition.y,FinalrectTransform_Y,Speed);
        rectTransform.anchoredPosition = new float2(rectTransform.anchoredPosition.x, rectTransform_Y);
    }
    public void UpPosition()
    {
        IsMoving = true;
        FinalrectTransform_Y = rectTransform.anchoredPosition.y + FinalDistance;
        UpIndex++;
    }
}
