using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer 
{
    public List<Card> Deck { get; set; }
    public List<Card> WonCards { get; set; }
}
