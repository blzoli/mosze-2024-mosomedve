using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Alap attribútumok
    public float speed = 3.0f;  // sebesseg
    public int health = 5;    // hp
    public int attackPower = 10; // attack
    public float attackRange = 7.0f; // attack range
    public float attackRate = 2.0f; // attack rate

    public Weapon weapon; // fegyver

    private Transform player;    // jatekos pozicioja
    private float lastAttackTime; // utolso tamadas ideje
    private float cameraHalfHeight; // kamera magassaga

    private float lastDirectionChange; // utolso iranyvaltas
    private bool movingRight = true; // jobbra mozogjon-e

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

    // ellenseg mozgasa jatekos fele
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
                if (Random.Range(0, 100) < 50 && Time.time - lastDirectionChange > 1.0f)
                {
                    lastDirectionChange = Time.time;
                    movingRight = !movingRight; // Toggle direction
                }

                if (movingRight && transform.position.x < cameraRightBound)
                {
                    transform.position += Vector3.right * speed * Time.deltaTime;
                }
                else if (!movingRight && transform.position.x > cameraLeftBound)
                {
                    transform.position += Vector3.left * speed * Time.deltaTime;
                }
            }
        }
    }

    // tamadas
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

    // ellenseg sebzese
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy sebzést kapott: " + damage + ". Jelenlegi életerõ: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    // ellenseg death
    void Die()
    {
        Debug.Log("Enemy megsemmisült!");
        Destroy(gameObject); // ellenseg eltavolitasa
    }
}
