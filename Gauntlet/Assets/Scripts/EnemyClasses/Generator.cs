using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
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
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        GameObject temp = enemyType;
        if (temp.GetComponent<Grunt>())
        {
            temp.GetComponent<Grunt>().rank = rank;
        }
        if (temp.GetComponent<Demon>())
        {
            temp.GetComponent<Demon>().rank = rank;
        }
        if (temp.GetComponent<Sorcerer>())
        {
            temp.GetComponent<Sorcerer>().rank = rank;
        }
        Instantiate(temp, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(spawnRate);
        StartCoroutine(SpawnEnemy());
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
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
