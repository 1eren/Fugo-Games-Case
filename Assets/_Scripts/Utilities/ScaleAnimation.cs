using UnityEngine;
using DG.Tweening;

public enum ScaleDirection { ScaleUp, ScaleDown }
public enum ScaleAxis { X, Y, Z, All } // T�m ekseni temsil eden "All" se�ene�i eklenmi�tir

public class ScaleAnimation : MonoBehaviour
{
    public ScaleDirection ScaleDirection = ScaleDirection.ScaleUp;
    public ScaleAxis ScaleAxis = ScaleAxis.All; // Eksenleri se�ebilme parametresi
    public Ease Ease = Ease.Linear;
    public float Duration = 1f;
    public float ScaleFactor = 1.1f;
    public float Delay = 0f;

    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;

        switch (ScaleDirection)
        {
            case ScaleDirection.ScaleUp:
                ScaleUp();
                break;
            case ScaleDirection.ScaleDown:
                ScaleDown();
                break;
            default:
                break;
        }
    }

    void ScaleUp()
    {
        Vector3 targetScale = originalScale;

        // Se�ilen ekseni kullanarak hedef �l��y� ayarla
        if (ScaleAxis == ScaleAxis.All || ScaleAxis == ScaleAxis.X)
        {
            targetScale.x *= ScaleFactor;
        }
        if (ScaleAxis == ScaleAxis.All || ScaleAxis == ScaleAxis.Y)
        {
            targetScale.y *= ScaleFactor;
        }
        if (ScaleAxis == ScaleAxis.All || ScaleAxis == ScaleAxis.Z)
        {
            targetScale.z *= ScaleFactor;
        }

        transform.DOScale(targetScale, Duration).SetDelay(Delay)
            .SetEase(Ease)
            .SetLoops(-1, LoopType.Yoyo);
    }

    void ScaleDown()
    {
        Vector3 targetScale = originalScale;

        // Se�ilen ekseni kullanarak hedef �l��y� ayarla
        if (ScaleAxis == ScaleAxis.All || ScaleAxis == ScaleAxis.X)
        {
            targetScale.x /= ScaleFactor;
        }
        if (ScaleAxis == ScaleAxis.All || ScaleAxis == ScaleAxis.Y)
        {
            targetScale.y /= ScaleFactor;
        }
        if (ScaleAxis == ScaleAxis.All || ScaleAxis == ScaleAxis.Z)
        {
            targetScale.z /= ScaleFactor;
        }

        transform.DOScale(targetScale, Duration).SetDelay(Delay)
            .SetEase(Ease)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
