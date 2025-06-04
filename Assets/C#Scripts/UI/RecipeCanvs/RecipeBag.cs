using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeBag : MonoBehaviour
{
    public FruitData.Fruit ThisFruit;
    public Image RecipeImage;
    public Text Count;
    public void RefreshData(FruitData.Fruit fruit)
    {
        ThisFruit = fruit;
        RecipeImage.sprite = ThisFruit.sprite;
    }
    private void Update()
    {
        Count.text = ThisFruit.Count.ToString();
    }
}
