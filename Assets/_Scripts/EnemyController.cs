using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Vector3 Direction;

    [SerializeField]
    private GameObject _enemyBulletPrefab;

    private const float _minSpeed = 1;
    private const float _maxSpeed = 5;
    private const float _firstFireInstant = 0.5f;
    private const float _fireInterval = 2.0f;
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

    public void ShootPlayer()
    {
        InvokeRepeating("FireBullet", _firstFireInstant, _fireInterval);
    }

    private void FireBullet()
    {
        GameObject playerShip = GameObject.FindGameObjectWithTag("Player");
        if (playerShip != null)
        {
            GameObject bullet = Instantiate(_enemyBulletPrefab, transform.position, Quaternion.identity);
            EnemyBulletController enemyBulletController = bullet.GetComponent<EnemyBulletController>();
            enemyBulletController.ShootAtPlayer(playerShip.transform.position);
        }
    }

    private void OnDestroy()
    {
        CancelInvoke();
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
