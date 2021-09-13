using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    private int _screenWidth;
    private int _screenHeight;

    private ScreenEdges _screenEdges;

    private void Start()
    {
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;
        _screenEdges = Utils.ScreenEdges;
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
            _screenEdges = Utils.ScreenEdges;
        }

        if (transform.position.x > _screenEdges.Right)
        {
            MoveToLeft();

        }
        else if (transform.position.x < _screenEdges.Left)
        {
            MoveToRight();
        }
        else if (transform.position.y > _screenEdges.Top)
        {
            MoveToBottom();
        }
        else if (transform.position.y < _screenEdges.Bottom)
        {
            MoveToTop();
        }
    }

    private void MoveToLeft()
    {
        transform.position = new Vector3(_screenEdges.Left, transform.position.y, 0);
    }

    private void MoveToRight()
    {
        transform.position = new Vector3(_screenEdges.Right, transform.position.y, 0);
    }

    private void MoveToBottom()
    {
        transform.position = new Vector3(transform.position.x, _screenEdges.Bottom, 0);
    }

    private void MoveToTop()
    {
        transform.position = new Vector3(transform.position.x, _screenEdges.Top, 0);
    }
}
