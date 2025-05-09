using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class DeleteHealth : MonoBehaviour
{
    private RectTransform rectTransform;
    private Image image;
    private float AlphaColor;//透明度
    public float AlphaChangeSpeed;//透明度改变速度
    public float DownSpeed;//下降速度
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }
    private void OnEnable()
    {
        AlphaColor = 1;
    }
    private void Update()
    {
        image.color = new Color(image.color.r,image.color.g,image.color.b, AlphaColor);
        ChangeAlpha();
        ChangePosition();
        if (AlphaColor <= 0.1f)
        {
            Destroy(gameObject);
        }
    }
    private void ChangeAlpha()
    {
        AlphaColor = math.lerp(AlphaColor, 0, AlphaChangeSpeed);
    }
    private void ChangePosition()
    {
        rectTransform.anchoredPosition -= new Vector2(0,1f) * DownSpeed * Time.deltaTime;
    }
}
