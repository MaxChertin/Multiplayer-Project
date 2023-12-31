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
    [HideInInspector] public bool interactable = true;

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
        parentSlot = transform.parent;
        UpdateTxtCount();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            GetComponentInParent<InventorySlot>().QuickHop(eventData);
            return;
        }
        if (!interactable) return;
        if (eventData.button == PointerEventData.InputButton.Right && count != 1)
            count = InventoryManager.Instance.SplitItems(this);
        if (eventData.button == PointerEventData.InputButton.Middle && count != 1)
            count = InventoryManager.Instance.SingleSplitItem(this);
        
        UpdateTxtCount();
        parentSlot = transform.parent;
        image.raycastTarget = false;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!interactable || Input.GetKey(KeyCode.LeftShift)) return;
        transform.position = (Vector3) eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!interactable || Input.GetKey(KeyCode.LeftShift)) return;
        
        // BUG: While seperating Items, when dragged item will be dropped outside a slot, two of the separeted items will be stacked on top of each other, without combining them.
        
        transform.SetParent(parentSlot);
        transform.localPosition = Vector3.zero;
        transform.localScale = new Vector3(1, 1, 1);
        image.raycastTarget = true;
        UpdateTxtCount();
    }

    public void UpdateTxtCount () => countTxt.text = count != 1 ? "x" + count.ToString("N0") : string.Empty;
    
}
