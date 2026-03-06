using System.Security.Cryptography;
using UnityEngine;
    using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
    {
    public RawImage image;
    public Color selectedColor, notSelectedColor;

    private void Awake()
    {
        if (image == null)
            image = GetComponent<RawImage>(); // or GetComponentInChildren<RawImage>()
        Deselect();
    }
    public void Select()
    {
        image.color = selectedColor;
        print("Selected Slot: " + transform.GetSiblingIndex());
    }
    public void Deselect()
    {
        image.color = notSelectedColor;
        print("Deselected Slot: " + transform.GetSiblingIndex());
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            InventoryItem DraggableItem = dropped.GetComponent<InventoryItem>();
            DraggableItem.parentAfterDrag = transform;
        }
    }
}
