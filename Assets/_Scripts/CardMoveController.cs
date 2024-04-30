using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CardMoveController : Singleton<CardMoveController>
{
    [HideInInspector] public static UnityEvent<Card, CardOwner, PlayerBase> OnCardPlayed = new UnityEvent<Card, CardOwner, PlayerBase>();

    public List<PlayerBase> players = new List<PlayerBase>();

    private int playerTurnIndex;
    private void OnEnable()
    {
        players = FindObjectsOfType<PlayerBase>().ToList();
        playerTurnIndex = Random.Range(0, players.Count);
        ChangeTurn();

        OnCardPlayed.AddListener((x, y,z) => ChangeTurn());
    }
    private void OnDisable()
    {
        OnCardPlayed.AddListener((x, y,z) => ChangeTurn());
    }
    public void ChangeTurn()
    {
        if (playerTurnIndex == players.Count)
            playerTurnIndex = 0;

        players[playerTurnIndex].isPlayerTurn = true;

        playerTurnIndex++;
    }
}
