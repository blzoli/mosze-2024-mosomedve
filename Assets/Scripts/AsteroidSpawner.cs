using Unity.Mathematics;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public Asteroid asteroidPrefab;

    public float trajectoryVariance = 15.0f;

    public float spawnRate = 1.50f;
    public float spawnDistance = 10.0f;

    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating(nameof(Spawn), this.spawnRate, this.spawnRate);
    }

    private void Spawn()
    {
        Vector3 spawnDirection = UnityEngine.Random.insideUnitCircle.normalized * this.spawnDistance;
        Vector3 spawnPosition = this.transform.position + spawnDirection;

        float variance = UnityEngine.Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

        Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPosition, rotation);
        asteroid.SetTrajectory(rotation * -spawnDirection);
    }

    

}
