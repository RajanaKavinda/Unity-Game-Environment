using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float minSpawnDelay = 2f;
    public float maxSpawnDelay = 10f;
    public float coinDisappearTime = 8f;
    public Transform[] spawnPoints;

    public int minCoinsToSpawn = 1;
    public int maxCoinsToSpawn = 4;

    private void Start()
    {
        StartCoroutine(SpawnCoinsRoutine());
    }

    private IEnumerator SpawnCoinsRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
            SpawnCoins(Random.Range(minCoinsToSpawn, maxCoinsToSpawn + 1));
        }
    }

    private void SpawnCoins(int numberOfCoins)
    {
        if (spawnPoints.Length == 0)
            return;

        for (int i = 0; i < numberOfCoins; i++)
        {
            Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            GameObject newCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
            Destroy(newCoin, coinDisappearTime);
        }
    }
}
