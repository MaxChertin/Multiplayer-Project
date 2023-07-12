using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /*[HideInInspector]*/ public Item item;
    [HideInInspector] public uint count;
    
    //refrences to UI
    [SerializeField] Image image;


    public void OnBeginDrag(PointerEventData eventData)
    {
        //image = item.
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = (Vector3) eventData.position;
        Debug.Log("DRAGGING");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}
