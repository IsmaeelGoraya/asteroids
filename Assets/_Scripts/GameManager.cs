using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private ShipController _shipController;
    [SerializeField]
    private UIManager _uIManager;
    [SerializeField]
    private GameObject _asteroidPrefab;

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
        SpawnAsteroids();
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

    private void SpawnAsteroids()
    {
        //Spawn one to left
        Instantiate(_asteroidPrefab, new Vector3(Utils.ScreenEdges.Left, Random.Range(Utils.ScreenEdges.Bottom, Utils.ScreenEdges.Top)), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        //Spawn one to right
        Instantiate(_asteroidPrefab, new Vector3(Utils.ScreenEdges.Right, Random.Range(Utils.ScreenEdges.Bottom, Utils.ScreenEdges.Top)), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        //Spawn one on top
        Instantiate(_asteroidPrefab, new Vector3(Random.Range(Utils.ScreenEdges.Left, Utils.ScreenEdges.Right), Utils.ScreenEdges.Top), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        //Spawn one on bottom
        Instantiate(_asteroidPrefab, new Vector3(Random.Range(Utils.ScreenEdges.Left, Utils.ScreenEdges.Right), Utils.ScreenEdges.Bottom), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
    }
}
