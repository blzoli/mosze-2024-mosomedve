using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Class responsible for displaying the UI elements in the game.
/// </summary>
public class GameUIDisplay : MonoBehaviour
{
    public GameObject heartPrefab; ///< Prefab for heart icon.
    public Transform healthPanel; ///< Panel to hold heart icons.
    public TextMeshProUGUI stageText; ///< Text to display current stage.
    public Game game; ///< Reference to your Game class instance.
    public PlayerController player; ///< Reference to your PlayerController class instance.
    public GameObject pauseMenu; ///< Reference to the pause menu.
    public GameObject gameOverMenu; ///< Reference to the game over menu.
    public GameObject StartMenu;  ///< Reference to the start menu.
    public GameObject GameUI; ///< Reference to the game UI.
    public GameObject gameCompleteMenu; ///< Reference to the game complete menu.
    public TextMeshProUGUI scoreText; ///< Text to display the player's score.

    /// <summary>
    /// Updates the health display based on the current health value.
    /// </summary>
    /// <param name="currentHealth">The current health value.</param>
    public void UpdateHealth(int currentHealth)
    {
        foreach (Transform child in healthPanel)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentHealth; i++)
        {
            Instantiate(heartPrefab, healthPanel);
        }
    }

    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score;
    }

    /// <summary>
    /// Updates the stage display based on the current stage ID.
    /// </summary>
    /// <param name="stageId">The current stage ID.</param>
    public void UpdateStage(int stageId)
    {
        stageText.text = "Stage: " + stageId;
    }

    /// <summary>
    /// Updates the pause menu display based on the current pause state.
    /// </summary>
    public void UpdatePauseMenu()
    {
        pauseMenu.SetActive(Game.isPaused && !Game.isOver && !Game.isStoryDisplayed);
    }

    /// <summary>
    /// Shows game over screen.
    /// </summary>
    /// 
    public void ShowGameOver()
    {
        if (!Game.isGameComplete) gameOverMenu.SetActive(Game.isOver);
    }


    void Start()
    {
        UpdateHealth(PlayerController.health);
        UpdateStage(game.CurrentStageID);
        UpdatePauseMenu();
        ShowGameOver();
    }

    /// <summary>
    /// Updates the start game start menu display based on the current game state.
    /// </summary>

    public void UpdateStartGameMenu()
    {
        StartMenu.SetActive(!Game.isStarted);
        GameUI.SetActive(Game.isStarted);
    }

    /// <summary>
    /// Updates the start game complete menu display based on the current game state.
    /// </summary>

    public void UpdateGameCompleteMenu()
    {         
        gameCompleteMenu.SetActive(Game.isGameComplete);
    }

    private void Update()
    {
        if (PlayerController.health != healthPanel.childCount)
        {
            UpdateHealth(PlayerController.health);
        }

        if (game.CurrentStageID != int.Parse(stageText.text.Split(' ')[1]))
        {
            UpdateStage(game.CurrentStageID);
        }

        UpdateScore(Game.score);
        UpdatePauseMenu();
        UpdateStartGameMenu();
        ShowGameOver();
        UpdateGameCompleteMenu();
    }
}
