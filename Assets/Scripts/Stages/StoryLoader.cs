using UnityEngine;
using System.Collections.Generic;

/// @class StoryLoader
/// @brief Loads story data from a JSON file and creates stages based on that data.
///
/// This class is responsible for loading the story JSON file located in the Resources
/// folder, parsing the JSON data into a structured format, and managing the game stages
/// associated with the story. Each stage contains a story segment that can be utilized
/// in the game.
///
/// @note Ensure the JSON file is correctly formatted and located in the Resources folder.
public static class StoryLoader
{
    /// @brief Holds the parsed story data.
    public static Story story;


    /// @brief Loads the story from a JSON file.
    ///
    /// This method attempts to load the `story.json` file from the Resources folder
    /// and parse it into the Story object. If the file is not found, it logs an error
    /// message.
    public static void LoadStory()
    {
        // Load the JSON file
        TextAsset jsonText = Resources.Load<TextAsset>("story");

        if (jsonText != null)
        {
            // Parse the JSON to the Story object
            story = JsonUtility.FromJson<Story>(jsonText.text);
        }
        else
        {
            Debug.LogError("Could not find story.json in Resources.");
        }
    }
    /// @brief Creates stages based on the loaded story data.
    ///
    /// This method returns an array of stages from the loaded story.
    public static Stage[] CreateStages()
    {
        return story.plot.ToArray(); // Convert the List<Stage> to Stage[]
    }
}

/// @class Story
/// @brief Represents the entire story structure.
///
/// This class contains a list of stages that make up the complete story.
[System.Serializable]
public class Story
{
    /// @brief List of stages in the story.
    public List<Stage> plot; ///< The stages that comprise the story.
}
