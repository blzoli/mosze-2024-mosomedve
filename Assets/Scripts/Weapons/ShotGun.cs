using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Weapon
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public int numberOfProjectiles = 5;
    public float spreadAngle = 30f;

    private Vector2 direction;

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
