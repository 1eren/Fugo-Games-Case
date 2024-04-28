using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFitter : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            transform.localScale = Vector3.one;

            //calculate the screen ratio
            float screenRatio = (float)Screen.width / (float)Screen.height;
            float targetRatio = spriteRenderer.sprite.bounds.size.x / spriteRenderer.sprite.bounds.size.y;

            if (targetRatio > screenRatio)
            {
                Camera.main.orthographicSize = spriteRenderer.sprite.bounds.size.y / 2;
            }
            else
            {
                float differenceInSize = screenRatio / targetRatio;
                Camera.main.orthographicSize = spriteRenderer.sprite.bounds.size.y / 2 * differenceInSize;
            }
        }
        else
        {
            Debug.LogWarning("SpriteRenderer Cant Find!");
        }
    }
}
