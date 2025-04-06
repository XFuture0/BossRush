using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealth : MonoBehaviour
{
    public Sprite NullHealth;
    private Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    public void ChangeImage()
    {
        image.sprite = NullHealth;
    }
}
