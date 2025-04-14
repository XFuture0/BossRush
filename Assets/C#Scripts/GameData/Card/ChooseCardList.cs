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
        [TextArea]
        public string Description;
        public BallType BallType;
        public Quality Quality;
        public VoidEventSO CardEvent;

    }
    public List<Card> CardLists = new List<Card>();
}
