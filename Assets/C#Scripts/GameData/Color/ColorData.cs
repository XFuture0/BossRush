using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New ColorList", menuName = "List/ColorList")]

public class ColorData : ScriptableObject
{
    [System.Serializable]
    public class Colordata
    {
        public Color Color1;
        public Color Color2;
    }
    public List<Colordata> ColorLists = new List<Colordata>();
}
