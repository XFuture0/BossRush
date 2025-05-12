using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBox : MonoBehaviour
{
    [Header("ÊÂ¼þ¼àÌý")]
    public VoidEventSO ClosePlatformBoxEvent;
    private void OnEnable()
    {
        ClosePlatformBoxEvent.OnEventRaised += OnClosePlatform;
    }
    private void OnClosePlatform()
    {
        for(int i = 0;i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        StartCoroutine(ReOpen());
    }
    private IEnumerator ReOpen()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }
    private void OnDisable()
    {
        ClosePlatformBoxEvent.OnEventRaised -= OnClosePlatform;
    }
}
