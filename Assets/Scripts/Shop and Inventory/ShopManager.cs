using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{

    public GameObject shopUI; // Reference to the Shop UI panel

    // Start is called before the first frame update
    void Start()
    {
        // Hide the shop UI panel when the game starts
        HideShopUI();
    }

    // Function to show the shop UI panel
    public void ShowShopUI()
    {
        // Pause the game
        Time.timeScale = 0;

        // Activate the shop UI panel
        shopUI.SetActive(true);
    }

    // Function to hide the shop UI panel
    public void HideShopUI()
    {
        // Resume the game
        Time.timeScale = 1;

        // Deactivate the shop UI panel
        shopUI.SetActive(false);
    }
}
