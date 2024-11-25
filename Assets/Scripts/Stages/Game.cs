using UnityEngine;
using System.Collections.Generic;
using System.Linq;

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
    public static bool isOver = false; ///< Flag to indicate if the game is over.

    /// @brief Initializes the game and starts the first stage.
    void Start()
    {
        Debug.Log("Game started.");
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
        if (stages == null) Start();
        if (CurrentStageID < stages.Length)
        {
            Stage currentStage = stages[CurrentStageID];
            Debug.Log($"Starting Stage {currentStage.stageID}: {currentStage.story}");

            CurrentStageID++;
        }
        else
        {
            Debug.Log("All stages completed.");
            isOver = true;
        }
    }

    /// @brief Restarts current stage.
    /// 
    /// This method restarts the current stage by resetting the stage ID and player attributes.
    
    public void RestartStage()
    {
        isOver = false;
        TogglePause(false);
        CurrentStageID--;
        PlayerController.ResetPlayer();
        // find all enemies by tag and destroy them
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] eprojectiles = GameObject.FindGameObjectsWithTag("EnemyProjectile");
        GameObject[] pprojectiles = GameObject.FindGameObjectsWithTag("PlayerProjectile");

        foreach (GameObject enemy in enemies.Concat(eprojectiles).Concat(pprojectiles))
        {
            Destroy(enemy);
        }
        
        StartNextStage();
    }

    /// @brief Pauses the game.
    /// 
    /// This method pauses the game by setting the timescale to 0.

    public static void TogglePause(bool pause)
    {
        isPaused = pause;
        Time.timeScale = isPaused ? 0 : 1;
    }

    /// @brief Game over method.
    /// 
    /// This method is called when the game is over. Pauses time and shows the game over screen.
    /// 

    public static void GameOver()
    {
        isOver = true;
        TogglePause(true); 
    }

    /// @brief Handles the game update loop.

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause(!isPaused);
        }
        if (isOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartStage();
            }
        }
    }

    public static void ResetGameState()
    {
        isOver = false;
        TogglePause(false);
    }

}
