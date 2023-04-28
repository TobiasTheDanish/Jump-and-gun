using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private void Start()
    {
        Scene activeScene = SceneManager.GetActiveScene();

        if (AudioManager.Instance.IsPlaying(activeScene.buildIndex))
        {
            return;
        }

        if (activeScene.buildIndex == 0)
        {
            if(AudioManager.Instance.IsPlaying(activeScene.buildIndex + 1))
                AudioManager.Instance.StopPlaying(activeScene.buildIndex + 1);

            AudioManager.Instance.Play(activeScene.buildIndex);
        }
        else
        {
            AudioManager.Instance.IsPlaying(activeScene.buildIndex - 1);
                AudioManager.Instance.StopPlaying(activeScene.buildIndex - 1);

            AudioManager.Instance.Play(activeScene.buildIndex);
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("StartMenu");
    }
}
