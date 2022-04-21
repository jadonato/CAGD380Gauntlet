using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour, IDamageable
{
    public float health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        
        health -= damage;
    }
    public void Heal(float heal)
    {
        health += heal;
    }
    public void Die()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            TakeDamage(other.GetComponent<Projectile>().damage);
            Destroy(other.gameObject);
        }
    }
}
