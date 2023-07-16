using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("Base Item")]
    /* [HideInInspector] */ public Item item;
    /* [HideInInspector] */ public uint count;
    [HideInInspector] public Transform parentSlot;

    //refrences to UI
    [Header("UI")]
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI countTxt;


    // Start method temporary only. for testing purposes only. TODO delete when done testing
    // TODO replace logic dragging -> clicking
    private void Start () => OnInitializeItem();

    private void OnInitializeItem()
    {
        // Initialize item
        transform.localPosition = Vector3.zero;
        image.sprite = item.icon;
        image.color = Color.white;
        parentSlot = transform.parent;
        UpdateTxtCount();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && count != 1)
            count = InventoryManager.Instance.SplitItems(this);
        UpdateTxtCount();
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
        UpdateTxtCount();
    }

    private void OnTransformParentChanged()
    {
        
    }

    public void UpdateTxtCount () => countTxt.text = count != 1 ? "x" + count.ToString("N0") : string.Empty;
}
