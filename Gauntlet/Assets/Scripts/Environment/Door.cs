using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isLocked;
    public GameObject canvas;
    public GameObject[] alertList;
    private new List<GameObject> players = new List<GameObject>();

    private void Update()
    {
        if (players.Count > 0)
        {
            canvas.SetActive(true);
        }
        else
        {
            canvas.SetActive(false);
        }
    }

    public void openDoor()
    {
        if (alertList.Length > 0)
        {
            alertEnemies();
        }

        Destroy(gameObject);

    }

    private void alertEnemies()
    {
        for (int e = 0; e < alertList.Length; e++)
        {
            if (alertList[e] != null)
            {
                if (alertList[e].GetComponent<CoreEnemy>())
                {
                    alertList[e].GetComponent<CoreEnemy>().alerted = true;
                }
                if (alertList[e].GetComponent<Generator>())
                {
                    alertList[e].GetComponent<Generator>().startSpawning();
                }
            }
            
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().door = gameObject;
            players.Add(collision.gameObject);
        }
    }
    

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().door = null;
            players.Remove(other.gameObject);
        }
    }
}
