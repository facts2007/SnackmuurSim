using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;

    int selectedSlot = -1;
    private void Start()
    {
        changeSelectedSlot(0);
        print("Selected Slot: " + selectedSlot);
    }

    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number <= inventorySlots.Length) { 
                changeSelectedSlot(number - 1);
            }
        }
    }
    void changeSelectedSlot(int NewValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();
        } 
        
        inventorySlots[NewValue].Select();

        selectedSlot = NewValue;
        print("Selected Slot: " + selectedSlot);
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            
            if (itemInSlot == null )
            {
                SpawnNewItem(item, slot);
                return;
            }
        }
    }  

    public void SpawnNewItem(Item item, InventorySlot slot) 
    { 
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if (use == false) {
                Destroy(itemInSlot.gameObject);
            }

            return item;
        }
        return null;
    }
}
