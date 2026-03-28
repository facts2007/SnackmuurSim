using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [HideInInspector] public Transform[] wallPositions;
    [HideInInspector] public Muur[] muren;
    [HideInInspector] public MoneyManager moneyManager;
    [HideInInspector] public Transform[] exitPoints;
    [HideInInspector] public QuestHandler questhandler;

    public TextMeshProUGUI statusText;

    private NavMeshAgent agent;
    private Animator animator;

    private Muur targetMuur;
    private bool hasBought = false;
    private bool isLeaving = false;

    private int spawnPointIndex = -1;

    public void Initialize(Transform[] wallPositions, Muur[] muren, MoneyManager moneyManager, Transform[] exitPoints, int spawnIndex, QuestHandler questhandler)
    {
        this.wallPositions = wallPositions;
        this.muren = muren;
        this.moneyManager = moneyManager;
        this.questhandler = questhandler;
        this.exitPoints = exitPoints;
        this.spawnPointIndex = spawnIndex;

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (statusText != null)
            statusText.text = "";

        if (Random.value > 0.5f)
        {
            spawnPointIndex = -1;
            GoToRandomMuur();
        }
        else
        {
            hasBought = true;
            isLeaving = true;
            Leave();
        }
    }

    private void Update()
    {
        if (agent == null) return;

        // aangekomen bij muur
        if (!hasBought && !isLeaving && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            BuyFromMuur();
        }

        // aangekomen bij exit
        if (isLeaving && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            Destroy(gameObject);
        }
    }

    void GoToRandomMuur()
    {
        System.Collections.Generic.List<int> availableMuren = new();

        for (int i = 0; i < muren.Length; i++)
        {
            if (muren[i] != null && muren[i].HasAvailableItems())
                availableMuren.Add(i);
        }

        if (availableMuren.Count == 0)
        {
            hasBought = true;
            isLeaving = true;
            Leave();
            return;
        }

        int random = availableMuren[Random.Range(0, availableMuren.Count)];
        targetMuur = muren[random];

        agent.SetDestination(wallPositions[random].position);
    }

    void BuyFromMuur()
    {
        hasBought = true;

        agent.isStopped = true;

        if (animator != null)
        {
            animator.SetTrigger("Grab");
        }

        Invoke(nameof(FinishBuying), 2f);
    }

    void FinishBuying()
    {
        Item item = targetMuur.GetRandomAvailableItem();

        if (item != null)
        {
            moneyManager.GiveMoney(item.Cost * 1.2f);
            questhandler.SellSnack(1);

            if (statusText != null)
                statusText.text = "Hmm lekker " + item.Name;
        }
        else
        {
            if (statusText != null)
                statusText.text = "brev er lig niks";
        }

        agent.isStopped = false;

        Leave();
    }

    void Leave()
    {
        isLeaving = true;

        System.Collections.Generic.List<int> availableExits = new();

        for (int i = 0; i < exitPoints.Length; i++)
        {
            if (i != spawnPointIndex)
                availableExits.Add(i);
        }

        int random = availableExits[Random.Range(0, availableExits.Count)];

        Vector3 exitPos = exitPoints[random].position;
        exitPos += new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

        agent.SetDestination(exitPos);
    }
}