using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    private float _rightEdge = 0.0f;
    private float _leftEdge = 0.0f;
    private float _topEdge = 0.0f;
    private float _bottomEdge = 0.0f;

    private int _screenWidth;
    private int _screenHeight;

    private void Start()
    {
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
        CalculateScreenEdges();
    }

    private void Update()
    {
        //Check if the user resized the game window
        if(_screenWidth != Screen.width ||
           _screenHeight != Screen.height)
        {
            //Update the bounds
            _screenWidth = Screen.width;
            _screenHeight = Screen.height;
            CalculateScreenEdges();
        }
    }

    private void OnBecameInvisible()
    {
        if (transform.position.x > _rightEdge)
        {
            MoveToLeft();

        }
        else if (transform.position.x < _leftEdge)
        {
            MoveToRight();
        }
        else if (transform.position.y > _topEdge)
        {
            MoveToBottom();
        }
        else if (transform.position.y < _bottomEdge)
        {
            MoveToTop();
        }
    }

    private void CalculateScreenEdges()
    {
        //Get the boundaries of the camera
        float camDistance = Vector3.Distance(this.transform.position, Camera.main.transform.position);
        Vector2 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        _rightEdge = topRight.x;
        _leftEdge = bottomLeft.x;
        _topEdge = topRight.y;
        _bottomEdge = bottomLeft.y;
    }

    private void MoveToLeft()
    {
        transform.position = new Vector3(_leftEdge, transform.position.y, 0);
    }

    private void MoveToRight()
    {
        transform.position = new Vector3(_rightEdge, transform.position.y, 0);
    }

    private void MoveToBottom()
    {
        transform.position = new Vector3(transform.position.x, _bottomEdge, 0);
    }

    private void MoveToTop()
    {
        transform.position = new Vector3(transform.position.x, _topEdge, 0);
    }
}
