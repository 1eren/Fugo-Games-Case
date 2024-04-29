using System.Collections;
using UnityEngine;

public class GroundCardsLayout2D : MonoBehaviour
{
    public float randomMaxSpacing = 0.8f; // Spacing Between Cards

    private void OnTransformChildrenChanged()
    {
        StartCoroutine(ArrangeObjectsHorizontally());
    }
    IEnumerator ArrangeObjectsHorizontally()
    {
        yield return new WaitForEndOfFrame();
     
        Transform lastChild = transform.GetChild(transform.childCount - 1);

        lastChild.GetComponent<CardVisual>().SetOrder(transform.childCount);
    }

    public Vector3 GetRandomLayoutPos()
    {
        float randomPosX = Random.Range(-randomMaxSpacing, randomMaxSpacing);
        float randomPosY = Random.Range(-randomMaxSpacing, randomMaxSpacing);

        return transform.position + new Vector3(randomPosX, randomPosY, 0);
    }
}
