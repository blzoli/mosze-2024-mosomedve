using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main menu script to handle button clicks.
/// </summary>

public class MainMenuController : MonoBehaviour
{
    /// Load the game scene.
    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    /// Quit the application.
    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
