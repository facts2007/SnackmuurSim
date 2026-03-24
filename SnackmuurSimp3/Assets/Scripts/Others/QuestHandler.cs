using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestHandler : MonoBehaviour
{
    [Header("Quest")]
    public string questName;
    public int targetAmount;
    public int currentAmount;

    [Header("Instellingen")]
    public int minTarget = 5;
    public int maxTarget = 20;

    [Header("Reward")]
    public int rewardMoney;
    public int playerMoney;

    [Header("UI")]
    public TextMeshProUGUI questText;

    [Header("Extra")]
    public MoneyManager moneyManager;

    void Start()
    {
        GenerateNewQuest();
        UpdateUI();
    }

    // 🍟 Snack verkopen
    public void SellSnack(int amount)
    {
        currentAmount += amount;
        print("Verkocht: " + amount + " snacks. Totaal verkocht: " + currentAmount);
        if (currentAmount >= targetAmount)
        {
            CompleteQuest();
        }

        UpdateUI();
    }

    void CompleteQuest()
    {
        Debug.Log("Quest voltooid!");

       
         moneyManager.GiveMoney(rewardMoney);

        GenerateNewQuest();
    }

    void GenerateNewQuest()
    {
        currentAmount = 0;

        targetAmount = Random.Range(minTarget, maxTarget + 1);

        rewardMoney = targetAmount * 2;

        questName = "Verkoop " + targetAmount + " snacks";

        Debug.Log("Nieuwe quest: " + questName);
    }

    void UpdateUI()
    {
        if (questText != null)
        {
            questText.text = questName + "\n" + currentAmount + "/" + targetAmount;
        }
    }
}