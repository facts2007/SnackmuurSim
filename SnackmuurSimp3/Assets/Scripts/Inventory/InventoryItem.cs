using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    [Header("UI")]
    //public Image Image;
    [HideInInspector] public Transform parentAfterDrag;

    //drag en drop

    public void OnBeginDrag(PointerEventData eventData) {
       // Image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData) { 
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData) { 
       // Image.raycastTarget=true;
        transform.SetParent(parentAfterDrag);
    }
}
