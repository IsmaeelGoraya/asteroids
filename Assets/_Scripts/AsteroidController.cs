using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class AsteroidController : MonoBehaviour
{
    public Action<GameObject> OnBeforeDestroyed;

    private Rigidbody2D _asteroidBody;

    private void Awake()
    {
        _asteroidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _asteroidBody.AddForce(transform.up * Random.Range(-50.0f, 150.0f) * GetComponent<Rigidbody2D>().mass);
        _asteroidBody.angularVelocity = Random.Range(-0.0f, 90.0f);
    }

    public void SpawnSmallAsteroids()
    {
        OnBeforeDestroyed(gameObject);
    }
}
