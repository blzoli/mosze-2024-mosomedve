using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Weapon that fires multiple projectiles in a spread pattern.
/// </summary>
public class ShotGun : Weapon
{
    public GameObject projectilePrefab;  ///< The projectile prefab to instantiate.
    public float projectileSpeed = 10f;  ///< The speed of the projectile.
    public int numberOfProjectiles = 5;  ///< The number of projectiles to fire.
    public float spreadAngle = 30f;  ///< The angle between each projectile.

    private Vector2 direction;  ///< The direction of the projectile.

    public void Awake()
    {
        this.damage = 1;
    }

    public override void Fire(GameObject caller)
    {
        direction = caller.tag == "Player" ? Vector2.up : Vector2.down;

        if (projectilePrefab != null)
        {
            Vector3 firePosition = caller.transform.position;

            for (int i = 0; i < numberOfProjectiles; i++)
            {
                GameObject projectile = Instantiate(projectilePrefab, firePosition, Quaternion.identity);
                projectile.tag = (caller.tag == "Player") ? "PlayerProjectile" : "EnemyProjectile";
                projectile.GetComponent<Bullet>().damage = damage;

                Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    float angle = (i - (numberOfProjectiles - 1) / 2) * spreadAngle;
                    Vector2 spreadDirection = Quaternion.Euler(0, 0, angle) * direction;
                    rb.velocity = spreadDirection * projectileSpeed;
                }
            }
        }
    }

}
