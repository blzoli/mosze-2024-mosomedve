using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  
    public float spawnInterval = 10.0f; 
    public float spawnRangeX = 5.0f; 
    public float spawnPositionY = 6.0f; 

    private void Start()
    {
        StartCoroutine(SpawnEnemyRoutine()); // spawn routine
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)  // folyamatos megjelenesi ciklus
        {
            yield return new WaitForSeconds(spawnInterval); // varakozas a kovetkezo megjelenesig
            SpawnEnemy();  // ellenseg letrehozasa
        }
    }

    private void SpawnEnemy()
    {
        // x pozicio generalasa kepernyon kivul
        Vector2 spawnPosition = new Vector2(
            Random.Range(-spawnRangeX, spawnRangeX), // veletlenszeru x
            spawnPositionY // fix y a kepernyo tetejen
        );

        // ellenseg peldanyositasa
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
