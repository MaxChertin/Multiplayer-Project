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
    private void Start () => OnInitializeItem();

    private void OnInitializeItem()
    {
        // Initialize item
        transform.localPosition = Vector3.zero;
        image.sprite = item.icon;
        image.color = Color.white;
        UpdateTxtCount();
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
        UpdateTxtCount();
    }
    
    public void UpdateTxtCount () => countTxt.text = count != 1 ? "x" + count.ToString("N0") : string.Empty;
}
