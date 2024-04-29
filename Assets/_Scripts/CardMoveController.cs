using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CardMoveController : Singleton<CardMoveController>
{
    [HideInInspector] public static UnityEvent<Card, CardOwner> OnCardPlayed = new UnityEvent<Card, CardOwner>();

    public List<PlayerBase> players = new List<PlayerBase>();

    private int playerTurnIndex;
    private void OnEnable()
    {
        players = FindObjectsOfType<PlayerBase>().ToList();
        playerTurnIndex = Random.Range(0, players.Count);
        ChangeTurn();

        OnCardPlayed.AddListener((x, y) => ChangeTurn());
    }
    private void OnDisable()
    {
        OnCardPlayed.AddListener((x, y) => ChangeTurn());
    }
    public void ChangeTurn()
    {
        if (playerTurnIndex == players.Count)
            playerTurnIndex = 0;
     
        players[playerTurnIndex].isPlayerTurn = true;

        playerTurnIndex++;
    }
}
