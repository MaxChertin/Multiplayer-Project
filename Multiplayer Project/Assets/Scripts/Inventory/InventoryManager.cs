using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; 
    public Dictionary<InventoryItem, Item> inventory = new Dictionary<InventoryItem, Item>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
    }

    private void Start()
    {
        // Initialize inventory
        foreach (InventoryItem inventoryItem in GetComponentsInChildren<InventoryItem>())
        {
            inventory.Add(inventoryItem, null);
        }
    }
}
