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
    public GameObject NewWeapon;
    private int WeaponCount;
    private int RealWeaponCount;
    private void Update()
    {
        if (!SceneChangeManager.Instance.EndCanvs.activeSelf)
        {
            ChangeRotation();
        }
        if (WeaponCount != GameManager.Instance.Player().WeaponCount)
        {
            WeaponCount = GameManager.Instance.Player().WeaponCount;
            RefreshWeapon();
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
    private void RefreshWeapon()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        switch (WeaponCount)
        {
            case 0:
                transform.GetChild(0).gameObject.SetActive(true);
                break;
            case 1:
                transform.GetChild(1).gameObject.SetActive(true);
                break;
            case 2:
                transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 3:
                transform.GetChild(3).gameObject.SetActive(true);
                break;
            case 4:
                transform.GetChild(4).gameObject.SetActive(true);
                break;
            case 5:
                transform.GetChild(5).gameObject.SetActive(true);
                break;
        }
    }
}
