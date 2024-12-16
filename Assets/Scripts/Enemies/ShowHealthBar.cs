using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script to show health bar above enemy.
/// </summary>  
public class ShowHealthBar : MonoBehaviour
{
    private GameObject enemy;  ///< The enemy this health bar is for.
    private int maxHealth;     ///< The maximum health of the enemy.
    private int currentHealth; ///< The current health of the enemy.
    public GameObject healthBarPrefab; ///< The health bar prefab.
    
    /// @brief Instantiate the health bar prefab and set the enemy.
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

    /// @brief Update the health bar width to reflect the current health.
    private void UpdateHealthBar()
    {
        // set x scale of health bar to current health / max health

        healthBarPrefab.transform.localScale = new Vector3((float)currentHealth / maxHealth * 1.2f, 0.1f, healthBarPrefab.transform.localScale.z);
    }
}
