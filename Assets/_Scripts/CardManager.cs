using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CardType
{
    Heart,
    Diamond,
    Club,
    Spade
}
public enum CardValue
{
    Number,
    King,
    Quenn,
    Jack,
}
[System.Serializable]
public struct CardInfo
{
    [EnumToggleButtons] public CardType cardType;
    [EnumToggleButtons] public CardValue cardValue;
    public Sprite cardSprite;
    public int cardPoint;
}
public class CardManager : MonoBehaviour
{
    public List<CardData> cards = new List<CardData>();

    public GameObject cardPrefab;
    public Sprite cardBackSprite;
    void Awake()
    {
        CreateDeck();
    }

    private void CreateDeck()
    {

    }
}
