using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New FruitList", menuName = "List/FruitList")]
public class FruitData : ScriptableObject
{
    [System.Serializable]
    public class Fruit
    {
        public Sprite sprite;
        public string name;
        public int Count;
        public int Index;
    }
    public List<Fruit> Fruits = new List<Fruit>();
}
