using UnityEngine;

public class DirtSpawner : MonoBehaviour
{
    public GameObject dirtPrefab;
    public int amountToSpawn = 10;

    public Vector3 areaSize = new Vector3(5, 1, 5);

    void Start()
    {
        SpawnDirt();
    }

    void SpawnDirt()
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            Vector3 randomPos = transform.position + new Vector3(
                Random.Range(-areaSize.x / 2, areaSize.x / 2),
                0,
                Random.Range(-areaSize.z / 2, areaSize.z / 2)
            );

            Instantiate(dirtPrefab, randomPos, Quaternion.identity);
        }
    }
}