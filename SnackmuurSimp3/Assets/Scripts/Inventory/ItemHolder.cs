using UnityEngine;

[System.Serializable]
public class ItemPrefabMapping
{
    public Item item;
    public GameObject prefab;
}

public class ItemHolder : MonoBehaviour
{
    public InventoryManager inventoryManager;
    public Transform holdPoint;
    public ItemPrefabMapping[] itemPrefabs; // assign in Inspector

    private GameObject currentHeldObject;
    private Item currentItem;

    private void Update()
    {
        Item selectedItem = inventoryManager.GetSelectedItem(true);
        if (selectedItem != currentItem)
        {
            UpdateHeldItem(selectedItem);
        }
    }

    void UpdateHeldItem(Item newItem)
    {
        if (currentHeldObject != null)
        {
            Destroy(currentHeldObject);
            currentHeldObject = null;
        }

        currentItem = newItem;
        if (newItem == null) return;

        // find matching prefab
        foreach (ItemPrefabMapping mapping in itemPrefabs)
        {
            if (mapping.item == newItem)
            {
                currentHeldObject = Instantiate(mapping.prefab, holdPoint.position, holdPoint.rotation);
                currentHeldObject.transform.SetParent(holdPoint);
                return;
            }
        }

        Debug.LogWarning("No prefab mapped for item: " + newItem.Name);
    }
}