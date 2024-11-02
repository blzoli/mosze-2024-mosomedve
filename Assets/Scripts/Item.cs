using System;
using UnityEngine;

/**
 * @class Item
 * @brief An abstract base class for items that can be collected by the player.
 *
 * This class should be attached to a 2D GameObject. When a player collides with the GameObject,
 * the `ApplyEffect()` method is called, and the item is destroyed.
 */
public abstract class Item : MonoBehaviour
{
    /**
     * @brief An abstract method to define the effect of the item on the player.
     * 
     * This method must be overridden by any class that inherits from Item.
     * @param player The GameObject representing the player that collides with the item.
     */
    public abstract void ApplyEffect(GameObject player);

    /**
     * @brief Detects collision with the player and triggers the item's effect.
     * 
     * If the colliding object has the tag "Player", the `ApplyEffect()` method is called, and the item is destroyed.
     * @param collision The collision data.
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Item collected by player!"); 
            ApplyEffect(collision.gameObject);
            // deactivate but leaves the object in the scene
            gameObject.SetActive(false);
        }
    }
}
