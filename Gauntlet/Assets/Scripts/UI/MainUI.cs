using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject _playerScoreParent;
    [SerializeField] private GameObject _scorecardPrefab;

    #region Functions
    public void CreatePlayerScorecard(Player player)
    {
        GameObject newCard = Instantiate(_scorecardPrefab, _playerScoreParent.transform);
        PlayerScoreCard scorecard = newCard.GetComponent<PlayerScoreCard>();
        if(player != null)
        {
            scorecard.player = player;
            scorecard.UpdateTextElements();
        }
    }
    #endregion
}
