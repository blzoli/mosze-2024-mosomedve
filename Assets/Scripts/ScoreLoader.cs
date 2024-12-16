using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
/// @brief Represents a single entry in the leaderboard.
public class LeaderboardEntry
{
    public string name;
    public int score;
    public string datetime;
}

[System.Serializable]
/// @brief Represents the leaderboard as a whole.
public class Leaderboard
{
    public List<LeaderboardEntry> leaderboard = new List<LeaderboardEntry>(10); // Max 10 entries
}

/// @brief Loads and saves the leaderboard data to a file.
public class ScoreLoader : MonoBehaviour
{
    private static string filePath;  ///< Path to the file where the scores are saved

    public static Leaderboard leaderboard;  ///< The leaderboard data
    
    /// @brief Load the scores from the file when the script is enabled.
    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "scores.json");
    }

    /// @brief Lead when the script is enabled.
    public void OnEnable()
    {
        LoadOrCreateScores();
    }

    /// @brief Load or create the scores file.
    private void LoadOrCreateScores()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            leaderboard = JsonUtility.FromJson<Leaderboard>(json);
            Debug.Log("Scores loaded successfully!");
        }
        else
        {
            CreateEmptyScores();
        }
    }

    /// @brief Create an empty scores file.
    private void CreateEmptyScores()
    {
        leaderboard = new Leaderboard(); // Empty leaderboard
        string json = JsonUtility.ToJson(leaderboard, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Empty scores file created!");
    }

    /// @brief Check if the score is high enough to be added to the leaderboard (top 10).
    public static bool CheckIfScoreHighEnough(int score)
    {
        // If there are less than 10 scores, it's always high enough to be added
        if (leaderboard.leaderboard.Count < 10)
        {
            return true;
        }

        // Check if the score is higher than the lowest score in the leaderboard
        int lowestScore = leaderboard.leaderboard[leaderboard.leaderboard.Count - 1].score;
        return score > lowestScore;
    }

    /// @brief Add a new score to the leaderboard.
    public static void AddScore(string name, int score)
    {
        // Check if the score qualifies for the leaderboard
        if (CheckIfScoreHighEnough(score))
        {
            // Create a new leaderboard entry
            LeaderboardEntry newEntry = new LeaderboardEntry
            {
                name = name,
                score = score,
                datetime = System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")
            };

            // Add the new entry to the leaderboard
            leaderboard.leaderboard.Add(newEntry);

            // Sort the leaderboard by score in descending order
            leaderboard.leaderboard.Sort((a, b) => b.score.CompareTo(a.score));

            // Ensure only the top 10 scores are kept
            if (leaderboard.leaderboard.Count > 10)
            {
                leaderboard.leaderboard.RemoveAt(leaderboard.leaderboard.Count - 1);
            }

            // Save the updated leaderboard to the file
            string json = JsonUtility.ToJson(leaderboard, true);
            File.WriteAllText(filePath, json);
            Debug.Log("New score added and leaderboard updated!");
        }
        else
        {
            Debug.Log("Score is not high enough to be added to the leaderboard.");
        }
    }
}
