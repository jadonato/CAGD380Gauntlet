using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private MainUI _mainUI;
    [SerializeField] private float _announcementTime = 5f;

    private bool _isPaused = false;

    #region Properties
    public bool IsPaused { get { return _isPaused; } }
    public MainUI MainUI { get { return _mainUI; } }
    #endregion

    private void Awake()
    {
        //Singleton
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.Log("A Gamemamger already exists. Deleting the new one", Instance);
            Destroy(this);
        }
    }

    public void PauseGameTime(bool input)
    {
        if(input)
        {
            Time.timeScale = 0;
            SetAllPlayersEnabled(false);
        }
        else
        {
            Time.timeScale = 1;
            SetAllPlayersEnabled(true);
        }

        _isPaused = input;
    }

    public void CreateAnnouncement(string text)
    {
        _mainUI.CreateAnnouncement(text, _announcementTime);
    }

    private void SetAllPlayersEnabled(bool input)
    {
        foreach(Player player in FindObjectsOfType<Player>())
        {
            player.isEnabled = input;
        }
    }
}
