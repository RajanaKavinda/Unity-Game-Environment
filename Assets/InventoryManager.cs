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
    }


    public void IncreaseItemCount(int itemType)
    {
        Debug.LogError("Entered IncreaseItemCount");
        int index = itemType;
        Debug.LogError(index != -1);
        if (index != -1)
        {   

            itemCounts[index]++;
            Debug.LogError(itemCounts);

        }
        else
        {
            Debug.LogError("Item type not found in inventory!");
        }
    }

    // Update the inventory UI with the current item counts
    public void UpdateInventoryUI()
    {
        if (itemTexts == null)
        {
            Debug.LogError("Item texts array is not initialized!");
            return;
        }

        if (itemTexts.Length != itemCounts.Length)
        {
            Debug.LogError("Item texts array length does not match item counts array length!");
            return;
        }

        for (int i = 0; i < itemCounts.Length; i++)
        {
            itemTexts[i].text = itemCounts[i].ToString();
        }
    }

    
    

}
