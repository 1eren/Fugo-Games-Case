using UnityEngine;

public static class ScreenPositionUtility
{

    // Method to get the position for the top right corner of the screen
    public static Vector3 TopRight(float offsetX = 0f, float offsetY = 0f)
    {
        Vector3 worldPoint = Camera.main.ViewportToWorldPoint(new Vector3(1f - offsetX, 1f - offsetY, Camera.main.nearClipPlane));
        worldPoint.z = 0f;
        return worldPoint;
    }

    // Method to get the position for the top left corner of the screen
    public static Vector3 TopLeft(float offsetX = 0f, float offsetY = 0f)
    {
        Vector3 worldPoint = Camera.main.ViewportToWorldPoint(new Vector3(offsetX, 1f - offsetY, Camera.main.nearClipPlane));
        worldPoint.z = 0f;
        return worldPoint;
    }

    // Method to get the position for the bottom right corner of the screen
    public static Vector3 BottomRight(float offsetX = 0f, float offsetY = 0f)
    {
        Vector3 worldPoint = Camera.main.ViewportToWorldPoint(new Vector3(1f - offsetX, offsetY, Camera.main.nearClipPlane));
        worldPoint.z = 0f;
        return worldPoint;
    }

    // Method to get the position for the bottom left corner of the screen
    public static Vector3 BottomLeft(float offsetX = 0f, float offsetY = 0f)
    {
        Vector3 worldPoint = Camera.main.ViewportToWorldPoint(new Vector3(offsetX, offsetY, Camera.main.nearClipPlane));
        worldPoint.z = 0f;
        return worldPoint;
    }

    // Method to get the position for the center of the screen
    public static Vector3 Center(float offsetX = 0f, float offsetY = 0f)
    {
        Vector3 worldPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5f + offsetX, 0.5f + offsetY, Camera.main.nearClipPlane));
        worldPoint.z = 0f;
        return worldPoint;
    }

    // Method to get the position for the top center of the screen
    public static Vector3 TopCenter(float offsetX = 0f, float offsetY = 0f)
    {
        Vector3 worldPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5f + offsetX, 1f-offsetY, Camera.main.nearClipPlane));
        worldPoint.z = 0f;
        return worldPoint;
    }

    // Method to get the position for the bottom center of the screen
    public static Vector3 BottomCenter(float offsetX = 0f, float offsetY = 0f)
    {
        Vector3 worldPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5f + offsetX, 0f+offsetY, Camera.main.nearClipPlane));
        worldPoint.z = 0f;
        return worldPoint;
    }

    // Method to get the position for the center left of the screen
    public static Vector3 CenterLeft(float offSetX = 0f, float offsetY = 0f)
    {
        Vector3 worldPoint = Camera.main.ViewportToWorldPoint(new Vector3(0f + offSetX, 0.5f + offsetY, Camera.main.nearClipPlane));
        worldPoint.z = 0f;
        return worldPoint;
    }

    // Method to get the position for the center right of the screen
    public static Vector3 CenterRight(float offSetX = 0f,float offsetY = 0f)
    {
        Vector3 worldPoint = Camera.main.ViewportToWorldPoint(new Vector3(1f-offSetX, 0.5f + offsetY, Camera.main.nearClipPlane));
        worldPoint.z = 0f;
        return worldPoint;
    }
}
