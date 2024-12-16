using System.Collections;
using UnityEngine;

/**
 * @class PowerUp
 * @brief A class representing a power-up item that gives the player a temporary speed boost.
 */
public class SpeedPowerUp : Item
{
    public float speedBoostAmount = 5f;  ///< The amount by which the player's speed is increased.
    public float duration = 5f;         ///< The duration of the speed boost effect.

    /**
     * @brief Applies the speed boost effect to the player.
     * 
     * This method temporarily increases the player's speed.
     * @param player The GameObject representing the player.
     */
    public override void ApplyEffect(GameObject player)
    {
        Debug.Log("Speed boost applied!");
        StartCoroutine(ApplySpeedBoost(player));
    }


    /**
     * @brief Coroutine to apply the speed boost effect.
     * 
     * This coroutine increases the player's speed by the specified amount for the specified duration.
     * @param player The GameObject representing the player.
     */
    private IEnumerator ApplySpeedBoost(GameObject player)
    {
        Debug.Log("Applying speed boost...");
        PlayerController playerController = player.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.moveSpeed += speedBoostAmount;
            Debug.Log($"Player speed increased to {playerController.moveSpeed}");

            Debug.Log($"Waiting for {duration} seconds...");
            yield return new WaitForSeconds(duration);

            playerController.moveSpeed -= speedBoostAmount;
            Debug.Log("Speed boost ended. Player speed reset to " + playerController.moveSpeed);
        }
        else
        {
            Debug.LogError("PlayerController component not found on player object.");
        }
    }
}


