using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeDamage : MonoBehaviour
{
    public float damage;
    public bool canSteal;
    private bool stole = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player temp = other.GetComponent<Player>();
            temp.takeDamage(damage);
            if (canSteal)
            {

                print("Stole from player");
                int stolenScore = 500;
                if(temp.Score <= 500)
                {
                    temp.Score = 0;
                }
                else
                {
                    temp.Score -= stolenScore;
                }
                gameObject.GetComponentInParent<Thief>().escape(stolenScore);
                gameObject.GetComponentInParent<Thief>().hasBag = true;
            }
        }
    }
}
