using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New FruitMenuList", menuName = "List/FruitMenuList")]
public class FruitMenuList : ScriptableObject
{
    [System.Serializable]
    public class Menu
    {
        public string JuiceIndex;
        public JuiceData.Juice Result;
    }
    public List<Menu> MenuList = new List<Menu>();
}
