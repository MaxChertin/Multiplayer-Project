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
        
        CombineItems(currentSlotInvItem, droppedInvItem);
        droppedInvItem.parentSlot = transform;
    }

    public void CombineItems()
    {
        if (transform.childCount > 1)
        {
            foreach (InventoryItem invItem in GetComponentsInChildren<InventoryItem>())
            {
                //if (invItem)
            }
        }
    }

    public void CombineItems(InventoryItem currentSlotInvItem, InventoryItem droppedInvItem)
    {
        if (transform.childCount != 0)
        {
            if (currentSlotInvItem.item.id == droppedInvItem.item.id)
            {
                Destroy(currentSlotInvItem.gameObject);
                droppedInvItem.count += currentSlotInvItem.count;
            }
            
            // TODO Handle other conditions 
        }
    }

    public bool HasItemInSlot () => transform.childCount != 0;
}
