using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private float RotationX;
    private float RotationY;
    private float ResRotation;
    private void Update()
    {
        if (!SceneChangeManager.Instance.EndCanvs.activeSelf)
        {
            ChangeRotation();
        }
    }
    private void ChangeRotation()
    {
        var changeRo = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.parent.position).normalized;
        RotationX = MathF.Asin(changeRo.x);
        RotationY = MathF.Asin(changeRo.y);
        RotationY *= 57.3f;
        if (RotationX < 0 && RotationY >= 0)
        {
            RotationY = MathF.Asin(-changeRo.y);
            RotationY *= 57.3f;
            RotationY += 180f;
        }
        if (RotationX < 0 && RotationY < 0)
        {
            RotationY = MathF.Asin(-changeRo.y);
            RotationY *= 57.3f;
            RotationY += 180f;
        }
        if (RotationX >= 0 && RotationY < 0)
        {
            RotationY += 360f;
        }
        if(transform.parent.localScale.x == -1)
        {
            RotationY += 180f;
        }
        transform.rotation = Quaternion.Euler(0, 0,RotationY);
    }
}
