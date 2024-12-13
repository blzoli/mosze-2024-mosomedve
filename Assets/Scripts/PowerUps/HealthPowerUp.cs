using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : Item
{
    public int healthBoostAmount = 1;

    public override void ApplyEffect(GameObject player)
    {
        PlayerController playerController = player.GetComponent<PlayerController>();

        playerController.IncreaseHealth(healthBoostAmount);

        AudioManager.Instance.PlaySound("healthPickupSound");
    }
}
