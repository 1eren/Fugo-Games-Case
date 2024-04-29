using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPlayer : PlayerBase
{
    private void OnEnable()
    {
        CardMoveController.OnCardPlayed.AddListener(MakeMove);
        CardDealer.Instance?.OnCardsDealed.AddListener(() => MakeMove(null, lastMoveOwner: CardOwner.Player));
    }
    private void OnDisable()
    {
        CardMoveController.OnCardPlayed.RemoveListener(MakeMove);
        CardDealer.Instance?.OnCardsDealed.RemoveListener(() => MakeMove(null, lastMoveOwner: CardOwner.Player));

    }

    private void MakeMove(Card lastPlayedCard, CardOwner lastMoveOwner)
    {
        if (deck.Count == 0)
            return;

        if (!isPlayerTurn && CardDealer.Instance.playersCanPlay)
            return;
        isPlayerTurn = false;
        if (lastMoveOwner == CardOwner.Player)
        {
            DOVirtual.DelayedCall(Random.Range(0.2f, 0.5f), () =>
            {
                Card card = deck[0];
                deck.Remove(card);
                card.PlayCard();
                if (card.TryGetComponent(out CardVisual visual))
                {
                    visual.ShowCard();
                }
            });
        }
    }
}
