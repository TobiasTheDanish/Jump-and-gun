using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform spawnpoint;
    [SerializeField] GameObject playerDiedPanel;

    private int highScore = 0;

    private void Start()
    {
        highScore = GameData.LoadHighScore();
    }

    public void RespawnPlayer(GameObject player)
    {
        player.transform.position = spawnpoint.position;
        Player instance = player.GetComponentInParent<Player>();
        if (instance != null)
        {
            instance.TakeDamage(50f);
        }
    }

    public void PlayerDied(GameObject player)
    {
        SetHighScore(player.GetComponent<Player>().CalculateScore());
        playerDiedPanel.SetActive(true);
        Time.timeScale = 0;
        player.SetActive(false);
    }

    private void SetHighScore(int score)
    {
        if (highScore < score)
        {
            highScore = score;
            GameData.SaveHighScore(highScore);
        }
        else
        {
            highScore = GameData.LoadHighScore();
        }
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public void ResetHighScore()
    {
        GameData.ResetHighScore();
    }
}
