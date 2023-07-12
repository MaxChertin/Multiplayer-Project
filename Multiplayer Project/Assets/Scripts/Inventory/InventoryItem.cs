using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    /*[HideInInspector]*/ public Item item;
    [HideInInspector] public uint count;

    //refrences to UI
    [SerializeField] private Image image;

    private Transform parentSlot;

    // Start method temporary only. for testing purposes only. TODO delete
    private void Start()
    {
        OnInitializeItem();
    }

    public void OnInitializeItem()
    {
        image.sprite = item.icon;
        image.color = Color.white;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentSlot = transform.parent;
        image.raycastTarget = false;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = (Vector3) eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentSlot);
        transform.localPosition = Vector3.zero;
        image.raycastTarget = true;
    }
}
