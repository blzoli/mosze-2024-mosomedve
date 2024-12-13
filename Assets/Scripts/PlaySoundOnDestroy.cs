using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnDestroy : MonoBehaviour
{
    public string soundName;

   
    void OnDestroy()
    {
        // play sound if on screen
        if (transform.position.y < Camera.main.transform.position.y + Camera.main.orthographicSize && transform.position.y > Camera.main.transform.position.y - Camera.main.orthographicSize)
            AudioManager.Instance.PlaySound(soundName);
    }

}
