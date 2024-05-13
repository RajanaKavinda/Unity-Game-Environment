using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public int cost = 0; // Cost of the item
    public Text coinsText; // Reference to the coins text in the shop panel
    public int currentCoins = 1000; // Current number of coins available

    // Start is called before the first frame update
    void Start()
    {
        // Initialize currentCoins if coinsText is assigned
        if (coinsText != null)
        {
            // Try parsing the text from coinsText, defaulting to 1000 if parsing fails
            if (!int.TryParse(coinsText.text, out currentCoins))
            {
                currentCoins = 1000;
            }
        }
    }

    // Function to purchase the item
    public void PurchaseItem()
    {
        // Check if the player has enough coins to purchase the item
        if (currentCoins >= cost)
        {
            // Deduct the cost from the available coins
            currentCoins -= cost;

            // Update the coins text in the shop panel
            if (coinsText != null)
            {
                coinsText.text = "Coins: " + currentCoins.ToString();
            }

            // TODO: Add code to activate the purchased item or perform other actions
        }
        else
        {
            Debug.Log("Not enough coins to purchase this item!");
            // TODO: Add code to display a message indicating insufficient coins
        }
    }
}
