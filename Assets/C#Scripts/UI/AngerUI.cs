using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AngerUI : MonoBehaviour
{
    public Image AngerLine;
    public Image AngerBack;
    private void Update()
    {
        if(AngerLine.color != ColorManager.Instance.UpdateColor(1))
        {
            AngerLine.color = ColorManager.Instance.UpdateColor(1);
        }
        if(AngerBack.color != ColorManager.Instance.UpdateColor(1))
        {
            AngerBack.color = ColorManager.Instance.UpdateColor(1);
        }
        UpdateAngerValue();
        if (GameManager.Instance.Player().ThreeMinuteHeat)
        {
            AngerBack.gameObject.transform.localScale = new Vector3(0.5f, 1, 1);
            if (GameManager.Instance.Player().AngerValue >= GameManager.Instance.Player().FullAnger)
            {
                AngerLine.gameObject.transform.localScale = new Vector3(GameManager.Instance.Player().FullAnger, 1, 1);
            }
        }
        else
        {
            AngerBack.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        if (GameManager.Instance.Player().FuriousGatling)
        {
            gameObject.SetActive(false);
        }
    }
    private void UpdateAngerValue()
    {
        if(AngerLine.gameObject.transform.localScale.x != GameManager.Instance.Player().AngerValue)
        {
            AngerLine.gameObject.transform.localScale = new Vector3(GameManager.Instance.Player().AngerValue, 1, 1);
            if(GameManager.Instance.Player().AngerValue >= GameManager.Instance.Player().FullAnger)
            {
                AngerLine.gameObject.transform.localScale = new Vector3(GameManager.Instance.Player().FullAnger, 1, 1);
            }
        }
    }
}
