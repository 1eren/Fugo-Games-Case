using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct VisualPrefences
{
    public float playerCardsScreenPadding;
    public float computerCardsScreenPadding;
    public float allDeckScreenPadding;

    public float cardsScale;
}

[CreateAssetMenu(menuName = "Pisti/Prefences Data")]
public class GamePrefencesData :ScriptableObject
{
    public VisualPrefences visualPrefences;
}
