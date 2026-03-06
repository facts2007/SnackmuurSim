using UnityEngine;
public class DemoScript : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Item[] itemsToPickUp;
    public void PickUpItem(int id)
    {
        inventoryManager.AddItem(itemsToPickUp[id]);
    }
    public void GetSelectedItem()
    {
        Item recievedItem = inventoryManager.GetSelectedItem(true);
        if (recievedItem != null)
        {
            Debug.Log("Selected Item: " + recievedItem.Name);
        }
        else
        {
            Debug.Log("No item selected.");
        }
    }

    public void UseSelectedItem()
    {
        Item recievedItem = inventoryManager.GetSelectedItem(false);
        if (recievedItem != null)
        {
            Debug.Log("Used Item: " + recievedItem.Name);
        }
        else
        {
            Debug.Log("No item Used.");
        }
    }
}
