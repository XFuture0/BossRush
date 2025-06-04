using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
[CreateAssetMenu(fileName = "New JuiceList", menuName = "List/JuiceList")]
public class JuiceData : ScriptableObject
{
    [System.Serializable]
    public class Juice
    {
        public Sprite juicesprite;
        public string Name;
    }
    public List<Juice> JuiceList = new List<Juice>();
}
