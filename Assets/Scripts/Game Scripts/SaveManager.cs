
using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }
    private List<GameObject> placedItems = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        SavePlayerState();
        SaveInventoryState();
        SavePlacedItemsState();
        SaveLastPlayedDate();
        PlayerPrefs.Save();
        Debug.Log("Game Saved.");
    }

    private void SavePlayerState()
    {
        PlayerController playerController = PlayerController.Instance;
        if (playerController != null)
        {
            Vector3 playerPosition = playerController.transform.position;
            PlayerPrefs.SetFloat("PlayerX", playerPosition.x);
            PlayerPrefs.SetFloat("PlayerY", playerPosition.y);
            PlayerPrefs.SetFloat("PlayerZ", playerPosition.z);
            PlayerPrefs.SetInt("Score", CoinManager.currentCoins);
            PlayerPrefs.SetInt("TotalGems", GemsManager.Instance.totalGems); // Save total gems
            Debug.Log("Saved player state.");
        }
        else
        {
            Debug.LogWarning("PlayerController not found. Cannot save player state.");
        }
    }

    private void SaveInventoryState()
    {
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager != null)
        {
            inventoryManager.SaveInventory();
        }
    }

    private void SavePlacedItemsState()
    {
        placedItems.RemoveAll(item => item == null);

        for (int i = 0; i < placedItems.Count; i++)
        {
            GameObject item = placedItems[i];
            if (item != null)
            {
                PlayerPrefs.SetString("PlacedItem_" + i + "_Type", item.name);
                PlayerPrefs.SetFloat("PlacedItem_" + i + "_X", item.transform.position.x);
                PlayerPrefs.SetFloat("PlacedItem_" + i + "_Y", item.transform.position.y);
            }
        }
        PlayerPrefs.SetInt("PlacedItemCount", placedItems.Count);
        Debug.Log("Placed Items Saved.");
    }

    private void SaveLastPlayedDate()
    {
        PlayerPrefs.SetString("LastPlayedDate", DateTime.Now.ToString("yyyy-MM-dd"));
    }

    public void LoadGame()
    {
        GemsManager.Instance.UpdateGems();
        LoadPlayerState();
        LoadBarrierStates();
        LoadInventoryState();
        LoadPlacedItemsState();
        Debug.Log("Game Loaded.");
    }

    private void LoadPlayerState()
    {
        PlayerController playerController = PlayerController.Instance;
        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
        {
            Vector3 playerPosition = new Vector3(
                PlayerPrefs.GetFloat("PlayerX"),
                PlayerPrefs.GetFloat("PlayerY"),
                PlayerPrefs.GetFloat("PlayerZ")
            );
            playerController.SetPlayerPosition(playerPosition);
        }

        if (PlayerPrefs.HasKey("Score"))
        {
            CoinManager.currentCoins = PlayerPrefs.GetInt("Score");
        }

        if (PlayerPrefs.HasKey("TotalGems"))
        {
            GemsManager.Instance.totalGems = PlayerPrefs.GetInt("TotalGems");
        }

        Debug.Log("Loaded player state.");
    }

    private void LoadBarrierStates()
    {
        Barrier[] barriers = FindObjectsOfType<Barrier>();
        foreach (Barrier barrier in barriers)
        {
            barrier.LoadBarrierState();
        }
        Debug.Log("Loaded barrier states.");
    }

    private void LoadInventoryState()
    {
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
        if (inventoryManager != null)
        {
            inventoryManager.LoadInventory();
        }
    }

    private void LoadPlacedItemsState()
    {
        int placedItemCount = PlayerPrefs.GetInt("PlacedItemCount", 0);

        for (int i = 0; i < placedItemCount; i++)
        {
            string itemType = PlayerPrefs.GetString("PlacedItem_" + i + "_Type");
            itemType = itemType.Replace("(Clone)", "").Trim();
            float x = PlayerPrefs.GetFloat("PlacedItem_" + i + "_X");
            float y = PlayerPrefs.GetFloat("PlacedItem_" + i + "_Y");

            Vector3 position = new Vector3(x, y, 0);

            GameObject itemPrefab = Resources.Load<GameObject>(itemType);
            if (itemPrefab != null)
            {
                GameObject newItem = Instantiate(itemPrefab, position, Quaternion.identity);
                placedItems.Add(newItem);
            }
        }
        Debug.Log("Placed Items Loaded.");
    }

    public int GetPlacedItemCount(string itemName)
    {
        int count = 0;
        foreach (GameObject item in placedItems)
        {
            if (item != null && item.name.Contains(itemName))
            {
                count++;
            }
        }
        return count;
    }

    public void AddPlacedItem(GameObject item)
    {
        placedItems.Add(item);
    }
}
