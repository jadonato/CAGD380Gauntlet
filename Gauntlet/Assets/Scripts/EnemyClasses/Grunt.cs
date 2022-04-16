using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : CoreEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (alerted)
        {
            moveToTarget();


        }
    }

    private void clubAttack()
    {
        print("Attack player");
    }
}
