using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


/// <summary>
/// Tests the health bar behavior when the boss takes damage.
/// </summary>
public class HealthBarTest
{
    private GameObject player;
    private GameObject camera;
    private GameObject bossPrefab;
    private GameObject boss;
    private Enemy bossScript;

    [SetUp]
    public void SetUp()
    {
        // Create a player GameObject
        player = new GameObject();
        player.tag = "Player";
        player.transform.position = new Vector3(0, 0, 0);

        AudioManager.Instance = player.AddComponent<AudioManager>();
        player.GetComponent<AudioManager>().sounds = new AudioManager.Sound[0];

        // Create a camera GameObject
        camera = new GameObject();
        camera.AddComponent<Camera>();
        camera.tag = "MainCamera";

        // Load the boss prefab
        bossPrefab = Resources.Load<GameObject>("Prefabs/Boss1");

        // Instantiate the boss from the prefab
        boss = Object.Instantiate(bossPrefab, new Vector3(10, 10, 0), Quaternion.identity);
        bossScript = boss.GetComponent<Enemy>();
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.Destroy(player);
        Object.Destroy(camera);
        Object.Destroy(boss);
    }

    [UnityTest]

    public IEnumerator BossTakesDamageHealthBarDecreases()
    {
        // Wait for a frame to allow the Start method to run
        yield return null;

        GameObject healthBar = boss.GetComponentInChildren<ShowHealthBar>().healthBarPrefab;

        // Store the initial scale of the health bar
        Vector3 initialScale = healthBar.transform.localScale;

        // Apply damage to the boss
        bossScript.TakeDamage(1);

        // Wait for a frame to allow the TakeDamage method to run
        yield return null;

        // Check if the health bar scale has decreased
        Vector3 newScale = healthBar.transform.localScale;
        Assert.Less(newScale.x, initialScale.x);
    }
}
