using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipController : MonoBehaviour
{
    public Action OnShipDestroyed;
    public Action OnEnemyDestroyed;

    [SerializeField]
    private float _rotationSpeed = 100.0f;
    [SerializeField]
    private float _shipThrust = 10.0f;
    [SerializeField]
    private GameObject _bulletPrefab;

    private Rigidbody2D _shipRigidbody2D;
    private float _shipThurstInput;
    private int _bulletCount;
    private const int _bulletLimit = 5;

    private void Awake()
    {
        _bulletCount = 0;
    }

    private void Start()
    {
        _shipRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Rotate ship with left and right keyboard keys
        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * _rotationSpeed * Time.deltaTime);
        // Scan ship thurst input from left right keyboard keys here.
        _shipThurstInput = Input.GetAxis("Vertical");
        // Fire bullet on ctrl key
        // and no more than limit bullets
        if ((Input.GetKeyDown(KeyCode.LeftControl) ||
            Input.GetKeyDown(KeyCode.RightControl)) &&
            _bulletCount < _bulletLimit)
            FireBullet();
    }

    //Since we are using physics
    private void FixedUpdate()
    {
        // Thrust the ship with up and down keyboard keys
        _shipRigidbody2D.AddForce(transform.up * _shipThrust * _shipThurstInput);
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(_bulletPrefab,new Vector3(transform.position.x, transform.position.y, 0),transform.rotation);
        PlayerBulletController bulletController = bullet.GetComponent<PlayerBulletController>();
        bulletController.OnBulletDestroy = BulletDestroyCb;
        bulletController.OnEnemyDestroy = EnemyDestroyCb;
        _bulletCount++;
    }

    private void BulletDestroyCb()
    {
        _bulletCount--;
    }

    private void EnemyDestroyCb()
    {
        OnEnemyDestroyed();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Reset()
    {
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" || other.tag == "Asteroid")
        {
            OnShipDestroyed();
            Hide();
        }
    }
}
