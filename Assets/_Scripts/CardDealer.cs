using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CardDealer : Singleton<CardDealer>
{
    [InfoBox("This list contains a list of manually created scriptable objects.")]
    public List<CardData> cardDatas = new List<CardData>();

    [HideInInspector] public UnityEvent OnCardsDealed = new UnityEvent();
    public GameObject cardPrefab;

    [SerializeField] public GameObject mainPlayer;
    [SerializeField] public GameObject computerPlayer;
    [SerializeField] public GameObject groundCardsParent;
    [SerializeField] public GameObject allDeckParent;

    [SerializeField][BoxGroup("Visual Settings")] private float cardDealDelay = 0.1f;
    [ReadOnly] public List<GameObject> allDeck = new List<GameObject>();

    private GameManager gameManager;

    private bool isDealedToGround;

    public bool playersCanPlay = false;

    private PlayerBase[] players;
    void Awake()
    {
        gameManager = GameManager.Instance;
        CreateDeck();
        players = FindObjectsOfType<PlayerBase>();
        allDeck.Shuffle();

        allDeck[allDeck.Count - 1].SetActive(true);

        //Show card back 
        allDeck[allDeck.Count - 1].GetComponent<CardVisual>().ShowCard(false);

        SetCardPositions();
    }

    private void OnEnable()
    {
        GameManager.Instance?.OnGameStart.AddListener(DealCardsToPlayers);
        CardMoveController.OnCardPlayed.AddListener((x, y,z) => CheckDeckCount());
    }
    private void OnDisable()
    {
        GameManager.Instance?.OnGameStart.RemoveListener(DealCardsToPlayers);
        CardMoveController.OnCardPlayed.AddListener((x, y,z) => CheckDeckCount());
    }

    public void DealCardsToPlayers()
    {
        playersCanPlay = false;
        int dealedIndex = 0;
        for (int i = 0; i < 8; i++)
        {
            GameObject dealedCard = allDeck[0];
            GameObject dealedPlayer = i % 2 == 0 ? mainPlayer : computerPlayer;

            allDeck.RemoveAt(0);

            dealedCard.SetActive(true);
            dealedCard.GetComponent<CardVisual>().ShowCard(false);

         
            if (dealedCard.TryGetComponent(out Card card))
            {
                dealedPlayer.GetComponent<PlayerBase>().Deck.Add(card);
                CardOwner cardOwner = dealedPlayer == mainPlayer ? CardOwner.Player : CardOwner.Computer;

                DOVirtual.DelayedCall(i * cardDealDelay, () => card.MoveCard(cardOwner)).OnComplete(() =>
                {
                    //Show Player Cars
                    if (dealedPlayer == mainPlayer)
                    {
                        dealedCard.GetComponent<CardVisual>().ShowCard(true);
                    }
                    dealedIndex++;
                    //check all Cards dealed
                    if (dealedIndex == 7)
                    {
                        playersCanPlay = true;
                        OnCardsDealed.Invoke();
                    }
                });
                card.AssingOwner(cardOwner);

            }

        }
        if (!isDealedToGround)
            DealCardsToGround();
    }
    public void DealCardsToGround()
    {
        isDealedToGround = true;
        int dealedCount = 0;
        for (int i = 0; i < 4; i++)
        {
            GameObject dealedCard = allDeck[0];
            allDeck.RemoveAt(0);

            dealedCard.GetComponent<CardVisual>().ShowCard(false);

            dealedCard.SetActive(true);

            if (dealedCard.TryGetComponent(out Card card))
            {
                DOVirtual.DelayedCall(i * cardDealDelay, () => card.MoveCard(CardOwner.Ground)).OnComplete(() =>
                {
                    dealedCount++;
                    //show if the last ground card
                    if (dealedCount == 4)
                        dealedCard.GetComponent<CardVisual>().ShowCard(true);

                });
                GameMove gameMove = new GameMove();
                gameMove.card = card;
                gameMove.owner = CardOwner.None;
                ScoreManager.Instance.allMoves.Add(gameMove);
            }
        }
    }
    private void CheckDeckCount()
    {
        if (allDeck.Count == 0 && players.Where(x => x.Deck.Count == 0).Count() == players.Count())
        {
            DOVirtual.DelayedCall(1,()=> GameManager.Instance.EndGame());
            return;
        }
        if (players.Where(x => x.Deck.Count == 0).Count() == players.Count())
        {
            DOVirtual.DelayedCall(0.5f, () => DealCardsToPlayers());
        }
      
    }
    //Set Card Positions According To Screen Size and Corners
    private void SetCardPositions()
    {
        allDeckParent.transform.position = ScreenPositionUtility.CenterLeft(gameManager.gamePrefences.visualPrefences.allDeckScreenPadding, 0f);
        mainPlayer.transform.position = ScreenPositionUtility.BottomCenter(0f, gameManager.gamePrefences.visualPrefences.playerCardsScreenPadding);
        computerPlayer.transform.position = ScreenPositionUtility.TopCenter(0f, gameManager.gamePrefences.visualPrefences.computerCardsScreenPadding);
        groundCardsParent.transform.position = ScreenPositionUtility.Center();
    }
    private void CreateDeck()
    {
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
    //Create Number Cards variations fron 1 to 10
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
