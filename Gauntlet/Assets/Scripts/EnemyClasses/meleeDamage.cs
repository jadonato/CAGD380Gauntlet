using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeDamage : MonoBehaviour
{
    public float damage;
    public bool canSteal;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().takeDamage(damage);
            if (canSteal)
            {

            }
        }
    }
}
