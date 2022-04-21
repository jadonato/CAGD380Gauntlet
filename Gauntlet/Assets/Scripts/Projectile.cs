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
}
