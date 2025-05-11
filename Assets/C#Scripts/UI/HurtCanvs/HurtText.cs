using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class HurtText : MonoBehaviour
{
    private Text Hurttext;
    public int HurtCount;
    private float HurtTextXY;
    public float HurtTextSpeed;//缩放速度
    private Vector3 BaseTextScale;//基础缩放倍率
    private Vector3 ExtraTextScale;//额外缩放倍率
    private float AlphaColor;//透明度
    public float AlphaSpeed;//透明度变换速度
    [Header("存在计时器")]
    private float OpenTime_Count;
    private void Awake()
    {
        Hurttext = GetComponent<Text>();
    }
    private void Update()
    {
        Hurttext.transform.localScale = BaseTextScale + ExtraTextScale;
        Hurttext.text = HurtCount.ToString() + " Hit";
        Hurttext.color = new Color(ColorManager.Instance.UpdateColor(2).r, ColorManager.Instance.UpdateColor(2).g, ColorManager.Instance.UpdateColor(2).b, AlphaColor);
        if (HurtTextXY > 0)
        {
            HurtTextXY = math.lerp(HurtTextXY, 0,HurtTextSpeed);
            ExtraTextScale = new Vector3(HurtTextXY, HurtTextXY, 0);
        }
        else if(HurtTextXY <= 0)
        {
            ExtraTextScale = new Vector3(0, 0, 0);
        }
        CheckOpenTime();
    }
    public void SetHurtText()
    {
        HurtCount++;
        CheckHitType();
        HurtTextXY = 0.5f;
        AlphaColor = 1;
        Hurttext.color = new Color(Hurttext.color.r, Hurttext.color.g, Hurttext.color.b, 1);
        OpenTime_Count = 3;
    }
    private void CheckHitType()
    {
        if(HurtCount < 5)
        {
            BaseTextScale = new Vector3(0.5f, 0.5f, 1);
        }
        if(HurtCount >= 5 && HurtCount < 10)
        {
            BaseTextScale = new Vector3(0.8f, 0.8f, 1);
        }
        if(HurtCount >= 10 && HurtCount < 20)
        {
            BaseTextScale = new Vector3(1.1f, 1.1f, 11);
        }
        if(HurtCount >= 20)
        {
            BaseTextScale = new Vector3(1.4f, 1.4f, 1);
        }
    }
    private void CheckOpenTime()
    {
        if (OpenTime_Count > -2)
        {
            OpenTime_Count -= Time.deltaTime;
        }
        if (OpenTime_Count <= 1 && OpenTime_Count > 0)
        {
            DowmAlphaColor();
        }
        if (OpenTime_Count < 0)
        {
            HurtCount = 0;
            Hurttext.color = new Color(Hurttext.color.r, Hurttext.color.g, Hurttext.color.b, 0);
        }
    }
    private void DowmAlphaColor()
    {
        AlphaColor = math.lerp(AlphaColor, 0, AlphaSpeed);
        Hurttext.color = new Color(Hurttext.color.r, Hurttext.color.g, Hurttext.color.b, AlphaColor);
    }
    private void OnDisable()
    {
        OpenTime_Count = -2;
    }
}
