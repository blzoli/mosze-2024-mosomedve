using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Drops an item when the object is destroyed.
/// </summary>
public class DropItemOnDeath : MonoBehaviour
{
    public GameObject itemPrefab; ///< The item to drop.

    /// <summary>
    /// Drops item when the object is destroyed. Disabled when the game is over.
    /// </summary>
    private void OnDestroy()
    {
        if (!Game.isOver) Instantiate(itemPrefab, transform.position, Quaternion.identity);
    }
}
