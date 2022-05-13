using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject _playerScoreParent;
    [SerializeField] private GameObject _scorecardPrefab;
    [SerializeField] private GameObject _announcementBox;
    [SerializeField] private Text _announcementText;

    private float _timer = 0f;

    #region Functions
    private void Update()
    {
        if(_timer > 0f)
        {
            _announcementBox.SetActive(true);
            _timer -= Time.deltaTime;
        }
        else
        {
            _announcementBox.SetActive(false);
        }
    }
    public void CreatePlayerScorecard(Player player)
    {
        GameObject newCard = Instantiate(_scorecardPrefab, _playerScoreParent.transform);
        PlayerScoreCard scorecard = newCard.GetComponent<PlayerScoreCard>();
        if(player != null)
        {
            scorecard.player = player;
            scorecard.UpdateTextElements();
        }

        GameManager.Instance.CreateAnnouncement("You just started as a new character! Enjoy playing as the " + player.Class.ClassName);
    }

    public void CreateAnnouncement(string text, float duration)
    {
        _announcementText.text = text;
        _timer = duration;
    }

    public void ClearAnnouncement()
    {
        _timer = 0f;
    }
    #endregion
}
