using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeart : MonoBehaviour
{
    private Image image;
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
    }
}
