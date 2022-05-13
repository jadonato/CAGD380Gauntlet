using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostGameUI : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        GameManager oldManager = GameManager.Instance;
        oldManager.ClearInstance();
        Destroy(oldManager.gameObject);
        SceneManager.LoadScene("MainMenu");
    }
}
