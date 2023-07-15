using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; 
    public List<InventorySlot> inventory = new List<InventorySlot>();

    private const ushort maxItemStack = 2500;
    
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
        uint amount = 20000;
        bool assignedEmpty = false;
        InventorySlot firstEmptySlot = null;
        
        for (ushort iSlot = 0; iSlot < inventory.Count; iSlot++)
        {
            // For every slot in inventory try to get the inventory item
            var iInvItem = inventory[iSlot].GetComponentInChildren<InventoryItem>();

            if (iInvItem != null && iInvItem.item.id == item.id)
            {
                if (iInvItem.count + amount > maxItemStack)
                {
                    amount = iInvItem.count + amount - maxItemStack;
                    iInvItem.count = maxItemStack;
                }

                else
                {
                    iInvItem.count += amount;
                    iInvItem.UpdateTxtCount();
                    return;
                }
                
                iInvItem.UpdateTxtCount();
            }
        }

        // Looping for empty slots and adding item(s) 
        for (ushort iSlot = 0; iSlot < inventory.Count; iSlot++)
        {
            var iInvItem = inventory[iSlot].GetComponentInChildren<InventoryItem>();
            if (iInvItem == null)
            {
                InventoryItem newInvItem = Instantiate(inventoryItemPrefab, Vector3.zero, Quaternion.identity, inventory[iSlot].transform);
                newInvItem.item = item;
                
                if (amount <= maxItemStack) 
                {
                    newInvItem.count = amount;
                    newInvItem.UpdateTxtCount();
                    return;
                }
                newInvItem.count = maxItemStack;
                amount -= maxItemStack;
                newInvItem.UpdateTxtCount();
            }
        }
    }
}
