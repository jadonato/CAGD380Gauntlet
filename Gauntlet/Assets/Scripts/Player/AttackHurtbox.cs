using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHurtbox : MonoBehaviour
{
    public float damage;

    private MeshRenderer _mRenderer;
    private Collider _collider;

    private void Awake()
    {
        _mRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();

        SetActiveState(false);
    }

    public void SetActiveState(bool input)
    {
        _mRenderer.enabled = input;
        _collider.enabled = input;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            Debug.Log("Damaged enemy " + other.gameObject);
            damageable.TakeDamage(damage);
        }
    }
}
