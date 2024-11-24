using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage; ///< Amount of damage the bullet inflicts.

    // @brief Destroys the bullet when it collides with another object.
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // check if bullet is friendly or not (PlayerProjectile - friendly or EnemyProjectile tag - enemy)
        if (transform.transform.CompareTag("PlayerProjectile") && collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            Destroy(this.gameObject);
        }
        else if (transform.transform.CompareTag("EnemyProjectile") && collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }

}
