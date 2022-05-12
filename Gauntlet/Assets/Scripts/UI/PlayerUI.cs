using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _classSelectionMenu;
    [SerializeField] private GameObject _pauseMenu;

    /*
    [Header("Class References")]
    [SerializeField] private PlayerClassData _warrior;
    [SerializeField] private PlayerClassData _valkyrie;
    [SerializeField] private PlayerClassData _wizard;
    [SerializeField] private PlayerClassData _archer;
    */

    #region GenericFunctions
    private void Awake()
    {
        DisableAllMenus();
    }

    private void DisableAllMenus()
    {
        if (_classSelectionMenu)
            _classSelectionMenu.SetActive(false);

        if (_pauseMenu)
            _pauseMenu.SetActive(false);
    }
    #endregion

    #region PauseMenuFunctions
    public void PauseGame()
    {
        DisableAllMenus();

        if (_pauseMenu)
            _pauseMenu.SetActive(true);

        GameManager.Instance.PauseGameTime(true);
    }

    public void ResumeGame()
    {
        DisableAllMenus();
        GameManager.Instance.PauseGameTime(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    #endregion

    #region ClassSelectFunctions
    [ContextMenu("OpenClassSelect")]
    public void OpenClassSelectMenu()
    {
        if (_classSelectionMenu)
            _classSelectionMenu.SetActive(true);

        GameManager.Instance.PauseGameTime(true);
    }

    public void SetClass(PlayerClassData data)
    {
        _player.SetClass(data);
        DisableAllMenus();
        GameManager.Instance.PauseGameTime(false);
    }
    #endregion
}
