using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject connectedPort;
    public bool active = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (active)
            {
                StartCoroutine(cooldown());
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    player.transform.position = new Vector3(connectedPort.transform.position.x, player.transform.position.y, connectedPort.transform.position.z);
                }
                
            }
            
        }
    }
    private IEnumerator cooldown()
    {
        connectedPort.GetComponent<Teleporter>().active = false;
        active = false;
        yield return new WaitForSeconds(5f);
        connectedPort.GetComponent<Teleporter>().active = true;
        active = true;
    }
}
