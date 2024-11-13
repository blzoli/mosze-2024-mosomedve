using UnityEngine;
using System.Collections.Generic;

/// @class Game
/// @brief Manages the overall game state and progression through stages.
///
/// This class holds a list of stages, tracks the current stage ID,
/// and provides methods to start the next stage. It handles the flow of the game
/// by determining when to advance to the next stage based on player actions.
public class Game : MonoBehaviour
{
    /// @brief List of stages in the game.
    public Stage[] stages; ///< Array of stages that make up the game.

    /// @brief The ID of the currently active stage.
    public int CurrentStageID { get; private set; } ///< Current stage ID.


    public static bool isPaused = false; ///< Flag to indicate if the game is paused.

    /// @brief Initializes the game and starts the first stage.
    void Start()
    {
        StoryLoader.LoadStory(); // Load the story from the JSON file.
        stages = StoryLoader.CreateStages(); // Get the stages from the StoryLoader.

        CurrentStageID = 0; // Initialize to the first stage.
        StartNextStage(); // Start the first stage.
    }

    /// @brief Starts the next stage in the game.
    ///
    /// This method checks if there are more stages to play.
    /// If so, it activates the next stage based on the current stage ID.
    public void StartNextStage()
    {
        if (CurrentStageID < stages.Length)
        {
            Stage currentStage = stages[CurrentStageID];
            Debug.Log($"Starting Stage {currentStage.stageID}: {currentStage.story}");

            CurrentStageID++;
        }
        else
        {
            Debug.Log("All stages completed.");
        }
    }

    /// @brief Pauses the game.
    /// 
    /// This method pauses the game by setting the timescale to 0.

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;

        // TODO: Show or hide pause menu
    }

    /// @brief Handles the game update loop.

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

}
