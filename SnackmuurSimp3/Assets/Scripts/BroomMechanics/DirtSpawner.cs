using UnityEngine;

public class DirtSpawner : MonoBehaviour
{
    public GameObject dirtPrefab;
    public Vector3 areaSize = new Vector3(5, 1, 5);
    public float minSpawnTime = 5f;
    public float maxSpawnTime = 15f;
    public int maxDirt = 3;

    private int currentDirtCount = 0;

    private void Start()
    {
        ScheduleNextSpawn();
    }

    void ScheduleNextSpawn()
    {
        float randomTime = Random.Range(minSpawnTime, maxSpawnTime);
        Invoke(nameof(SpawnDirt), randomTime);
    }

    void SpawnDirt()
    {
        if (currentDirtCount < maxDirt)
        {
            Vector3 randomPos = transform.position + new Vector3(
                Random.Range(-areaSize.x / 2, areaSize.x / 2),
                0,
                Random.Range(-areaSize.z / 2, areaSize.z / 2)
            );

            GameObject dirt = Instantiate(dirtPrefab, randomPos, Quaternion.identity);
            currentDirtCount++;

            dirt.GetComponent<Dirt>().OnCleaned += () => currentDirtCount--;
        }

        ScheduleNextSpawn();
    }
}