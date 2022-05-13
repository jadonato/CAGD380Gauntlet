using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagicProjectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    public float Speed { get { return _speed; } set { _speed = value; } }
    public float Damage { get { return _damage; } set { _damage = value; } }

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            Debug.Log("Damaged enemy " + other.gameObject);
            damageable.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
