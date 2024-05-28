using System.Collections;
using UnityEngine;

public class BarrierCoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float minSpawnDelay = 2f;
    public float maxSpawnDelay = 10f;
    public float coinDisappearTime = 8f;
    public Transform[] spawnPoints;

    public int minCoinsToSpawn = 1;
    public int maxCoinsToSpawn = 4;

    public string barrierID;

    private bool canSpawn = false;

    private void OnEnable()
    {
        Barrier.OnBarrierDestroyed += HandleBarrierDestroyed;
    }

    private void OnDisable()
    {
        Barrier.OnBarrierDestroyed -= HandleBarrierDestroyed;
    }

    private void Start()
    {
        CheckBarrierState();
    }

    private void CheckBarrierState()
    {
        if (PlayerPrefs.GetInt(barrierID, 0) == 1)
        {
            canSpawn = true;
            StartCoroutine(SpawnCoinsRoutine());
        }
    }

    private void HandleBarrierDestroyed(string id)
    {
        if (id == barrierID)
        {
            canSpawn = true;
            StartCoroutine(SpawnCoinsRoutine());
        }
    }

    private IEnumerator SpawnCoinsRoutine()
    {
        while (canSpawn)
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
