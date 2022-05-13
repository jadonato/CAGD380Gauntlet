using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private MainUI _mainUI;
    [SerializeField] private float _announcementTime = 5f;
    [SerializeField] private CameraControl _cameraController;
    [SerializeField] private PlayerInputManager _playerManager;

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

    public void ClearAnnouncements()
    {
        _mainUI.ClearAnnouncement();
    }

    private void SetAllPlayersEnabled(bool input)
    {
        foreach(Player player in FindObjectsOfType<Player>())
        {
            player.isEnabled = input;
        }
    }

    [ContextMenu("TestWin")]
    public void YouWin()
    {
        _playerManager.enabled = false;
        ClearAnnouncements();
        DestroyAllPlayers();
        _cameraController.enabled = false;
        SceneManager.LoadScene("YouWin");
    }

    public void GameOver()
    {
        if (CheckIfAllPlayersDied())
        {
            _playerManager.enabled = false;
            ClearAnnouncements();
            DestroyAllPlayers();
            _cameraController.enabled = false;
            SceneManager.LoadScene("GameOver");
        }
    }

    private bool CheckIfAllPlayersDied()
    {
        foreach(Player player in FindObjectsOfType<Player>())
        {
            if (!player.isDead)
            {
                Debug.Log("Not all players are dead yet");
                CreateAnnouncement("You died but your allies will continue for you");
                return false;
            }
        }
        //CreateAnnouncement("All players are dead! Game Over");
        Debug.Log("All players are dead. Ending game");
        return true;
    }

    private void DestroyAllPlayers()
    {
        Player[] players = FindObjectsOfType<Player>();

        for (int i = players.Length - 1; i >= 0; i--)
        {
            Destroy(players[i]);
        }
    }

    public void ClearInstance()
    {
        Instance = null;
    }
}
