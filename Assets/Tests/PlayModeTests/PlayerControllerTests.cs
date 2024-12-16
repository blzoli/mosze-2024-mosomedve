using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Tests the following: Player takes damage, player health resets, player health increases, player fires weapon.
/// </summary>
public class PlayerControllerTests
{
    private GameObject player;
    private PlayerController playerController;
    private GameObject camera;

    [SetUp]
    public void SetUp()
    {
        // Create a new GameObject and instantiate a copy of the Player prefab
        player = Object.Instantiate(Resources.Load<GameObject>("Prefabs/Player"));
        playerController = player.GetComponent<PlayerController>();

        // Add a SpriteRenderer component to calculate player width and height
        player.AddComponent<SpriteRenderer>();

        AudioManager.Instance = player.AddComponent<AudioManager>();
        player.GetComponent<AudioManager>().sounds = new AudioManager.Sound[0];

        // Create a mock camera
        camera = new GameObject();
        camera.AddComponent<Camera>();
        camera.tag = "MainCamera"; // Set the tag to MainCamera so Camera.main can find it

        // Set up the initial conditions for the test
        playerController.moveSpeed = 5f; // Set the player's movement speed
        playerController.weapon = null; // No weapon equipped initially
        PlayerController.health = 5; // Reset health to default value
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.Destroy(player);
        Object.Destroy(camera);
    }


    [UnityTest]
    public IEnumerator PlayerTakesDamage()
    {
        // Damage the player
        playerController.TakeDamage(1);

        // Wait for a frame to allow the TakeDamage method to run
        yield return null;

        // Check if the player's health has decreased
        Assert.AreEqual(4, PlayerController.health);
    }

    [UnityTest]
    public IEnumerator PlayerHealthResets()
    {
        // Reset the player's health
        PlayerController.ResetPlayer();

        // Wait for a frame to allow the ResetPlayer method to run
        yield return null;

        // Check if the player's health has reset to the default value
        Assert.AreEqual(5, PlayerController.health);
    }

    [UnityTest]
    public IEnumerator PlayerIncreasesHealth()
    {
        // Increase the player's health
        playerController.IncreaseHealth(1);

        // Wait for a frame to allow the IncreaseHealth method to run
        yield return null;

        // Check if the player's health has increased
        Assert.AreEqual(6, PlayerController.health);
    }

    [UnityTest]
    public IEnumerator PlayerFiresWeapon()
    {
        // Create a mock weapon and equip it
        var weapon = new GameObject().AddComponent<MockWeapon>();
        playerController.PickUpWeapon(weapon);

        // Wait for a frame to allow the Update method to run
        yield return null;

        playerController.GetComponent<PlayerController>().weapon.Fire(player);

        yield return null;

        // Check if the weapon has fired
        Assert.IsTrue(weapon.HasFired);
    }

    private class MockWeapon : Weapon
    {
        public bool HasFired { get; private set; }

        public override void Fire(GameObject caller)
        {
            HasFired = true;
        }
    }

}