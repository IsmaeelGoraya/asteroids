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
    [SerializeField]
    private GameObject _enemyPrefab;

    private const float _shipSpawnMinTime = 10;
    private const float _shipSpawnMaxTime = 20;

    private int _score;
    private int _asteroidsRemaining;
    private int _wave;

    private bool _gameStarted;
    private bool _gameOver;

    private void Start()
    {
        _shipController.OnShipDestroyed = ShipDestroyedCb;
    }

    private void Update()
    {
        //Start game if player press enter or the start text
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!_gameStarted)
            {
                StartGame();
                _gameStarted = true;
            }

            if (_gameOver)
            {
                RestartGame();
                _gameOver = false;
            }
        }

        //Quit game if player press escape key
        if (Input.GetKeyDown(KeyCode.Escape))
            QuitGame();
    }

    public void StartGame()
    {
        //DO the UI hide, reset operations here
        Debug.Log("Start Game");
        _score = 0;
        _asteroidsRemaining = 0;
        _wave = 1;
        _gameStarted = false;
        _gameOver = false;

        _shipController.Reset();
        _shipController.Show();

        _uIManager.HideMainUI();
        _uIManager.HideGameOver();
        _uIManager.ShowHudUI();
        _uIManager.SetScore(_score);
        _uIManager.SetWave(_wave);

        //Clear previous invokes
        //NOTE: though this can cause troubles if we have more than one invokes
        //since we are only using for one function this should be ok.
        CancelInvoke();

        //Making ship spawn time a little random every game session, to make it
        //a little more challenging
        float firstInvoke = Random.Range(_shipSpawnMinTime,_shipSpawnMaxTime);
        float repeatInvoke = Random.Range(_shipSpawnMinTime,_shipSpawnMaxTime);

        InvokeRepeating("SpawnEnemy", firstInvoke, repeatInvoke);
        NewWave();
    }

    public void RestartGame()
    {
        _score = 0;
        _asteroidsRemaining = 0;
        _wave = 1;
        _gameStarted = false;

        ClearAllAsteroids();
        //Remove the bullets fired previously from scene
        ClearAllBullets();
        _shipController.Reset();
        _shipController.Hide();
        _uIManager.ShowMainUI();
        _uIManager.HideHudUI();
        _uIManager.HideGameOver();
        _uIManager.SetScore(_score);
        _uIManager.SetWave(_wave);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    private void NewWave()
    {
        for (int i = 0; i < _wave * 4; i += 4)
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

            _asteroidsRemaining += 4;
        }
    }

    private void BeforeAsteroidDestroyedCb(GameObject a_obj)
    {
        //Decrease the count for one destroyed
        _asteroidsRemaining -= 1;

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

            _asteroidsRemaining += 2;
        }
        else
        {
            //Score +50
            _score += 50;
        }

        if(_asteroidsRemaining < 1)
        {
            _wave++;
            NewWave();
        }

        _uIManager.SetScore(_score);
        _uIManager.SetWave(_wave);

    }

    private void ClearAllAsteroids()
    {
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        foreach (var asteroid in asteroids)
        {
            Destroy(asteroid);
        }
    }

    private void ClearAllBullets()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (var bullet in bullets)
        {
            Destroy(bullet);
        }
    }

    private void SpawnEnemy()
    {
        //Random position on either left or right side.
        ScreenEdges screenEdges = Utils.ScreenEdges;
        Vector3 randPosition;
        Vector3 shipMovementDirection;

        int rand = Random.Range(0, 2);
        if(rand == 0)
        {
            randPosition = new Vector3(screenEdges.Left - 2, Random.Range(screenEdges.Top - 1, screenEdges.Bottom + 1), 0);
            shipMovementDirection = Vector3.right;
        }
        else
        {
            randPosition = new Vector3(screenEdges.Right + 2, Random.Range(screenEdges.Top - 1, screenEdges.Bottom + 1), 0);
            shipMovementDirection = Vector3.left;
        }

        GameObject enemy = Instantiate(_enemyPrefab, randPosition, Quaternion.identity);
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        enemyController.Direction = shipMovementDirection;
    }

    private void ShipDestroyedCb()
    {
        _uIManager.ShowGameOver();
        _gameOver = true;
    }
}
