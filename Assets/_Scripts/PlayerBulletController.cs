using System;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    [SerializeField]
    private float _selfDestroyTime;
    private const float _bulletSpeed = 15.0f;

    public Action OnBulletDestroy;
    public Action OnEnemyDestroy;

    private void Start()
    {
        SelDestroy(_selfDestroyTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);
    }

    private void SelDestroy(float a_after)
    {
        Destroy(gameObject, a_after);
    }

    private void OnDestroy()
    {
        OnBulletDestroy();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Asteroid")
        {
            AsteroidController asteroidController = other.GetComponent<AsteroidController>();
            asteroidController.SpawnSmallAsteroids();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }else if(other.tag == "Enemy")
        {
            OnEnemyDestroy();
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
