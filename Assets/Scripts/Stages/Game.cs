using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

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

    public static bool isStarted = false; ///< Flag to indicate if the game has started.
    public static bool isPaused = false; ///< Flag to indicate if the game is paused.
    public static bool isOver = false; ///< Flag to indicate if the game is over.
    public static bool isGameComplete = false; ///< Flag to indicate if the game is complete.

    public static bool isStoryDisplayed = false; ///< Flag to indicate if the story is displayed.

    public static int score = 0; ///< The player's score.

    public GameObject playerObject; ///< Reference to the player GameObject.

    /// @brief Reference to the Asteroid prefab.
    public GameObject asteroidPrefab;

    /// @brief Reference to the Enemy prefab.
    public GameObject enemyPrefab;

    /// @brief Reference to the UI Text element for displaying the story.
    public GameObject storyText;

    /// @brief Boss prefabs
    public GameObject[] bosses;
    /// @brief Reference to high score tag input
    public TMPro.TMP_InputField highScoreTag;

    /// @brief Adds points to the player's score.
    public static void AddScore(int points)
    {
        score += points;
    }

    /// @brief Initializes the game and starts the first stage.
    void Start()
    {
        Debug.Log("Game started.");
        StoryLoader.LoadStory(); // Load the story from the JSON file.
        stages = StoryLoader.CreateStages(); // Get the stages from the StoryLoader.

        CurrentStageID = 0; // Initialize to the first stage.
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
            currentStage.Start(this); // Pass the Game instance

            CurrentStageID++;
        }
        else
        {
            if (Application.isPlaying) AudioManager.Instance.PlaySound("gameComplete");
            CleanUpScene();
            Debug.Log("All stages completed.");
            isOver = true;
            isGameComplete = true;

        }
    }

    /// @brief Spawns asteroids for a specified duration.

    public void SpawnAsteroids(float duration, Stage stage)
    {
        StartCoroutine(SpawnAsteroidsCoroutine(duration, stage));
    }

    /// @brief Coroutine to spawn asteroids for a specified duration.

    private IEnumerator SpawnAsteroidsCoroutine(float duration, Stage stage)
    {
        float endTime = Time.time + duration;

        AsteroidSpawner spawner = GetComponentInParent<AsteroidSpawner>();
        if (spawner != null)
        {
            spawner.enabled = true; // Enable the AsteroidSpawner script
        }

        while (Time.time < endTime)
        {
            yield return new WaitForSeconds(1.0f); // Adjust spawn rate as needed 
        }

        if (spawner != null)
        {
            spawner.enabled = false; // Disable the AsteroidSpawner script
        }

        yield return new WaitForSeconds(2.0f); // Wait for any remaining asteroids to clear

        stage.Complete();
    }

    /// @brief Spawns a specified number of enemies.

    public void SpawnEnemies(int count, Stage stage)
    {
        StartCoroutine(SpawnEnemiesCoroutine(count, stage));
    }

    /// @brief Coroutine to spawn a specified number of enemies.

    private IEnumerator SpawnEnemiesCoroutine(int count, Stage stage)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(enemyPrefab, GetRandomPosition(), Quaternion.identity);
            yield return null; // Wait for the next frame to continue spawning
        }

        // Wait until all enemies are destroyed
        while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            yield return new WaitForSeconds(1.0f); // Check every second
        }

        // spawn boss when all enemies are destroyed

        // if stageID/2 is larger than the size of the bosses array, spawn the first two bosses, else spawn the boss at the index of stageID/2

        if (CurrentStageID / 2 > bosses.Length)
        {
            Instantiate(bosses[0], GetRandomPosition(), Quaternion.identity);
            Instantiate(bosses[1], GetRandomPosition(), Quaternion.identity);
        }
        else
        {
            Instantiate(bosses[CurrentStageID / 2 - 1], GetRandomPosition(), Quaternion.identity);
        }

        if (Application.isPlaying) AudioManager.Instance.PlaySound("bossEnter");

        // Wait until all bosses are destroyed
        while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
        {
            yield return new WaitForSeconds(1.0f); // Check every second
        }


        stage.Complete();
    }

    /// @brief Gets a random position for spawning objects.

    private Vector2 GetRandomPosition()
    {
        Vector2 spawnPosition = new Vector2(
            Random.Range(-5, 5), // veletlenszeru x
            6 // fix y a kepernyo tetejen
        );
        return spawnPosition;
    }

    /// @brief Cleans up the scene by destroying all enemies and projectiles.
    public static void CleanUpScene()
    {
        // find all enemies by tag and destroy them
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] eprojectiles = GameObject.FindGameObjectsWithTag("EnemyProjectile");
        GameObject[] pprojectiles = GameObject.FindGameObjectsWithTag("PlayerProjectile");
        GameObject[] weaponPickups = GameObject.FindGameObjectsWithTag("WeaponPickup");
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("PowerUp");

        if (Application.isPlaying)
        { 
            foreach (GameObject enemy in enemies)
            { 
                // disable drop item on death
                if (enemy.GetComponent<DropItemOnDeath>())
                {
                    enemy.GetComponent<DropItemOnDeath>().enabled = false;
                }
            }
            foreach (GameObject enemy in enemies.Concat(eprojectiles).Concat(pprojectiles).Concat(weaponPickups).Concat(powerups))
            {
                Destroy(enemy);
            }
         }
    }

    /// @brief Restarts current stage.
    /// 
    /// This method restarts the current stage by resetting the stage ID and player attributes.
    public void RestartStage()
    {
        CleanUpScene();
        score = 0;

        TogglePause(false);
        CurrentStageID--;
        PlayerController.ResetPlayer();
     
        isOver = false;

        if (isGameComplete)
        {
            isGameComplete = false;
            CurrentStageID = 0;  
            for (int i = 0; i < stages.Length; i++)
            {
                stages[i].Reset();
            }
            isStarted = false;
            SceneManager.LoadScene("MenuScene");


        } else StartNextStage();
    }

    /// @brief Pauses the game.
    /// 
    /// This method pauses the game by setting the timescale to 0.
    public static void TogglePause(bool pause)
    {
        isPaused = pause;
        Time.timeScale = isPaused ? 0 : 1;

        if (Application.isPlaying) AudioManager.Instance.PlaySound("menuSound");
    }

    /// @brief Game over method.
    /// 
    /// This method is called when the game is over. Pauses time and shows the game over screen.
    public static void GameOver()
    {
        CleanUpScene();
        isOver = true;
        TogglePause(true);
        if (Application.isPlaying) AudioManager.Instance.PlaySound("gameOver");
    }

    /// @brief Handles the game update loop.
    void Update()
    {
        if (isStoryDisplayed && Input.GetKeyDown(KeyCode.R)) // Hide the story text and continue the game
        {
            storyText.SetActive(false);
            isStoryDisplayed = false;
            TogglePause(false);
            StartNextStage();
            return;
        }

        if (!isStarted) // Start the game
        {
        if (Input.GetKey(KeyCode.R))
            {
                StartNextStage(); // Start the first stage.
                
                playerObject.GetComponent<PlayerController>().enabled = true;

                isStarted = true;
            }
        }
        if (isStarted && Input.GetKeyDown(KeyCode.Escape)) // Pause the game
        {
            TogglePause(!isPaused);
        }
        if (!isGameComplete && isPaused && !isStoryDisplayed) // game is paused
        { 
            if (Input.GetKeyDown(KeyCode.R) && !isOver) // exit to menu on pause
            {
                score = 0;
                TogglePause(!isPaused);
                CurrentStageID = 0;
                for (int i = 0; i < stages.Length; i++)
                {
                    stages[i].Reset();
                }
                isStarted = false;
                SceneManager.LoadScene("MenuScene");
            }
            if (Input.GetKeyDown(KeyCode.Q)) // quit the game on pause
            {
                Application.Quit();
            }
        }
        if (isOver) // restart game
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartStage();
            }
        }
        if (isGameComplete) // add high score
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                AddHighScore();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("MenuScene");
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }
        }
    }

    /// @brief Resets the game state.

    public static void ResetGameState()
    {
        isOver = false;
        TogglePause(false);
    }

    /// @brief Displays the story text and pauses the game.
    ///
    /// This method is called when a stage is completed to show the story text
    /// and pause the game until the player presses R to continue.
    public void DisplayStory(string story)
    {

        // destroy remaining projectiles. Player might run into them after the story is displayed
        GameObject[] eprojectiles = GameObject.FindGameObjectsWithTag("EnemyProjectile");

        foreach (GameObject eprojectile in eprojectiles)
        {
            Destroy(eprojectile);
        }


        isStoryDisplayed = true;
        storyText.transform.Find("story").GetComponent<TMPro.TextMeshProUGUI>().text = story;
        storyText.SetActive(true);
        if (isGameComplete)
        {
            StartNextStage();
        }
        else TogglePause(true);
    }

    /// @brief Adds the player's score to the leaderboard, if it is high enough.
    public void AddHighScore() 
    {
        string tag = highScoreTag.text;
        if (tag.Length != 3) return;
        if (ScoreLoader.CheckIfScoreHighEnough(score))
        {
            ScoreLoader.AddScore(tag, score);
            SceneManager.LoadScene("MenuScene");
        }
    }

}