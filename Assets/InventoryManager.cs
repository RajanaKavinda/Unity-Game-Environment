using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryUI; // Reference to the Inventory UI panel

    // Start is called before the first frame update
    void Start()
    {
        // Hide the inventory UI panel when the game starts
        HideInventoryUI();
    }

    // Function to show the inventory UI panel
    public void ShowInventoryUI()
    {
        // Activate the inventory UI panel
        inventoryUI.SetActive(true);
    }

    // Function to hide the inventory UI panel
    public void HideInventoryUI()
    {
        // Deactivate the inventory UI panel
        inventoryUI.SetActive(false);
    }
}
