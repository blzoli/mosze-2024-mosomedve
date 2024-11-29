using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enemy class represents an enemy object in the game.
/// 

public class Enemy : MonoBehaviour
{
    
    public float speed = 3.0f;  ///< The speed of the enemy.
    public int health = 5;    ///< The health of the enemy.
    public int attackPower = 10; ///< The attack power of the enemy.
    public float attackRange = 7.0f; ///< The attack range of the enemy.
    public float attackRate = 2.0f; ///< The attack rate of the enemy.

    public Weapon weapon; ///< The weapon equipped by the enemy.

    private Transform player;    ///< Reference to the player objects position.
    private float lastAttackTime; ///< The time of the last attack.
    private float cameraHalfHeight; ///< The half-height of the camera.

    private float lastDirectionChange; ///< The time of the last direction change.
    private bool movingRight = true; ///< Indicates whether the enemy is moving right.

    void Start()
    {
        // jatekos player taggel
        player = GameObject.FindWithTag("Player").transform;
        // kamera magassaganak felet vertikalisan beallitjuk
        cameraHalfHeight = Camera.main.transform.position.y;
    }

    // mozgas es tamadas
    void Update()
    {
        // mozgas a jatekoshoz
        MoveTowardsPlayer();

        // tamadas ha jatekos kozel van
        if (Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            AttackPlayer();
        }
    }

    /// <summary>
    /// Moves the enemy towards the player.
    /// </summary>
    public void MoveTowardsPlayer()
    {
        // check if player is not too close
        if (player != null)
        {
            if (transform.position.y > cameraHalfHeight)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }
            else // ha mar eleg kozel van, jobbra balra mozogjon
            {
                float cameraLeftBound = Camera.main.transform.position.x - (Camera.main.orthographicSize * Camera.main.aspect);
                float cameraRightBound = Camera.main.transform.position.x + (Camera.main.orthographicSize * Camera.main.aspect);

                // Randomly decide to move left or right, but not too often
                if (Random.Range(0, 100) < 50 && Time.time - lastDirectionChange > 1.0f + Random.Range(0f, 1.0f))
                {
                    lastDirectionChange = Time.time;
                    movingRight = !movingRight; // Toggle direction
                }

                if (movingRight && transform.position.x < cameraRightBound ) // Try to avoid other enemies
                {
                    transform.position += Vector3.right * speed * Time.deltaTime;
                }
                else if (!movingRight && transform.position.x > cameraLeftBound) // TODO check nearby positions without paralysis
                {
                    transform.position += Vector3.left * speed * Time.deltaTime;
                }
            }
        }
    }

    /// <summary>
    /// Checks if another enemy is nearby.
    /// </summary>
    private bool IsAnotherEnemyNearby()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy") && hitCollider.gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Attacks the player, if the player is in range and the attack rate allows it.
    /// </summary>
    void AttackPlayer()
    {
        // tamadas rate
        if (Time.time - lastAttackTime < attackRate)
        {
            return;
        }
        else
        {
            lastAttackTime = Time.time;
            weapon.Fire(transform.gameObject);
        }
    }

    /// <summary>
    /// Takes damage and checks if the enemy is dead.
    /// </summary>
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy sebzést kapott: " + damage + ". Jelenlegi életerõ: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Destroys the enemy object.
    /// </summary>
    void Die()
    {
        Debug.Log("Enemy megsemmisült!");
        Destroy(gameObject); // ellenseg eltavolitasa
    }
}
