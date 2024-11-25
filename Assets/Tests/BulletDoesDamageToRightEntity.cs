using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

/// <summary>
/// Test class for checking if the bullet does damage to the right entity.
/// </summary>
public class BulletDoesDamageToRightEntity
{
    // A Test behaves as an ordinary method
    [Test]
    public void BulletDoesDamageToRightEntitySimplePasses()
    {
        // initialize bullet
        GameObject bulletObj = new GameObject();
        Bullet bullet = bulletObj.AddComponent<Bullet>();
        bulletObj.AddComponent<BoxCollider2D>().isTrigger = true;
        bullet.damage = 10;

        // initialize player
        GameObject playerObj = new GameObject();
        PlayerController player = playerObj.AddComponent<PlayerController>();
        playerObj.AddComponent<BoxCollider2D>().isTrigger = true;

        PlayerController.health = 100;
        playerObj.tag = "Player";

        // initialize enemy
        GameObject enemyObj = new GameObject();
        Enemy enemy = enemyObj.AddComponent<Enemy>();
        enemyObj.AddComponent<BoxCollider2D>().isTrigger = true;

        enemy.health = 100;
        enemyObj.tag = "Enemy";

        // player bullet test

        bulletObj.tag = "PlayerProjectile";

        bullet.OnTriggerEnter2D(enemyObj.GetComponent<Collider2D>());
        Assert.AreEqual(enemy.health, 90); // enemy takes damage from player bullet

        bullet.OnTriggerEnter2D(player.GetComponent<Collider2D>());
        Assert.AreEqual(PlayerController.health, 100); // player does not take damage from player bullet

        // enemy bullet test  

        bulletObj.tag = "EnemyProjectile";
        bullet.OnTriggerEnter2D(player.GetComponent<Collider2D>());
        Assert.AreEqual(PlayerController.health, 90); // player takes damage from enemy bullet

        bullet.OnTriggerEnter2D(enemyObj.GetComponent<Collider2D>());
        Assert.AreEqual(enemy.health, 90); // enemy does not take damage from enemy bullet

    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator BulletDoesDamageToRightEntityWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
