using UnityEngine;

/// <summary>
/// Manages audio playback in the game.
/// Implements a singleton pattern to ensure only one instance exists.
/// </summary>

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; ///< Singleton instance

    [System.Serializable]
    /// <summary>
    /// Class to store a clip with volume
    /// </summary>
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
    }

    public Sound[] sounds; ///< Array of sounds to play
    private AudioSource audioSource; ///< The audio source component


    /// <summary>
    /// Sets the singleton instance and ensures it persists between scenes.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        { 
            Destroy(gameObject);
        }

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    /// <summary>
    /// Plays a sound by name.
    /// </summary>
    public void PlaySound(string soundName)
    {
        Sound sound = System.Array.Find(sounds, s => s.name == soundName);
        if (sound != null)
        {
            audioSource.PlayOneShot(sound.clip, sound.volume);
        }
        else
        {
            Debug.LogWarning($"Sound '{soundName}' not found!");
        }
    }
}
