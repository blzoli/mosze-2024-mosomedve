using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemOnDeath : MonoBehaviour
{
    public GameObject itemPrefab;
    private void OnDestroy()
    {
        Instantiate(itemPrefab, transform.position, Quaternion.identity);
    }
}
