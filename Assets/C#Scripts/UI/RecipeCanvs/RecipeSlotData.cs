using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeSlotData : MonoBehaviour
{
    public FruitData.Fruit ThisFruit;
    public Image image;
    private void Update()
    {
        if(image.sprite == null)
        {
            image.color = Color.clear;
        }
        else
        {
            image.color = Color.white;
        }
    }
}
