using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private ShipController _shipController;
    [SerializeField]
    private UIManager _uIManager;

    private void Update()
    {
        //Start game if player press enter or the start text
        if (Input.GetKeyDown(KeyCode.Return))
            StartGame();

        //Quit game if player press escape key
        if (Input.GetKeyDown(KeyCode.Escape))
            QuitGame();
    }

    public void StartGame()
    {
        //DO the UI hide, reset operations here
        Debug.Log("Start Game");
        _shipController.Reset();
        _shipController.Show();
        _uIManager.HideMainUI();
        _uIManager.ShowHudUI();
    }

    public void RestartGame()
    {
        _shipController.Reset();
        _shipController.Hide();
        _uIManager.ShowMainUI();
        _uIManager.HideHudUI();
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
