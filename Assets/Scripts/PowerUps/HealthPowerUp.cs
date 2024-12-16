using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Power-up that increases the player's health.
/// </summary>
public class HealthPowerUp : Item
{
    public int healthBoostAmount = 1;

    /// <summary>
    /// Increases the player's health when the power-up is picked up.
    /// </summary>
    public override void ApplyEffect(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();

        playerController.IncreaseHealth(healthBoostAmount);

        AudioManager.Instance.PlaySound("healthPickupSound");
    }
}
