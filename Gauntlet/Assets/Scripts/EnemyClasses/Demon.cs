using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Demon : CoreEnemy
{
    
    public float Range;
    public float projectileSpeed;
    public float firingRate;
    public GameObject projectile;

    private bool firing;
    // Start is called before the first frame update
    void Start()
    {
        rankCheck();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        findTargets();
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
            RangedAttack();

        }
        
    }

    private void RangedAttack()
    {
        if(Vector3.Distance(transform.position, target.transform.position) <= Range)
        {
            agent.speed = 0;

            Vector3 temp = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);

            transform.LookAt(temp);
            if (!firing)
            {
                StartCoroutine(Fire());

            }
        }
        else
        {
            agent.speed = speed;
        }
    }

    private IEnumerator Fire()
    {
        Vector3 dir = target.transform.position - transform.position;
        dir = new Vector3(dir.x, 0, dir.z);
        GameObject temp = projectile;
        temp.GetComponent<Projectile>().speed = projectileSpeed;
        temp.GetComponent<Projectile>().damage = attackDamage[rank];
        temp.GetComponent<Projectile>().source = gameObject;
        temp.GetComponent<Projectile>().dir = dir;
        Instantiate(temp, transform.position + (transform.forward * 2), Quaternion.identity);
        

        firing = true;
        yield return new WaitForSeconds(1f / firingRate);
        
        firing = false;
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
