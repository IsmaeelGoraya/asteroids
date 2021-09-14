using System;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private float _selfDestroyTime;

    public Action OnBulletDestroy;

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
}
