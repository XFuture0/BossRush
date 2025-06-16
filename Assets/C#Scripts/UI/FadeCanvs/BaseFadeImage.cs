using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFadeImage : MonoBehaviour
{
    private RectTransform rectTransform;
    private float DownSpeed;
    private float RoSpeed;
    private void OnEnable()
    {
        var BaseLarge = UnityEngine.Random.Range(2f, 3f);
        transform.localScale = new Vector3(BaseLarge, BaseLarge, 1);
        DownSpeed = (3 - BaseLarge) + 0.5f;
        RoSpeed =0.75f * (3 - BaseLarge) + 0.25f;
    }
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        rectTransform.anchoredPosition += new Vector2(0, -DownSpeed);
        transform.localEulerAngles += new Vector3(0, 0, RoSpeed);
        if(rectTransform.anchoredPosition.y < UnityEngine.Screen.height * -0.5f - 100)
        {
            Destroy(gameObject);
        }
    }
}
