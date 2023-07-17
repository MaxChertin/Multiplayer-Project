using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; 
    [Header("Inventory")]
    public List<InventorySlot> inventory = new List<InventorySlot>();

    public const ushort maxItemStack = 2500;
    
    [Header("Prefabs")]
    [SerializeField] private InventoryItem inventoryItemPrefab;
    // TODO Switch inventory system to be server authoritative
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
    }

    public void AddItem(Item item, uint amount)
    {
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

    public void RemoveItem(Item item, uint count)
    {
        for (ushort iSlot = 0; iSlot < inventory.Count; iSlot++)
        {
            var iInvItem = inventory[iSlot].GetComponentInChildren<InventoryItem>();
            if (iInvItem != null && iInvItem.item.id == item.id)
            {
                if (iInvItem.count - count >= 0)
                {
                    iInvItem.count -= count; //+ count=0
                    iInvItem.UpdateTxtCount();
                    return;
                }
                
                count -= iInvItem.count;
                Destroy(iInvItem.gameObject);
            }
        }
    }
    
    // Quick Hop (left mouse click + shift on slot)
    // TODO if there is items that are the same then add them together -> potentialy instantiate new items + add time delay + [later] animation
    // TODO add implementation for quick-hopping for other inventory when available.
    public void QuickHop(InventoryItem invItem)
    {
        GameObject storeTypeObj = invItem.GetComponentInParent<GridLayoutGroup>().gameObject;
        if (storeTypeObj.CompareTag("Inventory/Toolbar"))
        {
            for (ushort iSlot = 0; iSlot < inventory.Count; iSlot++)
            {
                var iInvSlot = inventory[iSlot].GetComponent<InventorySlot>();
                
                if (iInvSlot.HasItemInSlot())
                {
                    var iItemSlot = inventory[iSlot].GetComponentInChildren<InventoryItem>();
                    if (iItemSlot.count + invItem.count <= maxItemStack) // Normal operation
                    {
                        iItemSlot.count += invItem.count;
                        Destroy(invItem.gameObject);
                        return;
                    }
                    else // current slot has to be filled and then if needed instatiated
                    {
                        
                    }
                }

                if (!iInvSlot.HasItemInSlot())
                {
                    invItem.transform.SetParent(iInvSlot.transform);
                    invItem.transform.localPosition = Vector3.zero;
                    break;
                }
            }
        }
        else if (storeTypeObj.CompareTag("Inventory/Inventory"))
        {
            for (ushort iSlot = (ushort) (inventory.Count - 1); iSlot >= 0; iSlot--)
            {
                var iInvSlot = inventory[iSlot].GetComponent<InventorySlot>();
                if (!iInvSlot.HasItemInSlot())
                {
                    invItem.transform.SetParent(iInvSlot.transform);
                    invItem.transform.localPosition = Vector3.zero;
                    break;
                }
            }
        }

        invItem.interactable = true;
    }

    public uint SplitItems(InventoryItem inventoryItem)
    {
        InventorySlot invSlot = inventoryItem.parentSlot.GetComponent<InventorySlot>();
        InventoryItem newInvItem = Instantiate(inventoryItemPrefab, Vector3.zero, Quaternion.identity, invSlot.transform);
        newInvItem.item = inventoryItem.item;
        
        newInvItem.count = inventoryItem.count / 2u;
        return inventoryItem.count % 2 == 0 ? inventoryItem.count / 2u : inventoryItem.count / 2u + 1;
    }
}
