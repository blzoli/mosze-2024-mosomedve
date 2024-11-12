using UnityEngine;

/// <summary>
/// Represents a Rocket Launcher weapon.
/// </summary>
public class RocketLauncher : Weapon
{
    /// <summary>
    /// Fires the rocket launcher.
    /// </summary>
    /// 
    public GameObject projectilePrefab;
    public float projectileSpeed = 3f;
    public override void Fire()
    {
        // Implement the firing logic for the RocketLauncher
        Debug.Log("RocketLauncher Fired!");

        if (projectilePrefab != null)
        {
            Vector3 firePosition = GameObject.FindWithTag("Player").transform.position;
            GameObject projectile = Instantiate(projectilePrefab, firePosition, Quaternion.identity);

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = Vector2.up * projectileSpeed;
            }
        }
    }
}
