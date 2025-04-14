using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    public Image ItemImage;
    public int Index;
    private void Awake()
    {
        ItemImage = GetComponent<Image>();
    }
}
