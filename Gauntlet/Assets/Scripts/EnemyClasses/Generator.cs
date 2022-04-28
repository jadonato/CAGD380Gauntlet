using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject enemyType;
    public int rank;
    public float spawnRate;
    // Start is called before the first frame update
    void Start()
    {
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
}
