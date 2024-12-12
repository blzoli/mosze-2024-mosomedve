using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHealthBar : MonoBehaviour
{
    private GameObject enemy;
    private int maxHealth;
    private int currentHealth;
    public GameObject healthBarPrefab;
    private void Start()
    {
        enemy = transform.gameObject;
        maxHealth = enemy.GetComponent<Enemy>().health;
        currentHealth = maxHealth;

        // put health bar as child of enemy
        healthBarPrefab = Instantiate(healthBarPrefab, enemy.transform.position + new Vector3(0, 1.5f, 0), Quaternion.identity);
        healthBarPrefab.transform.parent = enemy.transform;

    }

    private void Update()
    {
        currentHealth = enemy.GetComponent<Enemy>().health;
        
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        // set x scale of health bar to current health / max health

        healthBarPrefab.transform.localScale = new Vector3((float)currentHealth / maxHealth * 1.2f, 0.1f, healthBarPrefab.transform.localScale.z);
    }
}
