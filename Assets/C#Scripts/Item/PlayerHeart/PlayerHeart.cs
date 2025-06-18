using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeart : MonoBehaviour
{
    private Image image;
    public HeartType heartType;
    private int Index;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    private void Update()
    {
        if(image.color != ColorManager.Instance.UpdateColor(2))
        {
            image.color = ColorManager.Instance.UpdateColor(2);
        }
        UpdatePosition();
    }
    private void UpdatePosition()
    {
        Index = transform.GetSiblingIndex();//»ñµÃË÷Òý
        if(Index != transform.parent.childCount - 1)
        {
            if (transform.parent.GetChild(Index + 1).GetComponent<PlayerHeart>().heartType == HeartType.Heart)
            {
                transform.parent.GetChild(Index + 1).SetSiblingIndex(Index);
                transform.SetSiblingIndex(Index + 1);
            }
        }
    }
}
