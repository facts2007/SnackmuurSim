using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public MoneyManager moneyManager;

    int selectedSlot = -1;
    private void Start()
    {
        changeSelectedSlot(0);
        LoadInventory();
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
        bool success = moneyManager.RemoveMoney(item.Cost);
        if (success)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
            {

                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

                if (itemInSlot == null)
                {
                    SpawnNewItem(item, slot);
                    return;
                }
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

    public void SaveInventory()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventoryItem itemInSlot = inventorySlots[i].GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                //print("Saving item: " + itemInSlot.item.Name + " in slot " + i);
                PlayerPrefs.SetString("Slot" + i, itemInSlot.item.Name);
            }
            else
            {
                //print("Saving empty slot: " + i);   
                PlayerPrefs.SetString("Slot" + i, "");
            }
        }
        PlayerPrefs.Save();
    }

    public void LoadInventory()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            string itemName = PlayerPrefs.GetString("Slot" + i, "");
            if (itemName != "")
            {
                Item item = Resources.Load<Item>(itemName);
                if (item != null)
                {
                    print("Loading item: " + item.Name + " in slot " + i);
                    SpawnNewItem(item, inventorySlots[i]);
                }
            }
        }
    }

    private void OnApplicationQuit()
    {
        SaveInventory(); // save when game closes
    }
}
