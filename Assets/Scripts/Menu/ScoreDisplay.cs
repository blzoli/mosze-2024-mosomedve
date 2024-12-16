using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

/// <summary>
/// Handles the display of the leaderboard on the screen.
/// </summary>
public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI titleText;  // The title text, e.g., "Leaderboard"
    public GameObject leaderboardPanel; // The panel holding the leaderboard entries
    public GameObject scoreEntryPrefab; // Prefab to display each score entry (with Text fields)

    void Start()
    { 
        StartCoroutine(DisplayLeaderboard());
    }

    /// <summary>
    /// Parses the leaderboard data and displays it on the screen.
    /// </summary>
    private IEnumerator DisplayLeaderboard()
    {
        yield return new WaitForSeconds(0.1f); // Wait for a short time to ensure the leaderboard is loaded
        // Clear the existing leaderboard entries
        foreach (Transform child in leaderboardPanel.transform)
        {
            Destroy(child.gameObject);
        }
        
        Debug.Log("Displaying leaderboard...");
        Debug.Log(ScoreLoader.leaderboard); 

        foreach (var entry in ScoreLoader.leaderboard.leaderboard)
        {
            // Instantiate a new score entry
            GameObject scoreEntry = Instantiate(scoreEntryPrefab, leaderboardPanel.transform);

            // Find and update the Text components in the score entry prefab
            TextMeshProUGUI[] texts = scoreEntry.GetComponentsInChildren<TextMeshProUGUI>();
            texts[0].text = entry.name;        // Name
            texts[1].text = entry.score.ToString(); // Score
            texts[2].text = FormatDateTime(entry.datetime);   // Date/Time
        }
        // if there are less than 10 entries, add empty entries

        if (ScoreLoader.leaderboard.leaderboard.Count < 10)
            {
                for (int i = ScoreLoader.leaderboard.leaderboard.Count; i < 10; i++)
                {
                    GameObject scoreEntry = Instantiate(scoreEntryPrefab, leaderboardPanel.transform);
                    TextMeshProUGUI[] texts = scoreEntry.GetComponentsInChildren<TextMeshProUGUI>();
                    texts[0].text = ""; // Name
                    texts[1].text = ""; // Score
                    texts[2].text = ""; // Date/Time
                }
            }
        }

    /// <summary>
    /// Formats the date and time string to a more readable format.
    /// </summary>
    private string FormatDateTime(string datetime)
    {
        DateTime parsedDateTime;
        if (DateTime.TryParse(datetime, out parsedDateTime))
        {
            // Format the DateTime to a more human-readable format (e.g., "October 12, 2024, 2:30 PM")
            return parsedDateTime.ToString("yyyy-MM-dd HH:mm");
        }
        else
        {
            return "Invalid Date";
        }
    }
}
