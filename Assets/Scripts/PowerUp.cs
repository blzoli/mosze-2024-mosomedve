using System.Collections;
using UnityEngine;

/**
 * @class PowerUp
 * @brief A class representing a power-up item that gives the player a temporary speed boost.
 */
public class PowerUp : Item
{
    public float speedBoostAmount = 2f; 
    public float duration = 5f;         

    /**
     * @brief Applies the speed boost effect to the player.
     * 
     * This method temporarily increases the player's speed.
     * @param player The GameObject representing the player.
     */
    public override void ApplyEffect(GameObject player)
    {
        
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Debug.Log("Speed boost applied!");
            StartCoroutine(ApplySpeedBoost(rb));
        }
    }

 
   
    private IEnumerator ApplySpeedBoost(Rigidbody2D rb)
    {
      
        Vector2 originalVelocity = rb.velocity;
        rb.velocity *= speedBoostAmount;

        
        yield return new WaitForSeconds(duration);

      
        rb.velocity = originalVelocity;
        Debug.Log("Speed boost ended.");
    }
}


