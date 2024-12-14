using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AsteroidCollision
{
    // A Test behaves as an ordinary method
    [Test]
    public void AsteroidCollisionSimplePasses()
    {
        // Set up player object
        PlayerController player = new PlayerController();
        GameObject playerObject = new GameObject("Player");
        playerObject.tag = "Player";
        playerObject.AddComponent<PlayerController>();
        playerObject.AddComponent<BoxCollider2D>();

        PlayerController.health = 5;

        // Set up an asteroid object
        Asteroid asteroid = new Asteroid();
        GameObject asteroidObject = new GameObject();
        asteroidObject.tag = "Asteroid";
        asteroidObject.AddComponent<Asteroid>();

        // Collide the asteroid with the player
        asteroid.OnTriggerEnter2D(playerObject.GetComponent<Collider2D>());

        Assert.AreEqual(PlayerController.health, 4);

        asteroid.OnTriggerEnter2D(playerObject.GetComponent<Collider2D>());

        Assert.AreEqual(PlayerController.health, 3);

    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator AsteroidCollisionWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
