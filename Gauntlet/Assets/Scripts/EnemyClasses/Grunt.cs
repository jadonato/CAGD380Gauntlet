using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Grunt : CoreEnemy
{
    public Material[] rankColor;
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
        }
        colorCheck();
    }

    private void colorCheck()
    {
        if (rank > 0)
        {
            GetComponent<MeshRenderer>().material = rankColor[rank - 1];
        }
    }

    private void clubAttack()
    {
        print("Attack player");
    }
}
