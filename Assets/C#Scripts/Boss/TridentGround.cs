using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TridentGround : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("OpenTrident", 1.5f);
    }
    private void OpenTrident()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        CancelInvoke();
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
