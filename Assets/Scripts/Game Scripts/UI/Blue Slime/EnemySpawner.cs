using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject blueSlimePrefab;  // Prefab of the Blue Slime
    public Transform[] spawnPoints;     // Spawn points for the Blue Slimes
    public string barrierID;            // ID of the barrier that controls the spawning

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
            SpawnBlueSlimes();
        }
    }

    private void HandleBarrierDestroyed(string id)
    {
        if (id == barrierID)
        {
            canSpawn = true;
            SpawnBlueSlimes();
        }
    }

    private void SpawnBlueSlimes()
    {
        if (spawnPoints.Length == 0 || !canSpawn)
            return;

        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject newBlueSlime = Instantiate(blueSlimePrefab, spawnPoint.position, Quaternion.identity);
            newBlueSlime.SetActive(true);
        }
    }
}
