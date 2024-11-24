using UnityEngine;

/// <summary>
/// Represents a laser gun weapon.
/// </summary>
public class LaserGun : Weapon
{
    /// <summary>
    /// Fires the laser gun.
    /// </summary>
    /// 
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public override void Fire(string projectileType)
    {
        // Implement the firing logic for the LaserGun
        Debug.Log("LaserGun fired!");

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
