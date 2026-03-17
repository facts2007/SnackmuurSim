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

    public void Initialize(Transform[] wallPositions, Muur[] muren, MoneyManager moneyManager, Transform[] exitPoints)
    {
        this.wallPositions = wallPositions;
        this.muren = muren;
        this.moneyManager = moneyManager;
        this.exitPoints = exitPoints;

        agent = GetComponent<NavMeshAgent>();

        var random = Random.value;
        print("Random value: " + random);
        if (random > 0.5f)
        {
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
        int random = Random.Range(0, exitPoints.Length);
        Vector3 exitPos = exitPoints[random].position;
        exitPos += new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
        agent.SetDestination(exitPos);
    }
}