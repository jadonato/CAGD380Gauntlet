using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float cameraPosAdjustment;
    public new List<GameObject> playerList = new List<GameObject>();
    private Camera main;

   
    private void Update()
    {
        if(main == null)
        {
            main = FindObjectOfType<Camera>();
        }
        if (playerList.Count > 0)
        {
            adjustCameraPos();
        }
    }

    public void checkForNewPlayers()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            bool newPlayer = true;
            for(int p = 0; p < playerList.Count; p++)
            {
                if(player == playerList[p])
                {
                    newPlayer = false;
                }
            }

            if (newPlayer)
            {
                playerList.Add(player);
            }
            
        }
    }

    private void adjustCameraPos()
    {
        Vector3 temp = playerList[0].transform.position;
        for (int p = 1; p < playerList.Count; p++)
        {
            if (playerList[p].GetComponent<Player>().isEnabled)
            {

                temp += playerList[p].transform.position;
            }

        }
        temp = temp / playerList.Count;
        //print("temp pos is " + temp);
        main.transform.position = new Vector3(temp.x, main.transform.position.y, temp.z - cameraPosAdjustment);
        //print(main + " pos is " + main.transform.position);
    }
}
