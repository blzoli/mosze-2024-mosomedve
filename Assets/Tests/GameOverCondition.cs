using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;

/// <summary>
/// Represents the class for testing the game over condition.
/// </summary>
public class GameOverCondition
{
    // A Test behaves as an ordinary method
    [Test]
    /// <summary>
    /// Tests the game over condition. When the player's health reaches zero, the game should be over.
    /// </summary>
    public void GameOverConditionSimplePasses()
    {
        Game.isOver = false;

        PlayerController player = new PlayerController();

        PlayerController.health = 5;

        Assert.AreEqual(PlayerController.health, 5);
        Assert.AreEqual(Game.isOver, false);

        player.TakeDamage(3);

        Assert.AreEqual(PlayerController.health, 2);
        Assert.AreEqual(Game.isOver, false);

        player.TakeDamage(3);

        Assert.AreEqual(Game.isOver, true);

        // Reset static variables
        Game.ResetGameState();

    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator GameOverConditionWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

}
