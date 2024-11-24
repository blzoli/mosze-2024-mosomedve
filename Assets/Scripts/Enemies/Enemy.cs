using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Alap attrib�tumok
    public float speed = 3.0f;  // sebesseg
    public int health = 100;    // hp
    public int attackPower = 10; // attack
    public float attackRange = 5.0f; // attack range

    public Weapon weapon; // fegyver

    private Transform player;    // jatekos pozicioja

    
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
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    // tamadas
    void AttackPlayer()
    {
        // player hp csokkenes
        Debug.Log("Enemy t�madja a j�t�kost!");
    }

    // ellenseg sebzese
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy sebz�st kapott: " + damage + ". Jelenlegi �leter�: " + health);

        if (health <= 0)
        {
            Die();
        }
    }

    // ellenseg death
    void Die()
    {
        Debug.Log("Enemy megsemmis�lt!");
        Destroy(gameObject); // ellenseg eltavolitasa
    }
}
