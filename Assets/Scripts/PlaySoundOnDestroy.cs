using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays a sound when the object is destroyed.
/// </summary>
public class PlaySoundOnDestroy : MonoBehaviour
{
    public string soundName;
    
    /// <summary>
    /// Plays the sound when the object is destroyed, if it is on screen.
    /// </summary>
    void OnDestroy()
    {
        // play sound if on screen
        if (transform.position.y < Camera.main.transform.position.y + Camera.main.orthographicSize && transform.position.y > Camera.main.transform.position.y - Camera.main.orthographicSize)
            if (AudioManager.Instance != null) AudioManager.Instance.PlaySound(soundName);
    }

}
