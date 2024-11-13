using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Represents an asteroid in the game.
/// </summary>
public class Asteroid : MonoBehaviour
{
    /// <summary>
    /// The speed of the asteroid.
    /// </summary>
    public float speed = 0.5f;

    /// <summary>
    /// The maximum life of the asteroid in seconds.
    /// </summary>
    public float maxLife = 30.0f;

    private Vector2 direction = Vector2.down;


    /// <summary>
    /// Moves the asteroid on frame update.
    /// </summary>

    private void Update()
    {
        if (Game.isPaused)
        {
            return;
        }
        transform.Translate(direction * speed * 0.0004f);
        Destroy(this.gameObject, maxLife);
    }

    /// <summary>
    /// Sets the trajectory of the asteroid.
    /// </summary>
    /// <param name="direction">The direction of the trajectory.</param>
    public void SetTrajectory(Vector2 direction)
    {
        direction = direction.normalized;
    }

    /// <summary>
    /// Checks for collisions with the player.
    /// </summary>

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit!");
        }
    }
}
