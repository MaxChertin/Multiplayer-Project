using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData data)
    {
        // On inventory slot of the dropped one
        // TODO Add extra checks and verifications + support other functions
        data.pointerDrag.GetComponent<InventoryItem>().parentSlot = transform;
    }
}
