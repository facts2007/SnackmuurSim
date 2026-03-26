using UnityEngine;

public class Snackvakje : MonoBehaviour
{
    public string vakjeID;
    public Item currentItem;
    public Transform displayPoint; // empty GameObject inside vakje where item spawns

    private GameObject currentDisplayObject;

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
        SpawnDisplayObject();
        return true;
    }

    public Item BuyItem()
    {
        if (currentItem == null) return null;

        Item boughtItem = currentItem;
        currentItem = null;
        DestroyDisplayObject();
        PlayerPrefs.SetString(vakjeID, "");
        PlayerPrefs.Save();
        return boughtItem;
    }

    void SpawnDisplayObject()
    {
        DestroyDisplayObject();
        if (currentItem == null) return;

        GameObject prefab = Resources.Load<GameObject>(currentItem.Name);
        if (prefab != null)
        {
            Transform spawnAt = displayPoint != null ? displayPoint : transform;
            currentDisplayObject = Instantiate(prefab, spawnAt.position, spawnAt.rotation);
            currentDisplayObject.transform.SetParent(spawnAt);
        }
        else
        {
            Debug.LogWarning("No prefab found in Resources for: " + currentItem.Name);
        }
    }

    void DestroyDisplayObject()
    {
        if (currentDisplayObject != null)
        {
            Destroy(currentDisplayObject);
            currentDisplayObject = null;
        }
    }

    void SaveItem()
    {
        PlayerPrefs.SetString(vakjeID, currentItem != null ? currentItem.Name : "");
        PlayerPrefs.Save();
    }

    void LoadItem()
    {
        string savedName = PlayerPrefs.GetString(vakjeID, "");
        if (savedName != "")
        {
            currentItem = Resources.Load<Item>(savedName);
            if (currentItem != null)
                SpawnDisplayObject();
        }
    }
}