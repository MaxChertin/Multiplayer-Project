using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; 
    public List<InventorySlot> inventory = new List<InventorySlot>();

    [SerializeField] private InventoryItem inventoryItemPrefab;
    [SerializeField] private Item itemTest;
    // TODO Switch inventory system to be server authoritative
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
    }

    public void AddItem(Item item)
    {
        bool assignedEmpty = false;
        InventorySlot firstEmptySlot;
        InventorySlot firstSlot;
        
        for (ushort iSlot = 0; iSlot < inventory.Count; iSlot++)
        {
            // First check if there is any item that is the same in the inventory (if yes, check that its not stacked)
            // If the item is stacked, find the second item with the same id (if there is one) and add it there
            // If it is stacked, repeat the proccess.
            // If didnt find any same item (either there isn't any or there is but they are all stacked), find the first slot that is not occupied by any item and add the item there.
            
            if (!assignedEmpty && !inventory[iSlot].HasItemInSlot())
            {
                firstEmptySlot = inventory[iSlot];
                assignedEmpty = true;
            }
                
            if (!inventory[iSlot].HasItemInSlot())
            {
                
            }
        }
        // InventoryItem _invItem = Instantiate(inventoryItemPrefab, Vector3.zero, Quaternion.identity, inventory[iSlot].transform);
        // _invItem.item = itemTest;
        // return;
    }
}
