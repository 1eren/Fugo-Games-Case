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
    public Sprite cardBackSprite;

    [HideInInspector] public int cardNumber;
}

[CreateAssetMenu(menuName = "Pisti/Card Data")]
public class CardData : ScriptableObject
{
    public CardInfo cardInfo;

    private CardValue GetCardValue()
    {
        return cardInfo.cardValue;
    }

}
