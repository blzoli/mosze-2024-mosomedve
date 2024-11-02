using UnityEngine;
using TMPro; // Include this for TextMeshPro functionality

/// @class StageDisplay
/// @brief This script displays the current stage ID on the screen.
public class StageDisplay : MonoBehaviour
{
    public Game game; // Reference to your Game class instance
    public TextMeshProUGUI textMeshPro; // Reference to the TextMeshPro component

    /// @brief Start is called before the first frame update.
    void Start()
    {
        UpdateStageText(); // Initialize text display
    }

    /// @brief Update is called once per frame.
    void Update()
    {
        // Update the text only if the currentStageId has changed
        if (game.CurrentStageID != int.Parse(textMeshPro.text.Split(' ')[3])) // Assumes format is "Current Stage ID: X"
        {
            UpdateStageText();
        }
    }

    /// @brief Updates the displayed stage ID text.
    private void UpdateStageText()
    {
        textMeshPro.text = "Current Stage ID: " + game.CurrentStageID;
    }
}
