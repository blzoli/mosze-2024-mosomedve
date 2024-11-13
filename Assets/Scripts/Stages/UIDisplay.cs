using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUIDisplay : MonoBehaviour
{
    [Header("Health Display Settings")]
    public GameObject heartPrefab; // Prefab for heart icon
    public Transform healthPanel;   // Panel to hold heart icons

    [Header("Stage Display Settings")]
    public TextMeshProUGUI stageText; // Text to display current stage

    public Game game; // Reference to your Game class instance
    public PlayerController player; // Reference to your PlayerController class instance

    void Start()
    {
        // Initialize health display
        UpdateHealth(player.health);

        // Initialize stage display
        UpdateStage(game.CurrentStageID);
    }

    private void Update()
    {
        // Update health display if player health has changed
        if (player.health != healthPanel.childCount)
        {
            UpdateHealth(player.health);
        }

        // Update stage display if current stage ID has changed
        if (game.CurrentStageID != int.Parse(stageText.text.Split(' ')[1])) // Assumes format is "Stage: X"
        {
            UpdateStage(game.CurrentStageID);
        }
    }

    // Method to update player health display
    public void UpdateHealth(int currentHealth)
    {
        // Clear existing hearts
        foreach (Transform child in healthPanel)
        {
            Destroy(child.gameObject);
        }

        // Instantiate hearts up to the current health value
        for (int i = 0; i < currentHealth; i++)
        {
            Instantiate(heartPrefab, healthPanel);
        }
    }

    // Method to update the stage display
    public void UpdateStage(int stageId)
    {
        stageText.text = "Stage: " + stageId;
    }
}