using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A modified version of LaserGun that fires three bullets at once.
/// </summary>
public class Boss1_LaserGun : Weapon
{
    /// <summary>
    /// Fires the laser gun.
    /// </summary>
    /// 
    public GameObject projectilePrefab; ///< The projectile prefab to fire.
    public float projectileSpeed = 10f; ///< The speed of the projectile.

    private Vector2 direction;

    public void Awake()
    {
        this.damage = 1;
    }

    /// <summary>
    /// Fire the laser gun.
    /// </summary>
    public override void Fire(GameObject caller)
    {

        if (projectilePrefab != null)
        {
            fireGunAtPosition(caller.transform.position);
            fireGunAtPosition(caller.transform.position + new Vector3(0.75f, 0, 0));
            fireGunAtPosition(caller.transform.position + new Vector3(-0.75f, 0, 0));
        }
    }

    /// <summary>
    /// Used in Fire method to instantiate a projectile at a given position.
    /// </summary>
    private void fireGunAtPosition(Vector3 position)
    {
        GameObject projectile = Instantiate(projectilePrefab, position, Quaternion.identity);
        projectile.tag = "EnemyProjectile";
        projectile.GetComponent<Bullet>().damage = damage;

        Vector2 direction = Vector2.down;
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed;
        }
    }
}
