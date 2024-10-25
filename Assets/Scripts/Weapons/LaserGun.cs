using UnityEngine;

/// <summary>
/// Represents a laser gun weapon.
/// </summary>
public class LaserGun : Weapon
{
    /// <summary>
    /// Fires the laser gun.
    /// </summary>
    public override void Fire()
    {
        // Implement the firing logic for the LaserGun
        Debug.Log("LaserGun fired!");
    }
}
