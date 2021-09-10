using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ShipController : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 100.0f;
    [SerializeField]
    private float _shipThrust = 10.0f;

    private Rigidbody2D _shipRigidbody2D;
    private float _shipThurstInput;

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
    }

    //Since we are using physics
    private void FixedUpdate()
    {
        // Thrust the ship with up and down keyboard keys
        _shipRigidbody2D.AddForce(transform.up * _shipThrust * _shipThurstInput);
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
}
