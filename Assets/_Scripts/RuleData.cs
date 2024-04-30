using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RuleData", menuName = "Pisti/RuleData")]
public class RuleData : ScriptableObject
{
    public int matchPoint;

    public int acePoint;
    public int spadeTwoPoint;
    public int diamondTenPoint;
    public int jackPoint;

    public int cardWinnerPoint;
}
