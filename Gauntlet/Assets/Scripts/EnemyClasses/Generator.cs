using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour, IDamageable
{
    public float healthBase;
    public float healthMod;
    public GameObject enemyType;
    public int rank;
    public float spawnRate;
    public bool alerted;
    private float health;
    // Start is called before the first frame update
    void Start()
    {
        health = healthBase + (healthMod * rank);
        if (alerted)
        {
            StartCoroutine(SpawnEnemy());
        }
        
    }

    public void startSpawning()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        GameObject temp = enemyType;
        if (temp.GetComponent<CoreEnemy>())
        {
            temp.GetComponent<CoreEnemy>().rank = rank;
        }
        Instantiate(temp, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnRate);
        StartCoroutine(SpawnEnemy());
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    public void Heal(float heal)
    {
        heal = 0;
    }
    public void Die()
    {
        GameManager.Instance.CreateAnnouncement("You have destroyed a generator");
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
