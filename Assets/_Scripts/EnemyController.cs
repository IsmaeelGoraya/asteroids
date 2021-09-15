using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector3 Direction;

    private const float _minSpeed = 5;
    private const float _maxSpeed = 10;
    private float _speed;

    private void Start()
    {
        //Pick random speed in limit
        _speed = Random.Range(_minSpeed, _maxSpeed);
    }


    private void Update()
    {
        transform.Translate(Direction * _speed * Time.deltaTime);
    }

    public void ShootAt(Vector3 a_position)
    {

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
