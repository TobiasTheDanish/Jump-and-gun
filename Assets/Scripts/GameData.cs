using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    static public void SaveHighScore(int highScore)
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    static public int LoadHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            int intToLoadTo = PlayerPrefs.GetInt("HighScore");
            return intToLoadTo;
        }
        else
        {
            return 0;
        }
    }

    static public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
    }
}
