using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoreEnemy : MonoBehaviour
{
    public float speed;
    public float damage;
    public int rank;
    public bool alerted;

    protected GameObject target;
    protected NavMeshAgent agent;

    protected void moveToTarget()
    {
        agent.destination = target.transform.position;
    }

    protected void findTargets()
    {
        //FindObjectsOfType<>
    }
    
}
