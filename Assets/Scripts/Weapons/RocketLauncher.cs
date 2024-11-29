using System.Runtime.CompilerServices;
using UnityEngine;
using static UnityEditor.Rendering.CameraUI;

/// <summary>
/// Represents a Rocket Launcher weapon.
/// </summary>
public class RocketLauncher : Weapon
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 3f;

    private Vector2 direction;

    public void Start()
    {
        this.damage = 2;
    }
    /// <summary>
    /// Fires the rocket launcher.
    /// </summary>
    /// 

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
