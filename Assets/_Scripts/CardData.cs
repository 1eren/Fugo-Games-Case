using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Pisti/Card Data")]
public class CardData : ScriptableObject
{
    public CardInfo cardInfo;

    private CardValue GetCardValue()
    {
        return cardInfo.cardValue;
    }

}
