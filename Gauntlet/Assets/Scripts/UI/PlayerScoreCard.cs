using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreCard : MonoBehaviour
{
    [Header("Associated Player")]
    public Player player;

    [Header("UIReferences")]
    [SerializeField] private Text _classText;
    [SerializeField] private Text _healthText, _livesText, _scoreText, _potionsText, _keysText;

    private void Update()
    {
        if(player != null && player.gameObject.activeInHierarchy)
        {
            UpdateTextElements();
        }
    }

    public void UpdateTextElements()
    {
        _classText.text = player.Class.ClassName;
        _healthText.text = "Health: " + player.Health;
        _livesText.text = "Lives: " + player.Lives;
        _scoreText.text = "Score: " + player.Score;
        _potionsText.text = "Potions: " + player.PotionCount;
        _keysText.text = "Keys: " + player.KeysCount;
    }
}
