using UnityEngine;
using TMPro;

public class VakjeShop : MonoBehaviour
{
    public Snackvakje[] allVakjes;
    public MoneyManager moneyManager;
    public float baseVakjeCost = 5f;
    public float priceMultiplier = 1.3f;
    public int startingVakjes = 2;
    public TextMeshProUGUI priceText;

    private int unlockedCount;

    private void Start()
    {
        LoadUnlocked();
        RefreshVakjes();
        UpdatePriceText();
    }

    public void BuyVakjeButton()
    {
        BuyVakje();
    }

    public bool BuyVakje()
    {
        if (unlockedCount >= allVakjes.Length)
        {
            Debug.Log("All vakjes already unlocked!");
            return false;
        }

        float currentCost = GetCurrentCost();
        bool success = moneyManager.RemoveMoney(currentCost);
        if (success)
        {
            unlockedCount++;
            SaveUnlocked();
            RefreshVakjes();
            UpdatePriceText();
            Debug.Log("Vakje unlocked! Total: " + unlockedCount);
            return true;
        }

        Debug.Log("Not enough money!");
        return false;
    }

    float GetCurrentCost()
    {
        return baseVakjeCost * Mathf.Pow(priceMultiplier, unlockedCount);
    }

    void UpdatePriceText()
    {
        if (priceText != null)
            priceText.text = "€" + GetCurrentCost().ToString("F2");
    }

    void RefreshVakjes()
    {
        for (int i = 0; i < allVakjes.Length; i++)
        {
            allVakjes[i].gameObject.SetActive(i < unlockedCount);
        }
    }

    void SaveUnlocked()
    {
        PlayerPrefs.SetInt("UnlockedVakjes", unlockedCount);
        PlayerPrefs.Save();
    }

    void LoadUnlocked()
    {
        unlockedCount = PlayerPrefs.GetInt("UnlockedVakjes", startingVakjes);
    }
}