using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    [HideInInspector] public Transform[] wallPositions;
    [HideInInspector] public Muur[] muren;
    [HideInInspector] public MoneyManager moneyManager;
    [HideInInspector] public Transform[] exitPoints;

    private NavMeshAgent agent;
    private Muur targetMuur;
    private bool hasBought = false;
    private bool isLeaving = false;
    private int spawnPointIndex = -1;

    public void Initialize(Transform[] wallPositions, Muur[] muren, MoneyManager moneyManager, Transform[] exitPoints, int spawnIndex)
    {
        this.wallPositions = wallPositions;
        this.muren = muren;
        this.moneyManager = moneyManager;
        this.exitPoints = exitPoints;
        this.spawnPointIndex = spawnIndex;
        agent = GetComponent<NavMeshAgent>();

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

        if (!hasBought && !isLeaving && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            BuyFromMuur();
        }

        if (isLeaving && !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            Destroy(gameObject);
        }
    }

    void GoToRandomMuur()
    {
        int random = Random.Range(0, muren.Length);
        targetMuur = muren[random];
        agent.SetDestination(wallPositions[random].position);
    }

    void BuyFromMuur()
    {
        hasBought = true;
        Item item = targetMuur.GetRandomAvailableItem();
        if (item != null)
        {
            moneyManager.GiveMoney(item.Cost);
          //  Debug.Log("NPC bought: " + item.Name + " for €" + item.Cost);
        }
        else
        {
           // Debug.Log("No items available!");
        }
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