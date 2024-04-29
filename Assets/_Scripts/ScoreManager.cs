using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct GameMove
{
    public Card card;
    public CardOwner owner;
}
public class ScoreManager : MonoBehaviour
{
    public List<GameMove> allMoves = new List<GameMove>();

    private void OnEnable()
    {
        CardMoveController.OnCardPlayed.AddListener(AddGameMoveToList);
    }
    private void OnDisable()
    {
        CardMoveController.OnCardPlayed.RemoveListener(AddGameMoveToList);
    }
    private void AddGameMoveToList(Card card, CardOwner owner)
    {
        GameMove gameMove = new GameMove();
        gameMove.card = card;
        gameMove.owner = owner;
        allMoves.Add(gameMove);
    }
}
