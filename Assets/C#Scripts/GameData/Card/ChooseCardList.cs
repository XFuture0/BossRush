using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New CardList",menuName ="List/CardList")]
public class ChooseCardList : ScriptableObject
{
    [System.Serializable]
    public class Card
    {
        public string CardName;
        public string CardInvokeName;
        [TextArea]
        public string Description;
        public Quality Quality;
        public bool IsOpen;
    }
    public List<Card> CardLists = new List<Card>();
}
