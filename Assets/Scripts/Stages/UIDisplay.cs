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
    /// 
    public void UpdatePauseMenu()
    {
        pauseMenu.SetActive(Game.isPaused);
    }

    void Start()
    {
        UpdateHealth(player.health);
        UpdateStage(game.CurrentStageID);
        UpdatePauseMenu();
    }

    private void Update()
    {
        if (player.health != healthPanel.childCount)
        {
            UpdateHealth(player.health);
        }

        if (game.CurrentStageID != int.Parse(stageText.text.Split(' ')[1]))
        {
            UpdateStage(game.CurrentStageID);
        }

        UpdatePauseMenu();
    }
}
