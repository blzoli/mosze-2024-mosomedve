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
    public float speed = 10f;

    /// <summary>
    /// The maximum life of the asteroid in seconds.
    /// </summary>
    public float maxLife = 3.0f;

    private Vector2 direction = Vector2.down;

    private GameObject sprite; ///< Reference to the asteroid sprite.
    private Vector3 rotationDirection; ///< Direction of rotation.

    void Start()
    {
        sprite = transform.GetChild(0).gameObject;
        rotationDirection = Random.Range(0, 100) > 50 ? Vector3.forward : Vector3.back;
    }

    /// <summary>
    /// Moves the asteroid on frame update.
    /// </summary>

    private void Update()
    {
        if (Game.isPaused)
        {
            return;
        }

        if (transform.position.y < -6) // if asteroid goes off screen destroy it and add score
        {
            Game.AddScore(10);
            Destroy(this.gameObject);
        }

        // rotate the asteroid sprite around its center
        sprite.transform.Rotate(rotationDirection * 100 * Time.deltaTime);


        // Move the asteroid
        transform.Translate(direction * speed * Time.deltaTime);

        // Destroy the asteroid after maxLife seconds
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(1);
            Debug.Log("Player hit!");
            if (Application.isPlaying) Destroy(this.gameObject); // for edit mode test
        }
    }

}
