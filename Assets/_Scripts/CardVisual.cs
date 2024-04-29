using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CardVisual : MonoBehaviour
{
    [SerializeField] private TextMeshPro cardNumber;
    [SerializeField] private SpriteRenderer cardSprite;

    private CardData cardData;
    public CardData CardData => cardData == null ? GetComponent<Card>().cardData : cardData;

    public void SetVisual()
    {
        cardSprite.sprite = CardData.cardInfo.cardSprite;
        transform.localScale = GameManager.Instance.gamePrefences.visualPrefences.cardsScale * Vector3.one;
        if (CardData.cardInfo.cardValue == CardValue.Number)
        {
            cardNumber.SetText(CardData.cardInfo.cardNumber.ToString());
            return;
        }
        else
            cardNumber.gameObject.SetActive(false);
    }

    public void ShowCard(bool show = true)
    {
        if (show)
        {
            cardSprite.sprite = CardData.cardInfo.cardSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sortingOrder = 99;
            cardSprite.sprite = CardData.cardInfo.cardBackSprite;
        }
    }
}
