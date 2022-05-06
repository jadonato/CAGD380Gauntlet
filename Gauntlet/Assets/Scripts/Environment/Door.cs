using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isLocked;
    public GameObject canvas;
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
            Destroy(gameObject);
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
