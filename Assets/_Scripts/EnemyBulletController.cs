using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    private Vector3 _playerPosition;
    private Vector3 _fireDirection;

    private void Update()
    {
        transform.Translate(_fireDirection * 10 * Time.deltaTime);
    }

    public void ShootAtPlayer(Vector3 a_playerPosition)
    {
        _playerPosition = a_playerPosition;
        _fireDirection = (_playerPosition - transform.position).normalized;
        Destroy(gameObject, 2.0f);
    }
}
