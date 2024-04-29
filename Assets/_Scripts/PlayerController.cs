using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerBase
{
    private void Update()
    {
        if (!CardDealer.Instance.playersCanPlay)
            return;
        MakeMove();
    }
    private void MakeMove()
    {
        if (Input.GetMouseButtonDown(0) && isPlayerTurn)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity);

            if (hit && hit.collider.TryGetComponent(out Card card))
            {
                if (card.IsOwnerPlayer())
                {
                    isPlayerTurn = false;
                    deck.Remove(card);
                    card.PlayCard();
                }
            }
        }
    }
}
