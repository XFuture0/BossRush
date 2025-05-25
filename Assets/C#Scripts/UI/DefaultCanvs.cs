using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultCanvs : MonoBehaviour
{
    public GameObject StopButton;
    public GameObject StopCanvs;
    private void Awake()
    {
        StopButton.GetComponent<Button>().onClick.AddListener(OnStopButton);
    }
    private void Update()
    {
        if(StopButton.GetComponent<Image>().color != ColorManager.Instance.UpdateColor(2))
        {
            StopButton.GetComponent<Image>().color = ColorManager.Instance.UpdateColor(2);
        }
    }
    private void OnStopButton()
    {
        StopCanvs.SetActive(true);
    }
}
