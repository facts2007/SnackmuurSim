using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class QuestHandler : MonoBehaviour
{
    [Header("Quest 1 - Snacks")]
    public string questName;
    public int targetAmount;
    public int currentAmount;
    [Header("Quest 2 - Cleaning")]
    public string cleanQuestName;
    public int cleanTargetAmount;
    public int cleanCurrentAmount;
    public TextMeshProUGUI cleanQuestText;
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
        GenerateNewCleanQuest();
        UpdateUI();
    }

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

    public void CleanDirt()
    {
        cleanCurrentAmount++;
        print("Schoongemaakt! Totaal: " + cleanCurrentAmount);
        if (cleanCurrentAmount >= cleanTargetAmount)
        {
            CompleteCleanQuest();
        }
        UpdateUI();
    }

    void CompleteQuest()
    {
        Debug.Log("Quest voltooid!");
        moneyManager.GiveMoney(rewardMoney);
        GenerateNewQuest();
    }

    void CompleteCleanQuest()
    {
        Debug.Log("Schoonmaak quest voltooid!");
        moneyManager.GiveMoney(5);
        GenerateNewCleanQuest();
    }

    void GenerateNewQuest()
    {
        currentAmount = 0;
        targetAmount = Random.Range(minTarget, maxTarget);
        rewardMoney = targetAmount;
        questName = "Verkoop " + targetAmount + " snacks";
        Debug.Log("Nieuwe quest: " + questName);
    }

    void GenerateNewCleanQuest()
    {
        cleanCurrentAmount = 0;
        cleanTargetAmount = Random.Range(minTarget, maxTarget);
        cleanQuestName = "Ruim " + cleanTargetAmount + " vuilnis op";
        Debug.Log("Nieuwe schoonmaak quest: " + cleanQuestName);
    }

    void UpdateUI()
    {
        if (questText != null)
            questText.text = questName + "\n" + currentAmount + "/" + targetAmount;

        if (cleanQuestText != null)
            cleanQuestText.text = cleanQuestName + "\n" + cleanCurrentAmount + "/" + cleanTargetAmount;
    }
}