using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Text[] itemTexts; // Array of UI Text elements to display item counts
    private int[] itemCounts; // Array to store the count of each item type

    void Start()
    {
        itemCounts = new int[] { 0, 0, 0, 0, 0 };
        UpdateInventoryUI();
    }

    public void IncreaseItemCount(int itemType)
    {
        if (itemType >= 0 && itemType < itemCounts.Length)
        {
            itemCounts[itemType]++;
            UpdateInventoryUI();
        }
        else
        {
            Debug.LogError("Item type not found in inventory!");
        }
    }

    public void DecreaseItemCount(int itemType)
    {
        if (itemType >= 0 && itemType < itemCounts.Length)
        {
            if (itemCounts[itemType] > 0)
            {
                itemCounts[itemType]--;
                UpdateInventoryUI();
            }
            else
            {
                Debug.LogError("No items left to decrease!");
            }
        }
        else
        {
            Debug.LogError("Item type not found in inventory!");
        }
    }

    public int GetItemCount(int itemType)
    {
        if (itemType >= 0 && itemType < itemCounts.Length)
        {
            return itemCounts[itemType];
        }
        else
        {
            Debug.LogError("Item type not found in inventory!");
            return 0;
        }
    }

    // Update the inventory UI with the current item counts
    public void UpdateInventoryUI()
    {
        for (int i = 0; i < itemCounts.Length; i++)
        {
            if (itemTexts.Length > i && itemTexts[i] != null)
            {
                itemTexts[i].text = itemCounts[i].ToString();
            }
        }
    }
}
