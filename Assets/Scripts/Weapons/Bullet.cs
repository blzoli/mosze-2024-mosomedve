using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage; ///< Amount of damage the bullet inflicts.

    private void Start()
    {
        // Start the despawn coroutine
        StartCoroutine(DespawnAfterTime(10f));
    }

    // Coroutine to despawn the bullet after a specified time
    private IEnumerator DespawnAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (Application.isPlaying) Destroy(this.gameObject);
    }

    // @brief Destroys the bullet when it collides with another object.
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Bullet collided with " + collision.gameObject.name);
        // check if bullet is friendly or not (PlayerProjectile - friendly or EnemyProjectile tag - enemy)
        if ((transform.gameObject.tag == "PlayerProjectile") && (collision.gameObject.tag == "Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            // only destroy in play mode to avoid test exception in edit mode
            if (Application.isPlaying) Destroy(this.gameObject);
        }
        else if ((transform.gameObject.tag == "EnemyProjectile") && (collision.gameObject.tag == "Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.TakeDamage(damage);
            if (Application.isPlaying) Destroy(this.gameObject);
        }
    }
}
