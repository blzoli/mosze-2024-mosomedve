using UnityEngine;

/// <summary>
/// Abstract base class for weapons.
/// </summary>
public abstract class Weapon : Item
{
    /// <summary>
    /// The damage inflicted by the weapon.
    /// </summary>
    public int damage = 1;

    /// <summary>
    /// Fire the weapon.
    /// </summary>
    public abstract void Fire(GameObject caller);

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
