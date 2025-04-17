using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FissureBox : MonoBehaviour
{
    private void Update()
    {
        if(transform.GetChild(0).GetComponent<SpriteRenderer>().color != ColorManager.Instance.UpdateColor(2) || transform.GetChild(1).GetComponent<SpriteRenderer>().color != ColorManager.Instance.UpdateColor(2))
        {
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = ColorManager.Instance.UpdateColor(2);
            transform.GetChild(1).GetComponent<SpriteRenderer>().color = ColorManager.Instance.UpdateColor(2);
        }
    }
    private void OnEnable()
    {
        Invoke("AutoDisable", 1);
    }
    private void AutoDisable()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
}
