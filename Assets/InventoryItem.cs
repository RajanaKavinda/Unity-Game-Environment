using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public string itemType; // Type of the item
    public int itemCount; // Count of this item in the inventory

    // Function to initialize the item with its type and count
    public void Initialize(string type, int count)
    {
        itemType = type;
        itemCount = count;
    }

    // Function to increase the count of this item in the inventory
    public void IncreaseCount(int amount)
    {
        itemCount += amount;
    }

    // Function to decrease the count of this item in the inventory
    public void DecreaseCount(int amount)
    {
        itemCount -= amount;
        if (itemCount < 0)
        {
            itemCount = 0;
        }
    }
}
