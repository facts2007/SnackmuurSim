using UnityEngine;

public class Muur : MonoBehaviour
{
    public Snackvakje[] vakjes;
    public InventoryManager inventoryManager;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Snackvakje clickedVakje = hit.collider.GetComponent<Snackvakje>();
                if (clickedVakje != null)
                {
                    Item selectedItem = inventoryManager.GetSelectedItem(true);
                    if (selectedItem != null)
                    {
                        bool placed = clickedVakje.PlaceItem(selectedItem);
                        if (placed)
                        {
                            inventoryManager.GetSelectedItem(false);
                        }
                    }
                    else
                    {
                        Debug.Log("No item selected!");
                    }
                }
            }
        }
    }

    public Item GetRandomAvailableItem()
    {
        System.Collections.Generic.List<Snackvakje> available = new();
        foreach (Snackvakje vakje in vakjes)
        {
            if (vakje.currentItem != null)
                available.Add(vakje);
        }

        if (available.Count == 0) return null;

        int random = Random.Range(0, available.Count);
        return available[random].BuyItem();
    }
}