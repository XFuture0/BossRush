using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class GetScore : MonoBehaviour
{
    private RectTransform rectTransform;
    private Text GetScoretext;
    private float AlphaColor;//透明度
    public float AlphaChangeSpeed;//透明度改变速度
    public float DownSpeed;//下降速度
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        GetScoretext = GetComponentInChildren<Text>();
    }
    private void OnEnable()
    {
        AlphaColor = 1;
    }
    private void Update()
    {
        GetScoretext.color = new Color(ColorManager.Instance.UpdateColor(2).r, ColorManager.Instance.UpdateColor(2).g, ColorManager.Instance.UpdateColor(2).b, AlphaColor);
        ChangeAlpha();
        ChangePosition();
        if (AlphaColor <= 0.3f)
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
        rectTransform.anchoredPosition -= new Vector2(0, 1f) * DownSpeed * Time.deltaTime;
    }
}
