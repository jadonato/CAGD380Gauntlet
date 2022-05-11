using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : CoreEnemy
{
    public float kamikazeRange;
   
    // Start is called before the first frame update
    void Start()
    {
        rankCheck();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;

        StartCoroutine(ZigZag());
    }

    // Update is called once per frame
    void Update()
    {

        findTargets();
        if (alerted && playerList.Count > 0)
        {
            closestPlayer();
            moveToTarget();
            kamikaze();
        }
    }



    private void kamikaze()
    {
        if(Vector3.Distance(transform.position, target.transform.position) < kamikazeRange)
        {
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (Vector3.Distance(transform.position, player.transform.position) < kamikazeRange)
                {
                    player.GetComponent<Player>().takeDamage(attackDamage[rank - 1]);
                }
            }
            Destroy(gameObject);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            TakeDamage(other.GetComponent<Projectile>().damage);
            Destroy(other.gameObject);
        }
        checkForZig(other, true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Projectile")
        {
            TakeDamage(other.GetComponent<Projectile>().damage);
            Destroy(other.gameObject);
        }
        checkForZig(other, false);
    }

    
}
