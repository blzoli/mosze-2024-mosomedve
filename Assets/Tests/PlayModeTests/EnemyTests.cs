using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EnemyTests
{
    private GameObject player;
    private GameObject enemy;
    private Enemy enemyScript;

    [SetUp]
    public void SetUp()
    {
        // Create a player GameObject
        player = new GameObject();
        player.tag = "Player";
        player.transform.position = new Vector3(0, 0, 0);

        // Create a camera GameObject
        var camera = new GameObject();
        camera.AddComponent<Camera>();
        camera.tag = "MainCamera";

        // Create an enemy GameObject
        enemy = new GameObject();
        enemyScript = enemy.AddComponent<Enemy>();
        enemyScript.speed = 3.0f;
        enemyScript.health = 5;
        enemyScript.attackPower = 10;
        enemyScript.attackRange = 7.0f;
        enemyScript.attackRate = 2.0f;

        // Set the enemy's initial position
        enemy.transform.position = new Vector3(10, 10, 0);
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.Destroy(player);
        Object.Destroy(enemy);
    }

    [UnityTest]
    public IEnumerator EnemyMovesTowardsPlayer()
    {
        // Wait for a frame to allow the Start method to run
        yield return null;

        // Store the initial position of the enemy
        Vector3 initialPosition = enemy.transform.position;

        // Wait for a few frames to allow the enemy to move
        yield return new WaitForSeconds(1.0f);

        // Check if the enemy has moved closer to the player
        Vector3 newPosition = enemy.transform.position;
        Debug.Log("Player Position: " + player.transform.position);
        Debug.Log("Initial Position: " + initialPosition);
        Debug.Log("New Position: " + newPosition);
        Assert.Less(Vector3.Distance(newPosition, player.transform.position), Vector3.Distance(initialPosition, player.transform.position));
    }
}
