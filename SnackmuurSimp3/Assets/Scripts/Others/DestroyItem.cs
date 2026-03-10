using UnityEngine;

public class GarbageCan : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Camera playerCamera;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == gameObject) // only runs if THIS object is clicked
                {
                    Item selectedItem = inventoryManager.GetSelectedItem(false);
                    if (selectedItem != null)
                    {
                        Debug.Log("Threw away: " + selectedItem.Name);
                    }
                    else
                    {
                        Debug.Log("Nothing to throw away.");
                    }
                }
            }
        }
    }
}