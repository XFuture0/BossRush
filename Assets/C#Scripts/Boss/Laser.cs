using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private Quaternion NormalQuar;
    private void OnEnable()
    {
        NormalQuar = transform.localRotation;
        InvokeRepeating("ChangeRoX", 0, 0.02f);
    }
    private void ChangeRoX()
    {
        var RotationX = transform.rotation.eulerAngles.z;
        RotationX += 1;
        transform.rotation = Quaternion.Euler(0, 0, RotationX);
    }
    private void OnDisable()
    {
        CancelInvoke();
        transform.localRotation = NormalQuar;
    }
}
