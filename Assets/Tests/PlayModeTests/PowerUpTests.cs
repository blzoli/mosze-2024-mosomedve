using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Tests the power-up behavior when the player picks them up.
/// </summary>
public class PowerUpTests
{
    private GameObject player;
    private PlayerController playerController;
    private GameObject healthPowerUpPrefab;
    private GameObject speedPowerUpPrefab;

    [SetUp]
    public void SetUp()
    {
        // Create a player GameObject
        player = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
        playerController = player.GetComponent<PlayerController>();

        // Load the power-up prefabs
        healthPowerUpPrefab = Resources.Load<GameObject>("Prefabs/HealthPickup");
        speedPowerUpPrefab = Resources.Load<GameObject>("Prefabs/SpeedPowerUp");
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.Destroy(player);
    }

    [UnityTest]
    public IEnumerator PlayerPicksUpHealthPowerUp()
    {
        // Set initial health
        PlayerController.health = 5;

        // Instantiate the health power-up
        GameObject healthPowerUp = Object.Instantiate(healthPowerUpPrefab, player.transform.position, Quaternion.identity);
        HealthPowerUp healthPowerUpScript = healthPowerUp.GetComponent<HealthPowerUp>();

        // Simulate collision
        healthPowerUpScript.ApplyEffect(player);

        // Wait for a frame to allow the effect to apply
        yield return null;

        // Check if the player's health increased by the specified amount
        Assert.AreEqual(6, PlayerController.health);

        // Clean up
        Object.Destroy(healthPowerUp);
    }

    [UnityTest]
    public IEnumerator PlayerPicksUpSpeedPowerUp()
    {
        // Set initial speed
        playerController.moveSpeed = 5f;

        // Instantiate the speed power-up
        GameObject speedPowerUp = Object.Instantiate(speedPowerUpPrefab, new Vector3(20f,20f,20f), Quaternion.identity);
        SpeedPowerUp speedPowerUpScript = speedPowerUp.GetComponent<SpeedPowerUp>();

        // Simulate collision
        speedPowerUpScript.ApplyEffect(player);

        // Wait for a frame to allow the effect to apply
        yield return null;

        // Check if the player's speed increased by the specified amount
        Assert.AreEqual(10f, playerController.moveSpeed);

        // Wait for the duration of the speed boost
        yield return new WaitForSeconds(speedPowerUpScript.duration);

        // Check if the player's speed returned to normal
        Assert.AreEqual(5f, playerController.moveSpeed);

        // Clean up
        Object.Destroy(speedPowerUp);
    }
}
