using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeCanvs : MonoBehaviour
{
    private Animator anim;
    private bool IsFadeIn;
    public Transform FadeEffect;
    public GameObject BaseFadeImage;
    [Header("生成计时器")]
    public float SetTime;
    private float SetTime_Count;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (IsFadeIn)
        {
            if (SetTime_Count > -2)
            {
                SetTime_Count -= Time.deltaTime;
            }
            if (SetTime_Count <= 0)
            {
                SetTime_Count = SetTime;
                SetFadeEffect();
            }
        }
    }
    public void FadeIn()
    {
        anim.SetTrigger("FadeIn");
        FadeEffect.parent.gameObject.SetActive(true);
        IsFadeIn = true;
    }
    public void FadeOut()
    {
        IsFadeIn = false;
        ClearFadeImage();
        FadeEffect.parent.gameObject.SetActive(false);
        anim.SetTrigger("FadeOut");
    }
    public void SetFadeEffect()
    {
        var screenwidth = UnityEngine.Screen.width;
        var screenheight = UnityEngine.Screen.height;
        var SetWidth = UnityEngine.Random.Range(-screenheight * 0.5f, screenwidth * 0.5f);
        var NewFadeImage = Instantiate(BaseFadeImage, FadeEffect);
        NewFadeImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(SetWidth, screenheight * 0.5f);
    }
    private void ClearFadeImage()
    {
        for(int i = 0; i < FadeEffect.childCount; i++)
        {
            Destroy(FadeEffect.GetChild(i).gameObject);
        }
    }
}
