using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    [HideInInspector] public Item item;

    [Header("UI")]
    public RawImage Image;
    [HideInInspector] public Transform parentAfterDrag;

    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        Image.texture = newItem.image;
        GetComponentInChildren<TextMeshProUGUI>().text = newItem.Name;
    }

    //drag en drop
    public void OnBeginDrag(PointerEventData eventData) {
        Image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData) { 
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) { 
        Image.raycastTarget=true;
        transform.SetParent(parentAfterDrag);
    }
}
