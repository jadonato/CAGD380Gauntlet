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
        if (!isLocked)
        {
            if (alertList.Length > 0)
            {
                alertEnemies();
            }
            
            Destroy(gameObject);
        }
        
    }

    private void alertEnemies()
    {
        for(int e = 0; e < alertList.Length; e++)
        {
            if(alertList[e] != null)
            {
                if (alertList[e].GetComponent<Grunt>())
                {
                    alertList[e].GetComponent<Grunt>().alerted = true;
                }
                if (alertList[e].GetComponent<Demon>())
                {
                    alertList[e].GetComponent<Demon>().alerted = true;
                }
                if (alertList[e].GetComponent<Sorcerer>())
                {
                    alertList[e].GetComponent<Sorcerer>().alerted = true;
                }
                if (alertList[e].GetComponent<Generator>())
                {
                    alertList[e].GetComponent<Generator>().alerted = true;
                }
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            players.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            players.Remove(other.gameObject);
        }
    }
}
