using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            player.transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
