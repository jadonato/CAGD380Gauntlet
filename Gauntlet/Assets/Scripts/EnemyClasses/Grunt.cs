using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grunt : CoreEnemy
{
    public float meleeEngagementRange;
    public GameObject hitCollider;
    private bool isAttacking;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        findTargets();
        StartCoroutine(ZigZag());
    }

    // Update is called once per frame
    void Update()
    {
        if (alerted)
        {
            closestPlayer();
            moveToTarget();
            clubAttack();
        }
        colorCheck();
    }

    

    private void clubAttack()
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= meleeEngagementRange && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        hitCollider.SetActive(true);
        hitCollider.GetComponent<meleeDamage>().damage = attackDamage[rank];
        yield return new WaitForSeconds(0.5f);
        hitCollider.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            TakeDamage(other.GetComponent<Projectile>().damage);
            Destroy(other.gameObject);
        }

    }
}
