using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizantalLayout2D : MonoBehaviour
{
    public float spacing = 0.8f; // Spacing Between Cards

    private void OnTransformChildrenChanged()
    {
        StartCoroutine(ArrangeObjectsHorizontally());

    }
    IEnumerator ArrangeObjectsHorizontally()
    {
        yield return new WaitForEndOfFrame();
        List<Transform> objects = GetAllChilds(transform);

        Vector3 startPos = transform.position + spacing * objects.Count / 2f * Vector3.left;
        startPos -= Vector3.left * spacing / 2f;
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].transform.position = startPos + Vector3.right * spacing * i;
        }
    }

    private List<Transform> GetAllChilds(Transform t)
    {
        List<Transform> childs = new List<Transform>();
        foreach (Transform item in t)
        {
            childs.Add(item);
        }
        return childs;
    }
}
