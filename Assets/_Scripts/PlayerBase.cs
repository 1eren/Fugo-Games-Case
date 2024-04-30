using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour, IPlayer
{
    private List<Card> deck = new List<Card>();

    private List<Card> wonCards = new List<Card>();

    [SerializeField] private TextMeshPro scoreText;

    [ReadOnly] public bool isPlayerTurn;

    public int score;

    [HideInInspector] public int matchCount = 0;

    [SerializeField] private string name;

    public List<Card> Deck { get => deck; set => deck = value; }
    public List<Card> WonCards { get => wonCards; set => wonCards = value; }

    protected void OnEnable()
    {
        scoreText.SetText(name + " | {0} |", score);

    }
  
    public void DisplayScore()
    {
        scoreText.SetText(name + " | {0} |", score);
    }
}
