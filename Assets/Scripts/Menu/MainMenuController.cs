using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main menu script to handle button clicks.
/// </summary>

public class MainMenuController : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject LeaderboardPanel;

    /// Load the game scene.
    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ShowLeaderBoard() 
    { 
        LeaderboardPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    public void HideLeaderBoard()
    {
        LeaderboardPanel.SetActive(false);
        MainMenuPanel.SetActive(true);
    }

    /// Quit the application.
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
