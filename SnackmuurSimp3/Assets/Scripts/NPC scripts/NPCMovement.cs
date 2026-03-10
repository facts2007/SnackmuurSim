using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public Transform[] walls; // drag your 2 wall spots in here
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoToRandomWall();
    }

    void GoToRandomWall()
    {
        int randomIndex = Random.Range(0, walls.Length);
        agent.SetDestination(walls[randomIndex].position);
    }
}