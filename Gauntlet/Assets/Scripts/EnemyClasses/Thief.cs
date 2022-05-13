using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Thief : CoreEnemy
{
    public float meleeEngagementRange;
    public GameObject hitCollider;
    public int stolenScore;
    private bool isAttacking;
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
        if (alerted && playerList.Count > 0 )
        {
            if (!hasBag)
            {
                closestPlayer();
                agent.destination = target.transform.position;
                if (Vector3.Distance(transform.position, target.transform.position) <= agent.stoppingDistance)
                {
                    Transform temp = target.transform;
                    temp.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
                    transform.LookAt(target.transform);
                    agent.speed = 0;
                }
                else
                {
                    agent.speed = speed;
                }
                stealAttack();
            }
            else
            {
                print("Thief's target is " + target);
            }
        }
    }

    public void escape(int score)
    {
        stolenScore = score;
        target = FindObjectOfType<LevelManager>().gameObject;
        agent.SetDestination(target.transform.position);
    }

    private void stealAttack()
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
        hitCollider.GetComponent<meleeDamage>().damage = attackDamage[0];
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
        checkForZig(other, true);
        if (other.GetComponent<LevelManager>())
        {
            Destroy(gameObject);
        }
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
