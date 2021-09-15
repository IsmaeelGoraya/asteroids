using System;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    public Action OnBulletDestroy;
    public Action OnEnemyDestroy;

    [SerializeField]
    private float _selfDestroyTime;
    private const float _bulletSpeed = 15.0f;

    private void OnEnable()
    {
        Invoke("SelDestroy", _selfDestroyTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);
    }

    private void SelDestroy()
    {
        gameObject.SetActive(false);
        OnBulletDestroy();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Asteroid")
        {
            AsteroidController asteroidController = other.GetComponent<AsteroidController>();
            asteroidController.SpawnSmallAsteroids();
            Destroy(other.gameObject);
            gameObject.SetActive(false);
            OnBulletDestroy();
        }
        else if(other.tag == "Enemy")
        {
            OnEnemyDestroy();
            Destroy(other.gameObject);
            gameObject.SetActive(false);
            OnBulletDestroy();
        }
    }
}
