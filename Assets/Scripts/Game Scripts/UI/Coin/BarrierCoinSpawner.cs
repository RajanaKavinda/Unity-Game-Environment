using System.Collections;
using UnityEngine;

public class BarrierCoinSpawner : MonoBehaviour
{
    // Prefab of the coin to spawn
    public GameObject coinPrefab;

    // Delay range for spawning coins
    public float minSpawnDelay = 2f;
    public float maxSpawnDelay = 10f;

    // Time for coins to disappear
    public float coinDisappearTime = 8f;

    // Array of spawn points for coins
    public Transform[] spawnPoints;

    // Range of coins to spawn
    public int minCoinsToSpawn = 1;
    public int maxCoinsToSpawn = 4;

    // Identifier for the barrier
    public string barrierID;

    // Flag to determine if coins can spawn
    private bool canSpawn = false;

    // Subscribe to the Barrier destroyed event on enable
    private void OnEnable()
    {
        Barrier.OnBarrierDestroyed += HandleBarrierDestroyed;
    }

    // Unsubscribe from the Barrier destroyed event on disable
    private void OnDisable()
    {
        Barrier.OnBarrierDestroyed -= HandleBarrierDestroyed;
    }

    // Check the state of the barrier when the object is enabled
    private void Start()
    {
        CheckBarrierState();
    }

    // Check if the barrier is destroyed
    private void CheckBarrierState()
    {
        // If the barrier is destroyed, enable spawning coins
        if (PlayerPrefs.GetInt(barrierID, 0) == 1)
        {
            canSpawn = true;
            StartCoroutine(SpawnCoinsRoutine());
        }
    }

    // Handle the event when the barrier is destroyed
    private void HandleBarrierDestroyed(string id)
    {
        // If the destroyed barrier matches this barrier's ID, enable spawning coins
        if (id == barrierID)
        {
            canSpawn = true;
            StartCoroutine(SpawnCoinsRoutine());
        }
    }

    // Coroutine for spawning coins
    private IEnumerator SpawnCoinsRoutine()
    {
        // Keep spawning coins as long as canSpawn flag is true
        while (canSpawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay)); // Wait for a random delay
            SpawnCoins(Random.Range(minCoinsToSpawn, maxCoinsToSpawn + 1)); // Spawn a random number of coins
        }
    }

    // Spawn a given number of coins
    private void SpawnCoins(int numberOfCoins)
    {
        if (spawnPoints.Length == 0) // If there are no spawn points, return
            return;

        for (int i = 0; i < numberOfCoins; i++)
        {
            Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position; // Randomly select a spawn point
            GameObject newCoin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity); // Instantiate a coin at the spawn point
            Destroy(newCoin, coinDisappearTime); // Destroy the coin after a set time
        }
    }
}
