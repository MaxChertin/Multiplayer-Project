using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData data)
    {
        // On inventory slot of the dropped one
        // TODO Add extra checks and verifications + support other functions
        
        var currentSlotInvItem = GetComponentInChildren<InventoryItem>();
        var droppedInvItem = data.pointerDrag.GetComponent<InventoryItem>();

        if (CombineItems(droppedInvItem, currentSlotInvItem))
        {
            droppedInvItem.parentSlot = transform;
        }
    }

    public bool CombineItems(InventoryItem droppedInvItem, InventoryItem currentSlotInvItem = null)
    {
        if (currentSlotInvItem == null) currentSlotInvItem = GetComponentInChildren<InventoryItem>();
        if (transform.childCount != 0)
        {
            if (currentSlotInvItem.count + droppedInvItem.count > InventoryManager.maxItemStack) return false;
            if (currentSlotInvItem.item.id == droppedInvItem.item.id)
            {
                Destroy(currentSlotInvItem.gameObject);
                droppedInvItem.count += currentSlotInvItem.count;
            }
            
            // TODO Handle other conditions 
        }

        return true;
    }

    private void OnTransformChildrenChanged()
    {
        Debug.Log($"child change, child count: {transform.childCount}. SLOT: {transform.name}");
        //if (transform.childCount == 2)
        //{
        //    InventoryItem[] children = GetComponentsInChildren<InventoryItem>();
        //    CombineItems(children[0], children[1]);
        //}
    }

    public bool HasItemInSlot () => transform.childCount != 0;
}
