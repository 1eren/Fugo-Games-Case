using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;


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
    public int cardPoint;

    [HideInInspector] public int cardNumber;
}
public class CardManager : MonoBehaviour
{
    [InfoBox("This list contains a list of manually created scriptable objects.")]
    public List<CardData> cardDatas = new List<CardData>();

    public GameObject cardPrefab;


    [SerializeField] private GameObject mainPlayer;
    [SerializeField] private GameObject computerPlayer;
    [SerializeField] private GameObject groundCardsParent;
    [SerializeField] private GameObject allDeckParent;

    [SerializeField][BoxGroup("Visual Settings")] private float cardDealDelay = 0.1f;
    [ReadOnly] public List<GameObject> allDeck = new List<GameObject>();

    private GameManager gameManager;

    private int dealedCardIndex = 0;
    void Awake()
    {
        gameManager = GameManager.Instance;
        CreateDeck();

        allDeck.Shuffle();
        SetCardPositions();
    }

    private void OnEnable()
    {
        GameManager.Instance?.OnGameStart.AddListener(DealCards);
    }
    private void OnDisable()
    {
        GameManager.Instance?.OnGameStart.RemoveListener(DealCards);
    }
    public void DealCards()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject dealedCard = allDeck[i];
            GameObject dealedPlayer = i % 2 == 0 ? mainPlayer : computerPlayer;
            dealedCard.SetActive(true);

            dealedCard.transform.DOMove(dealedPlayer.transform.position, 0.1f).SetDelay(cardDealDelay * i).
                OnComplete(() => dealedCard.transform.parent = dealedPlayer.transform);


        }
    }
    private void SetCardPositions()
    {
        allDeckParent.transform.position = ScreenPositionUtility.CenterLeft(gameManager.gamePrefences.visualPrefences.allDeckScreenPadding, 0f);
        mainPlayer.transform.position = ScreenPositionUtility.BottomCenter(0f, gameManager.gamePrefences.visualPrefences.playerCardsScreenPadding);
        computerPlayer.transform.position = ScreenPositionUtility.TopCenter(0f, gameManager.gamePrefences.visualPrefences.computerCardsScreenPadding);
        groundCardsParent.transform.position = ScreenPositionUtility.Center();
    }
    private void CreateDeck()
    {
        //Create a dummy card for allDeckVisual;
        GameObject allDeckVisual = InstantiateCard(cardDatas[0], allDeckParent.transform);
        allDeckVisual.GetComponent<CardVisual>().ShowCard(false);
        allDeckVisual.SetActive(true);
        foreach (var item in cardDatas)
        {
            if (item.cardInfo.cardValue == CardValue.Number)
            {
                CreateNumberCard(item);
                continue;
            }
            allDeck.Add(InstantiateCard(item, allDeckParent.transform));
        }
    }
    private void CreateNumberCard(CardData card)
    {
        for (int i = 1; i < 11; i++)
        {
            CardData newCard = new CardData();
            newCard.cardInfo = card.cardInfo;
            newCard.cardInfo.cardNumber = i;

            allDeck.Add(InstantiateCard(newCard, allDeckParent.transform));

        }
    }

    private GameObject InstantiateCard(CardData cardData, Transform parent)
    {
        GameObject card = Instantiate(cardPrefab, parent);
        card.GetComponent<Card>().cardData = cardData;
        card.GetComponent<CardVisual>().SetVisual();
        card.transform.localPosition = Vector3.zero;
        card.gameObject.SetActive(false);
        return card;
    }
}
