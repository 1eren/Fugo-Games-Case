using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;


[System.Serializable]
public struct GameMove
{
    public Card card;
    public CardOwner owner;
}
public class ScoreManager : Singleton<ScoreManager>
{
    public List<GameMove> allMoves = new List<GameMove>();
    [HideInInspector] public UnityEvent<PlayerBase> OnMatched = new UnityEvent<PlayerBase>();
    public RuleData ruleData;

    private List<PlayerBase> playerList = new List<PlayerBase>();
    private void OnEnable()
    {
        playerList = FindObjectsOfType<PlayerBase>().ToList();
        CardMoveController.OnCardPlayed.AddListener(AddGameMoveToList);
    }
    private void OnDisable()
    {
        CardMoveController.OnCardPlayed.RemoveListener(AddGameMoveToList);
    }
    private void AddGameMoveToList(Card card, CardOwner owner, PlayerBase player)
    {
        GameMove gameMove = new GameMove();
        gameMove.card = card;
        gameMove.owner = owner;
        allMoves.Add(gameMove);
        CheckMatch(player);
    }

    private void CheckMatch(PlayerBase player)
    {
        if (allMoves.Count < 2)
            return;
        if (allMoves.Last().card.cardData.cardInfo.cardValue == CardValue.Number)
        {
            if (allMoves.Last().card.cardData.cardInfo.cardNumber == allMoves[allMoves.Count - 2].card.cardData.cardInfo.cardNumber)
            {
                ClearTable(player);
            }
        }
        else if (allMoves.Last().card.cardData.cardInfo.cardValue != CardValue.Jack)
        {
            if (allMoves.Last().card.cardData.cardInfo.cardValue == allMoves[allMoves.Count - 2].card.cardData.cardInfo.cardValue)
            {
                ClearTable(player);
            }
        }
        else
        {
            ClearTable(player);
        }

    }
    public void ClearTable(PlayerBase player)
    {
        OnMatched.Invoke(player);
        foreach (var item in allMoves)
        {
            //Disappear Effect
            item.card.transform.DOScale(Vector3.zero, 0.2f).SetDelay(0.2f).OnComplete(() =>
            {
                item.card.gameObject.SetActive(false);
            });
            player.WonCards.Add(item.card);
        }
        CalculatePlayerScores(player);
        allMoves.Clear();
    }

    //Calculate Player Score with Rule Data
    private void CalculatePlayerScores(PlayerBase player)
    {
        player.matchCount++;

        foreach (var item in playerList)
        {
            item.score = item.matchCount * ruleData.matchPoint;
            foreach (Card card in item.WonCards)
            {
                CardInfo cardInfo = card.cardData.cardInfo;
                if (cardInfo.cardValue == CardValue.Number)
                {
                    if (cardInfo.cardNumber == 1)
                        item.score += ruleData.acePoint;
                    else if (cardInfo.cardType == CardType.Spade && cardInfo.cardNumber == 2)
                        item.score += ruleData.spadeTwoPoint;
                    else if (cardInfo.cardType == CardType.Diamond && cardInfo.cardNumber == 10)
                        item.score += ruleData.diamondTenPoint;
                }
                else if (cardInfo.cardValue == CardValue.Jack)
                    item.score += ruleData.jackPoint;
            }

        }
        PlayerBase highestScorer = playerList.OrderByDescending(player => player.score)
                                  .FirstOrDefault();

        // Dont Choose any player if there is same score
        if (playerList.Count(player => player.score == highestScorer.score) > 1)
            highestScorer = null;
        else
            highestScorer.score += ruleData.cardWinnerPoint;

        foreach (var item in playerList)
            item.DisplayScore();

    }
}
