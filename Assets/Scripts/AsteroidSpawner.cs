using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// Spawns asteroids in the game.
/// </summary>
public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroidPrefab;

    public float trajectoryVariance = 15.0f;

    public float spawnRate = 1.50f;
    public float spawnDistance = 1.0f;

    /// <summary>
    /// Starts the asteroid spawning process.
    /// </summary>
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    /// <summary>
    /// Spawns an asteroid at a random position above the player camera.
    /// </summary>
    private void Spawn()
    {
        Vector3 spawnDirection = UnityEngine.Random.insideUnitCircle.normalized * this.spawnDistance;
        Vector3 spawnPosition = new Vector3(Camera.main.transform.position.x + spawnDirection.x, Camera.main.transform.position.y + Camera.main.orthographicSize + this.spawnDistance, this.transform.position.z);

        float variance = UnityEngine.Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

        Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPosition, rotation);
        asteroid.SetTrajectory(Vector2.down);
    }
}
