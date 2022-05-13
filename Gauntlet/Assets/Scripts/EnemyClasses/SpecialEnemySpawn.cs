using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemySpawn : MonoBehaviour
{
    public GameObject[] enemies;
    public float spawnTime;
    private bool spawnEnemies = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<Player>() && !spawnEnemies)
        {
            spawnEnemies = true;
            StartCoroutine(spawn());
        }
    }

    private IEnumerator spawn()
    {
        yield return new WaitForSeconds(spawnTime);
        for(int e = 0; e < enemies.Length; e++)
        {
            enemies[e].SetActive(true);
            
        }
        Destroy(gameObject);
    }
}
