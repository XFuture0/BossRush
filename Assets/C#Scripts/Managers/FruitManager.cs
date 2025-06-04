using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitManager : SingleTons<FruitManager>
{
    public FruitData Fruitdata;
    public JuiceData Juicedata;
    public void GetFruit(Sprite FruitSprite,string FruitName,int Index)
    {
        foreach(var Fruit in Fruitdata.Fruits)
        {
            if(Fruit.name == FruitName)
            {
                Fruit.Count++;
                return;
            }
        }
        FruitData.Fruit NewFruit = new FruitData.Fruit();
        NewFruit.sprite = FruitSprite;
        NewFruit.name = FruitName;
        NewFruit.Count = 1;
        NewFruit.Index = Index;
        Fruitdata.Fruits.Add(NewFruit);
        DataManager.Instance.Save(DataManager.Instance.Index);
    }
    public void ClearFruit()
    {
        Fruitdata.Fruits.Clear();
    }
}
