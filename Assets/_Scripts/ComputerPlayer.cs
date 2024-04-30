using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPlayer : PlayerBase
{
    private void OnEnable()
    {
        base.OnEnable();
        CardMoveController.OnCardPlayed.AddListener((x, y, z) => MakeMove(y));
        CardDealer.Instance?.OnCardsDealed.AddListener(() => MakeMove(lastMoveOwner: CardOwner.Player));
    }
    private void OnDisable()
    {
        base.OnDisable();
        CardMoveController.OnCardPlayed.RemoveListener((x, y, z) => MakeMove(y));
        CardDealer.Instance?.OnCardsDealed.RemoveListener(() => MakeMove(lastMoveOwner: CardOwner.Player));
    }

    private void MakeMove(CardOwner lastMoveOwner)
    {
        if (Deck.Count == 0)
            return;

        if (!isPlayerTurn && CardDealer.Instance.playersCanPlay)
            return;
        isPlayerTurn = false;
        if (lastMoveOwner == CardOwner.Player)
        {
            DOVirtual.DelayedCall(Random.Range(0.3f, 0.5f), () =>
            {
                Card card = Deck[Random.Range(0, Deck.Count - 1)];
                Deck.Remove(card);
                card.PlayCard(this);
                if (card.TryGetComponent(out CardVisual visual))
                {
                    visual.ShowCard();
                }
            });
        }
    }
}
