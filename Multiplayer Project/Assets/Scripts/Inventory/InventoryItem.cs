using Unity.VisualScripting;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /*[HideInInspector]*/ public Item item;
    [HideInInspector] public uint count;
    
    //refrences to UI
    [SerializeField] private Image image;


    public void OnBeginDrag(PointerEventData eventData)
    {
        //item.icon;
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
