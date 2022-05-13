using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Death : MonoBehaviour, IDamageable
{
    public float drainRange;
    public float drainDamagePerFrame;
    public int[] scoreList;
    public float speed;
    private int score = 0;
    private float damageDealt;
    private bool draining;
    protected GameObject target;
    public List<GameObject> playerList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.CreateAnnouncement("You feel a cold wind travel down your spine");
    }

    // Update is called once per frame
    void Update()
    {

        findTargets();
        if (playerList.Count > 0)
        {
            closestPlayer();
            floatToPlayer();
        }
    }
    private void FixedUpdate()
    {
        if (playerList.Count > 0)
        {
            drainLife();
        }
    }

    private void drainLife()
    {
        if(Vector3.Distance(transform.position, target.transform.position) < drainRange)
        {
            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (Vector3.Distance(transform.position, player.transform.position) < drainRange)
                {
                    damageDealt += drainDamagePerFrame;
                    player.GetComponent<Player>().takeDamage(drainDamagePerFrame);
                    if(damageDealt >= 200)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    private void floatToPlayer()
    {
        transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    protected void findTargets()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            playerList.Add(player);
        }

    }
    protected void closestPlayer()
    {

        for (int p = 0; p < playerList.Count; p++)
        {
            if (playerList[p] == null)
            {
                playerList.Remove(playerList[p]);
            }
        }
        target = playerList[0];
        for (int p = 0; p < playerList.Count; p++)
        {
            if (Vector3.Distance(transform.position, playerList[p].transform.position) < Vector3.Distance(transform.position, target.transform.position))
            {
                target = playerList[p];
            }
        }

    }
    public void DeathDie(Player player)
    {
        player.Score += scoreList[score];
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
    }
    private void OnTriggerExit(Collider other)
    {

    }
    

    public void TakeDamage(float damage)
    {
        if(score < scoreList.Length)
        {
            score += 1;
        }
        else
        {
            score = 0;
        }
        print("new score is " + scoreList[score]);
    }

    public void Heal(float heal)
    {
        heal = 0;
    }
    public void Die()
    {
        
       // 
    }
}
