using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject npcPrefab;
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
        int random = Random.Range(0, spawnPoints.Length);
        Vector3 spawnPos = spawnPoints[random].position;
        spawnPos += new Vector3(Random.Range(-spawnOffset, spawnOffset), 0, Random.Range(-spawnOffset, spawnOffset));

        GameObject newNPC = Instantiate(npcPrefab, spawnPos, Quaternion.identity);
        NPC npcScript = newNPC.GetComponent<NPC>();
        npcScript.Initialize(wallPositions, muren, moneyManager, exitPoints); // pass all refs

        ScheduleNextSpawn();
    }
}