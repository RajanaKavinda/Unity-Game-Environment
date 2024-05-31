using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject blueSlimePrefab;  // Prefab of the Blue Slime
    public Transform[] spawnPoints;     // Spawn points for the Blue Slimes
    public string barrierID;            // ID of the barrier that controls the spawning

    private bool canSpawn = false;      // Indicates if spawning is allowed

    private void OnEnable()
    {
        // Subscribe to the barrier destroyed event
        Barrier.OnBarrierDestroyed += HandleBarrierDestroyed;
    }

    private void OnDisable()
    {
        // Unsubscribe from the barrier destroyed event
        Barrier.OnBarrierDestroyed -= HandleBarrierDestroyed;
    }

    private void Start()
    {
        CheckBarrierState(); // Check the initial state of the barrier
    }

    private void CheckBarrierState()
    {
        // Check if the barrier is already destroyed by retrieving its state from PlayerPrefs
        if (PlayerPrefs.GetInt(barrierID, 0) == 1)
        {
            canSpawn = true; // Allow spawning
            SpawnBlueSlimes(); // Spawn Blue Slimes
        }
    }

    private void HandleBarrierDestroyed(string id)
    {
        // Check if the destroyed barrier is the one controlling this spawner
        if (id == barrierID)
        {
            canSpawn = true; // Allow spawning
            SpawnBlueSlimes(); // Spawn Blue Slimes
        }
    }

    private void SpawnBlueSlimes()
    {
        // Check if there are spawn points defined and spawning is allowed
        if (spawnPoints.Length == 0 || !canSpawn)
            return;

        // Instantiate Blue Slime prefabs at each spawn point
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject newBlueSlime = Instantiate(blueSlimePrefab, spawnPoint.position, Quaternion.identity);
            newBlueSlime.SetActive(true); // Ensure the spawned Blue Slime is active
        }
    }
}
