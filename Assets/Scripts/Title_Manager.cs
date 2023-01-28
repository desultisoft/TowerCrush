using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Manager : MonoBehaviour
{
    public void CreditsPage()
    {
        SceneManager.LoadScene("Credits");
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }
}
