using System.Runtime.CompilerServices;
using UnityEngine;

/// <summary>
/// Represents a Rocket Launcher weapon.
/// </summary>
public class RocketLauncher : Weapon
{
    public GameObject projectilePrefab;  ///< The projectile prefab to instantiate.
    public float projectileSpeed = 3f;  ///< The speed of the projectile.

    private Vector2 direction;  ///< The direction of the projectile.

    public void Awake()
    {
        this.damage = 2;
    }
    /// <summary>
    /// Fires the rocket launcher.
    /// </summary>
    public override void Fire(GameObject caller)
    {

        direction = caller.tag == "Player" ? Vector2.up : Vector2.down;


        if (projectilePrefab != null)
        {
            Vector3 firePosition = caller.transform.position;
            GameObject projectile = Instantiate(projectilePrefab, firePosition, Quaternion.identity);
            projectile.tag = (caller.tag == "Player") ? "PlayerProjectile" : "EnemyProjectile"; // Set the tag of the projectile to projectileTag
            projectile.GetComponent<Bullet>().damage = damage; // Set the damage of the projectile to damage

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = direction * projectileSpeed;
            }
        }
    }
}
