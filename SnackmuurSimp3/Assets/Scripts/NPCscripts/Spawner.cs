using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] npcPrefabs;
    public Transform[] spawnPoints;
    public float minSpawnTime = 3f;
    public float maxSpawnTime = 8f;
    public float spawnOffset = 1f;

    [Header("NPC References")]
    public Transform[] wallPositions;
    public Muur[] muren;
    public MoneyManager moneyManager;
    public Transform[] exitPoints;

    private void Start()
    {
        ScheduleNextSpawn();
    }

    void ScheduleNextSpawn()
    {
        float randomTime = Random.Range(minSpawnTime, maxSpawnTime);
        Invoke(nameof(SpawnNPC), randomTime);
    }

    void SpawnNPC()
    {
        int randomSpawn = Random.Range(0, spawnPoints.Length);
        Vector3 spawnPos = spawnPoints[randomSpawn].position;
        spawnPos += new Vector3(Random.Range(-spawnOffset, spawnOffset), 0, Random.Range(-spawnOffset, spawnOffset));

        int randomNPC = Random.Range(0, npcPrefabs.Length);
        GameObject newNPC = Instantiate(npcPrefabs[randomNPC], spawnPos, Quaternion.identity);

        NPC npcScript = newNPC.GetComponent<NPC>();
        npcScript.Initialize(wallPositions, muren, moneyManager, exitPoints, randomSpawn);

        ScheduleNextSpawn();
    }
}