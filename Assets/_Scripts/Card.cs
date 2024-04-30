using DG.Tweening;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public enum CardOwner
{
    Player,
    Computer,
    Ground,
    None,
}
public class Card : MonoBehaviour
{
    [ReadOnly] public CardData cardData;

    [ReadOnly] private CardOwner owner = CardOwner.None;
    [ReadOnly] public CardOwner Owner => owner;

  
    public void PlayCard(PlayerBase player)
    {
        CardMoveController.OnCardPlayed.Invoke(this, owner, player);
        MoveCard(CardOwner.Ground);
    }
    public void MoveCard(CardOwner targetOwner)
    {
        Transform targetTransform = null;
        Vector3 targetPos = Vector3.zero;
        switch (targetOwner)
        {
            case CardOwner.Player:
                targetTransform = CardDealer.Instance.mainPlayer.transform;
                targetPos = targetTransform.position;
                break;
            case CardOwner.Computer:
                targetTransform = CardDealer.Instance.computerPlayer.transform;
                targetPos = targetTransform.position;
                break;
            case CardOwner.Ground:
                targetTransform = CardDealer.Instance.groundCardsParent.transform;
                targetPos = GroundCardsManager.Instance.GetRandomLayoutPos();
                break;
            case CardOwner.None:
                return;
            default:
                break;
        }
        transform.DOMove(targetPos, GameManager.Instance.gamePrefences.visualPrefences.cardMoveTime).
             OnComplete(() => transform.parent = targetTransform);
        AssingOwner(targetOwner);
    }


    public bool IsOwnerPlayer()
    {
        return owner == CardOwner.Player;
    }

    public void AssingOwner(CardOwner newOwner)
    {
        owner = newOwner;
    }
}
