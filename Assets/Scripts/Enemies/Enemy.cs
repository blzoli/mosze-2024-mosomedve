using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Alap attribútumok
    public float speed = 3.0f;  // sebesseg
    public int health = 100;    // hp
    public int attackPower = 10; // attack
    public float attackRange = 7.0f; // attack range
    public float attackRate = 1.0f; // attack rate

    public Weapon weapon; // fegyver

    private Transform player;    // jatekos pozicioja
    private float lastAttackTime; // utolso tamadas ideje

    void Start()
    {
        // jatekos player taggel
        player = GameObject.FindWithTag("Player").transform;
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
    void MoveTowardsPlayer()
    {
        // check if player is not too close
        if (player != null)
        {
            if (Vector3.Distance(transform.position, player.position) > attackRange)
            {
                Vector3 direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime; 
            }
            else // ha mar eleg kozel van, jobbra balra mozogjon
            {
                if (Time.time % 2 < 1)
                {
                    transform.position += Vector3.right * speed * Time.deltaTime;
                }
                else
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
