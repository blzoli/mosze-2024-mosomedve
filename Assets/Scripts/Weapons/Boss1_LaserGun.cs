using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1_LaserGun : Weapon
{
    /// <summary>
    /// Fires the laser gun.
    /// </summary>
    /// 
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;

    private Vector2 direction;

    public void Awake()
    {
        this.damage = 1;
    }

    public override void Fire(GameObject caller)
    {

        if (projectilePrefab != null)
        {
            fireGunAtPosition(caller.transform.position);
            fireGunAtPosition(caller.transform.position + new Vector3(0.75f, 0, 0));
            fireGunAtPosition(caller.transform.position + new Vector3(-0.75f, 0, 0));
        }
    }

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
