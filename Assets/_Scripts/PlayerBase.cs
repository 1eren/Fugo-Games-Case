using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public List<Card> deck = new List<Card>();

    [ReadOnly] public bool isPlayerTurn;
}
