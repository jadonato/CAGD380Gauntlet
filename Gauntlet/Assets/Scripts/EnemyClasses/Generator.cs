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
        Instantiate(temp, transform.position, Quaternion.identity);
        if (temp.GetComponent<Grunt>())
        {
            temp.GetComponent<Grunt>().rank = rank;
        }
        if (temp.GetComponent<Demon>())
        {
            temp.GetComponent<Demon>().rank = rank;
        }

        yield return new WaitForSeconds(spawnRate);
        StartCoroutine(SpawnEnemy());
    }
}
