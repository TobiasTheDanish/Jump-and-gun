using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDiedPanel : MonoBehaviour
{
    [SerializeField] private Text accuracyText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Player player;
    [SerializeField] private GameManager gameManager;

    private void OnEnable()
    {
        accuracyText.text = $"Accuracy: {player.Accuracy*100}%";
        scoreText.text = $"Score: {player.CalculateScore()}";
        highScoreText.text = $"Highscore: {gameManager.GetHighScore()}";
    }
}
