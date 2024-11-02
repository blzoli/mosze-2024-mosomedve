using UnityEngine;

/// <summary>
/// Abstract base class for weapons.
/// </summary>
public abstract class Weapon : Item
{
    /// <summary>
    /// The damage inflicted by the weapon.
    /// </summary>
    public float damage = 1f;

    /// <summary>
    /// Fire the weapon.
    /// </summary>
    public abstract void Fire();

    /// <summary>
    /// Pick up the weapon.
    /// </summary>
    public override void ApplyEffect(GameObject player)
    {
        // Call the player's PickupWeapon method.
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.PickUpWeapon(this);
        }
    }
}
