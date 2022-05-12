using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool hurtPlayer;
    public bool activatePotions;
    public float speed;
    public float damage;
    public GameObject source;
    public Vector3 dir = new Vector3(0, 0, 0);



    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = dir.normalized * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("Projectile deals damage");
            other.GetComponent<Player>().takeDamage(damage);
        }
        if (other.tag != "BigRoom" && !other.GetComponent<PlayerAttacking>())
        {
            
            Destroy(gameObject);
        }
        
    }
}
