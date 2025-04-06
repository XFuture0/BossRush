using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New CardList",menuName ="List/CardList")]
public class ChooseCardList : ScriptableObject
{
    [System.Serializable]
    public class Card
    {
        public string Description;
        public int Health;
        public int AttackPower;
        public BallType BallType;
    }
    public List<Card> CardLists = new List<Card>();
}
