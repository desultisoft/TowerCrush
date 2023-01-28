using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameOverEffects;
    public GameObject gameOverUI;
    public GameObject gameWinUI;
    public GameObject gameStatsUI;
    public CameraScript cameraScript;

    public static bool gameOver { private set; get; }

    public void Start()
    {
        instance = this;
    }
    public void Restart()
    {
        gameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            StartCoroutine(GameOverRoutine());
        }
       
    }
    public void GameWin()
    {
        if (!gameOver)
        {
            gameOver = true;
            gameWinUI.SetActive(true);
        }
    }

    private IEnumerator GameOverRoutine()
    {

        gameOverEffects.SetActive(true);

        

        yield return new WaitForSecondsRealtime(0.5f);

        cameraScript.enabled = false;
        gameOverUI.SetActive(true);
        gameStatsUI.SetActive(false);
    }
}
