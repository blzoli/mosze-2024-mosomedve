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
    private Coroutine moveInCircleCoroutine;

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
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Item collected by player!");
            ApplyEffect(collision.gameObject);
            // deactivate but leaves the object in the scene
            /*gameObject.SetActive(false);*/
            transform.position = new Vector3(-200, -200, 0); // can't deactivate it because of powerups. just move it offscreen

            // Stop the MoveInCircle coroutine
            if (moveInCircleCoroutine != null)
            {
                StopCoroutine(moveInCircleCoroutine);
                moveInCircleCoroutine = null;
            }
        }
    }

    public void Start()
    {
        // moves item slowly in a circle to make it more interesting
        Vector3 position = transform.position;
        moveInCircleCoroutine = StartCoroutine(MoveInCircle(position));
    }

    private System.Collections.IEnumerator MoveInCircle(Vector3 position)
    {
        float angle = 0;
        float radius = 0.2f;
        while (true)
        {
            angle += Time.deltaTime/2;
            float x = position.x + Mathf.Cos(angle) * radius;
            float y = position.y + Mathf.Sin(angle) * radius;
            transform.position = new Vector3(x, y, 0);
            yield return null;
        }
    }
}