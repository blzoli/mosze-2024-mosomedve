using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Spawns asteroids in the game.
/// </summary>
public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroidPrefab;

    public float trajectoryVariance = 2.0f;

    public float spawnRate = 1.50f;
    public float spawnDistance = 7.0f;

    /// <summary>
    /// Stops the asteroid spawning process when the script is disabled.
    /// </summary>
    private void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    /// <summary>
    /// Starts the asteroid spawning process when the script is enabled.
    /// </summary>
    private void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }


    /// <summary>
    /// Spawns an asteroid at a fixed distance above the player camera.
    /// </summary>
    private void Spawn()
    {
        float leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane)).x;
        float rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane)).x;
        float randomX = UnityEngine.Random.Range(leftEdge, rightEdge);

        Vector3 spawnPosition = new Vector3(randomX, Camera.main.transform.position.y + this.spawnDistance, this.transform.position.z);

        float variance = UnityEngine.Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

        Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPosition, rotation);
        Vector2 direction = (Camera.main.transform.position - spawnPosition).normalized;
        asteroid.SetTrajectory(direction);
    }
}
