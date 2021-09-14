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
    private GameObject _largeAsteroidPrefab;
    [SerializeField]
    private GameObject _smallAsteroidPrefab;

    private int _score;

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
        _score = 0;
        _uIManager.SetScore(_score);
        SpawnAsteroids();
    }

    public void RestartGame()
    {
        ClearAllAsteroids();
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
        GameObject asteroid = Instantiate(_largeAsteroidPrefab, new Vector3(Utils.ScreenEdges.Left, Random.Range(Utils.ScreenEdges.Bottom, Utils.ScreenEdges.Top)), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        AsteroidController asteroidController = asteroid.GetComponent<AsteroidController>();
        asteroidController.OnBeforeDestroyed = BeforeAsteroidDestroyedCb;

        //Spawn one to right
        asteroid = Instantiate(_largeAsteroidPrefab, new Vector3(Utils.ScreenEdges.Right, Random.Range(Utils.ScreenEdges.Bottom, Utils.ScreenEdges.Top)), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        asteroidController = asteroid.GetComponent<AsteroidController>();
        asteroidController.OnBeforeDestroyed = BeforeAsteroidDestroyedCb;

        //Spawn one on top
        asteroid = Instantiate(_largeAsteroidPrefab, new Vector3(Random.Range(Utils.ScreenEdges.Left, Utils.ScreenEdges.Right), Utils.ScreenEdges.Top), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        asteroidController = asteroid.GetComponent<AsteroidController>();
        asteroidController.OnBeforeDestroyed = BeforeAsteroidDestroyedCb;

        //Spawn one on bottom
        asteroid = Instantiate(_largeAsteroidPrefab, new Vector3(Random.Range(Utils.ScreenEdges.Left, Utils.ScreenEdges.Right), Utils.ScreenEdges.Bottom), Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        asteroidController = asteroid.GetComponent<AsteroidController>();
        asteroidController.OnBeforeDestroyed = BeforeAsteroidDestroyedCb;
    }

    private void BeforeAsteroidDestroyedCb(GameObject a_obj)
    {
        if (a_obj.name.ToLower().Contains("large"))
        {
            //Spawn two small asteroids
            GameObject asteroid1 = Instantiate(_smallAsteroidPrefab, a_obj.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            GameObject asteroid2 = Instantiate(_smallAsteroidPrefab, a_obj.transform.position, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            AsteroidController asteroid1Controller = asteroid1.GetComponent<AsteroidController>();
            AsteroidController asteroid2Controller = asteroid2.GetComponent<AsteroidController>();
            asteroid1Controller.OnBeforeDestroyed = BeforeAsteroidDestroyedCb;
            asteroid2Controller.OnBeforeDestroyed = BeforeAsteroidDestroyedCb;

            //Score +20
            _score += 20;
        }
        else
        {
            //Score +50
            _score += 50;
        }

        _uIManager.SetScore(_score);
    }

    private void ClearAllAsteroids()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        foreach (var asteroid in asteroids)
        {
            Destroy(asteroid);
        }
    }
}
