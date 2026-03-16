using UnityEngine;

public class Snackvakje : MonoBehaviour
{
    public string vakjeID;
    public Item currentItem;

    private void Start()
    {
        LoadItem();
    }

    public bool PlaceItem(Item item)
    {
        if (currentItem != null)
        {
            Debug.Log("Vakje already has an item!");
            return false;
        }

        currentItem = item;
        SaveItem();
        Debug.Log("Placed: " + item.Name + " in " + vakjeID);
        return true;
    }

    public Item BuyItem()
    {
        Debug.Log("BuyItem called on: " + vakjeID + " current item: " + (currentItem != null ? currentItem.Name : "null"));
        if (currentItem == null) return null;

        Item boughtItem = currentItem;
        currentItem = null;

        InventoryItem visual = GetComponentInChildren<InventoryItem>();
        if (visual != null)
        {
            Destroy(visual.gameObject);
        }

        PlayerPrefs.SetString(vakjeID, "");
        PlayerPrefs.Save();

        Debug.Log("Bought: " + boughtItem.Name + " vakje is now empty.");
        return boughtItem;
    }

    private void SaveItem()
    {
        PlayerPrefs.SetString(vakjeID, currentItem != null ? currentItem.Name : "");
        PlayerPrefs.Save();
    }

    private void LoadItem()
    {
        string savedName = PlayerPrefs.GetString(vakjeID, "");
        if (savedName != "")
        {
            currentItem = Resources.Load<Item>(savedName);
        }
    }
}