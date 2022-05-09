using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoreEnemy : MonoBehaviour, IDamageable
{
    public float health;
    public float speed;
    public float[] attackDamage;
    public int rank;
    public Material[] rankColor;
    public GameObject[] accessories;
    public bool alerted;


    protected GameObject target;
    protected List<GameObject> playerList = new List<GameObject>();
    protected NavMeshAgent agent;
    protected bool zigLeft;

    
    protected void moveToTarget()
    {
        agent.destination = target.transform.position;
        if(Vector3.Distance(transform.position, target.transform.position) <= agent.stoppingDistance)
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
        if (Vector3.Distance(transform.position, target.transform.position) > 3)
        {
            if (zigLeft)
            {
                transform.position -= transform.right * speed/2 * Time.deltaTime;
            }
            else
            {
                transform.position += transform.right * speed/2 * Time.deltaTime;
            }
        }
        
    }
    protected void colorCheck()
    {
        if (rank > 0)
        {
            GetComponent<MeshRenderer>().material = rankColor[rank - 1];
            for(int a = 0; a < accessories.Length; a++)
            {
                accessories[a].GetComponent<MeshRenderer>().material = rankColor[rank - 1];
            }
        }
    }

    protected void findTargets()
    {
        foreach(GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            playerList.Add(player);
        }
    }
    protected void closestPlayer()
    {

        for(int p = 0; p < playerList.Count; p++)
        {
            if(playerList[p] == null)
            {
                playerList.Remove(playerList[p]);
            }
        }
        target = playerList[0];
        for(int p = 0; p < playerList.Count; p++)
        {
            if(Vector3.Distance(transform.position, playerList[p].transform.position) < Vector3.Distance(transform.position, target.transform.position))
            {
                target = playerList[p];
            }
        }
        
    }
    protected IEnumerator ZigZag()
    {
        zigLeft = true;
        yield return new WaitForSeconds(1);
        zigLeft = false;
        yield return new WaitForSeconds(1);
        StartCoroutine(ZigZag());
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    public void Heal(float heal)
    {
        heal = 0;
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
