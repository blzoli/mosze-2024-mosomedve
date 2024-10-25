using UnityEngine;

/// <summary>
/// Represents a Rocket Launcher weapon.
/// </summary>
public class RocketLauncher : Weapon
{
    /// <summary>
    /// Fires the Rocket Launcher.
    /// </summary>
    public override void Fire()
    {
        // Implement the firing logic for the RocketLauncher
        Debug.Log("RocketLauncher fired!");
    }
}
