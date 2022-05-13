using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string levelName;
    public bool finalLevel;
    private List<GameObject> players = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (finalLevel)
            {
                GameManager.Instance.YouWin();
            }
            else
            {
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    DontDestroyOnLoad(player);
                }
                DontDestroyOnLoad(FindObjectOfType<CameraControl>());
                SceneManager.LoadScene(levelName);
                foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
                {
                    player.GetComponent<Player>().goToSpawnPoint();
                }
            }
            
            
        }
        
    }
}
