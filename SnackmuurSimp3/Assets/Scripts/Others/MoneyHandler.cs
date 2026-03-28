using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public float currentMoney = 5f;

    private void Start()
    {
        LoadMoney();
        UpdateUI();
    }

    public bool RemoveMoney(float amount)
    {
        if (currentMoney - amount < 0)
        {
            Debug.Log("Not enough money!");
            return false;
        }

        currentMoney -= amount;
        UpdateUI();
        return true;
    }

    public void GiveMoney(float amount)
    {
        currentMoney += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        moneyText.text = "€" + currentMoney.ToString("F2");
    }

    private void SaveMoney()
    {
        PlayerPrefs.SetFloat("Money", currentMoney);
        PlayerPrefs.Save();
    }

    private void LoadMoney()
    {
        currentMoney = PlayerPrefs.GetFloat("Money", 5f);
    }

    private void OnApplicationQuit()
    {
        SaveMoney();
    }
}