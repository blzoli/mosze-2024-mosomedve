using UnityEngine;

/// <summary>
/// Represents a laser gun weapon.
/// </summary>
public class LaserGun : Weapon
{
    public GameObject projectilePrefab; ///< The projectile prefab to instantiate.
    public float projectileSpeed = 10f; ///< The speed of the projectile.

    private Vector2 direction; ///< The direction of the projectile.

    public void Awake()
    {
        this.damage = 1;
    }

    /// <summary>
    /// Fires the laser gun.
    /// </summary>
    public override void Fire(GameObject caller)
    {

        direction = caller.tag == "Player" ? Vector2.up : Vector2.down;

        if (projectilePrefab != null)
        {
            // find parent object with tag "Player" or "Enemy" and get its position
            
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
