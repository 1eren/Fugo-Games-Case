using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CardManager : Singleton<CardManager>
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

    private bool isDealedToGround;

    [HideInInspector] public UnityEvent<Card> OnCardPlayed = new UnityEvent<Card>(); 
    void Awake()
    {
        gameManager = GameManager.Instance;
        CreateDeck();

        allDeck.Shuffle();
        SetCardPositions();
    }

    private void OnEnable()
    {
        GameManager.Instance?.OnGameStart.AddListener(DealCardsToPlayers);
        OnCardPlayed.AddListener(MoveCardToGround);
    }
    private void OnDisable()
    {
        GameManager.Instance?.OnGameStart.RemoveListener(DealCardsToPlayers);
        OnCardPlayed.AddListener(MoveCardToGround);
    }
    public void DealCardsToPlayers()
    {
        for (int i = 0; i < 8; i++)
        {
            GameObject dealedCard = allDeck[dealedCardIndex];
            GameObject dealedPlayer = i % 2 == 0 ? mainPlayer : computerPlayer;
            dealedCard.SetActive(true);
            if (dealedPlayer != mainPlayer)
            {
                dealedCard.GetComponent<CardVisual>().ShowCard(false);
            }
            dealedCardIndex++;
            dealedCard.transform.DOMove(dealedPlayer.transform.position, 0.1f).SetDelay(cardDealDelay * i).
                OnComplete(() => dealedCard.transform.parent = dealedPlayer.transform);
        }
        if (!isDealedToGround)
            DealCardsToGround();
    }
    public void DealCardsToGround()
    {
        isDealedToGround = true;
        for (int i = 0; i < 4; i++)
        {
            GameObject dealedCard = allDeck[dealedCardIndex];
            if (i == 3)
                dealedCard.GetComponent<CardVisual>().ShowCard(true);
            else
                dealedCard.GetComponent<CardVisual>().ShowCard(false);

            dealedCard.SetActive(true);
            dealedCardIndex++;
            DOVirtual.DelayedCall(i * cardDealDelay, () => MoveCardToGround(dealedCard.GetComponent<Card>()));
        }
    }
    private void MoveCardToGround(Card card)
    {
        card.transform.DOMove(groundCardsParent.GetComponent<GroundCardsLayout2D>().GetRandomLayoutPos(),0.1f).
             OnComplete(() => card.transform.parent = groundCardsParent.transform);
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
