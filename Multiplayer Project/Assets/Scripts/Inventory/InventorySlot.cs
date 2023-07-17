using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler, IDragHandler, IPointerEnterHandler
{
    public void OnDrop(PointerEventData data)
    {
        // On inventory slot of the dropped one
        // TODO Add extra checks and verifications + support other functions
        
        var currentSlotInvItem = GetComponentInChildren<InventoryItem>();
        var droppedInvItem = data.pointerDrag.GetComponent<InventoryItem>();
        if (droppedInvItem == null || !droppedInvItem.interactable) return;

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
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            QuickHop(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            QuickHop(eventData);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
        {
            QuickHop(eventData);
        }
    }
    
    public void QuickHop(PointerEventData eventData)
    {
        InventoryItem invItem = GetComponentInChildren<InventoryItem>();
        if (invItem == null || !invItem.interactable) return;
        invItem.interactable = false;
        print ("not interactable anymore!");
        InventoryManager.Instance.QuickHop(invItem);
    }


    public bool HasItemInSlot () => transform.childCount != 0;
}
