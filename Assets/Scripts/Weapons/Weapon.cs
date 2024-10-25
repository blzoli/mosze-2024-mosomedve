using UnityEngine;

/// <summary>
/// Abstract base class for weapons.
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    /// <summary>
    /// The damage inflicted by the weapon.
    /// </summary>
    public float damage = 1f;

    /// <summary>
    /// Fire the weapon.
    /// </summary>
    public abstract void Fire();
}
