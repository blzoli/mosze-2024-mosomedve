using UnityEngine;

/// @class Stage
/// @brief Represents a single stage in the story.
///
/// This class contains information about a specific stage, including its ID and
/// the story segment associated with it. It also provides methods to handle 
/// the completion and failure of the stage.
[System.Serializable]
public class Stage
{
    /// @brief Unique identifier for the stage.
    public int stageID; ///< The ID of the stage.

    /// @brief The story content for the stage.
    public string story; ///< The story text associated with this stage.

    /// @brief Indicates whether the stage has been completed.
    private bool isCompleted; ///< Tracks the completion state of the stage.

    /// @brief Reference to the Game object to progress stages.
    private Game game;

    /// @brief Completes the stage and triggers any relevant events.
    ///
    /// This method is called when the stage objectives have been met.
    public void Complete()
    {
        if (!isCompleted)
        {
            isCompleted = true;
            Debug.Log($"Stage {stageID} completed.");
            game.StartNextStage();
        }
    }

    /// @brief Fails the stage and handles failure events.
    ///
    /// This method is called when the stage objectives are not met
    /// or the player fails the stage.
    public void Fail()
    {
        if (!isCompleted)
        {
            Debug.Log($"Stage {stageID} failed.");
        }
    }

    public void Start(Game game)
    {
        Debug.Log($"Starting Stage {stageID}: {story}");
        this.game = game;

        if (stageID % 2 == 1)
        {
            // Odd stageID: Spawn asteroids for stageID * 5 seconds
            game.SpawnAsteroids(stageID * 3, this);
        }
        else
        {
            // Even stageID: Spawn an amount of enemies equal to stageID
            game.SpawnEnemies(stageID, this);
        }
    }

    public void Reset()
    {
        isCompleted = false;
    }
}