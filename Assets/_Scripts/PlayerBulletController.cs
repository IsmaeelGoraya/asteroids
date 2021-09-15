using System;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    [SerializeField]
    private float _selfDestroyTime;

    public Action OnBulletDestroy;
    public Action OnEnemyDestroy;

    private void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * 40);
        SelDestroy(_selfDestroyTime);
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
