using UnityEngine;

public static class Utils
{
    private static ScreenEdges _screenEdges;

    public static ScreenEdges ScreenEdges
    {
        get
        {
            CalculateScreenEdges();
            return _screenEdges;
        }
    }

    private static void CalculateScreenEdges()
    {

        //Get the boundaries of the camera
        float camDistance = Vector3.Distance(Vector3.zero, Camera.main.transform.position);
        Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        ScreenEdges retScreenEdges = new ScreenEdges(bottomLeft.x,
                                                     topRight.x,
                                                     topRight.y,
                                                     bottomLeft.y);
        _screenEdges = retScreenEdges;
    }
}
