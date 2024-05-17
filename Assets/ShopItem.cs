using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public int cost = 0; // Cost of the item
    public Text coinsText; // Reference to the coins text in the shop panel
    public int itemType; // Type of the item
    public InventoryManager inventoryManager; // Reference to the InventoryManager

    // Start is called before the first frame update
    void Start()
    {
        // Update the coins text with the initial coin count at the start
        if (coinsText != null)
        {
            coinsText.text = "Coins: " + CoinManager.currentCoins.ToString();
        }
    }

    // Function to purchase the item
    public void PurchaseItem()
    {
        // Check if the player has enough coins to purchase the item
        if (CoinManager.currentCoins >= cost)
        {
            // Deduct the cost from the available coins
            CoinManager.DecreaseCoins(cost);

            // Update the coins text in the shop panel
            if (coinsText != null)
            {
                coinsText.text = "Coins: " + CoinManager.currentCoins.ToString();
            }

            // Increase the count of the corresponding item type in the inventory
            inventoryManager.IncreaseItemCount(itemType);

            // Update the inventory UI
            inventoryManager.UpdateInventoryUI();

            // TODO: Add code to activate the purchased item or perform other actions
        }
        else
        {
            Debug.Log("Not enough coins to purchase this item!");
            // TODO: Add code to display a message indicating insufficient coins
        }
    }
}
