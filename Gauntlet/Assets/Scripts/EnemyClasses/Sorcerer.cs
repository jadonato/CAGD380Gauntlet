using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sorcerer : CoreEnemy
{
    public float invisibilityDuration;
    public float invisibilityCooldown;
    public float meleeEngagementRange;
    public ParticleSystem[] lightning;
    public GameObject hitCollider;
    private bool isAttacking;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        findTargets();
        StartCoroutine(ZigZag());
        StartCoroutine(Cooldown());
    }

    // Update is called once per frame
    void Update()
    {
        if (alerted)
        {
            closestPlayer();
            moveToTarget();
            meleeAttack();
        }
        colorCheck();
    }

    private void meleeAttack()
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= meleeEngagementRange && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        for (int i = 0; i < lightning.Length; i++)
        {
            lightning[i].Play();
        }
        hitCollider.SetActive(true);
        hitCollider.GetComponent<meleeDamage>().damage = attackDamage[rank];
        yield return new WaitForSeconds(lightning[0].duration);
        hitCollider.SetActive(false);
        yield return new WaitForSeconds(1);
        isAttacking = false;
    }

    private IEnumerator Cooldown()
    {
        GetComponent<MeshRenderer>().enabled = true;
        for (int i = 0; i < accessories.Length; i++)
        {
            accessories[i].SetActive(true);
        }
        GetComponent<CapsuleCollider>().enabled = true;

        yield return new WaitForSeconds(invisibilityCooldown);
        StartCoroutine(Invisible());
    }

    private IEnumerator Invisible()
    {
        GetComponent<MeshRenderer>().enabled = false;
        for (int i = 0; i < accessories.Length; i++)
        {
            accessories[i].SetActive(false);
        }
        GetComponent<CapsuleCollider>().enabled = false;

        yield return new WaitForSeconds(invisibilityDuration);
        StartCoroutine(Cooldown());
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
