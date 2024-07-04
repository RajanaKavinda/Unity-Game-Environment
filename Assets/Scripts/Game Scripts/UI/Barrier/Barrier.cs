using UnityEngine;
using UnityEngine.Events;

public class Barrier : MonoBehaviour
{
    public string barrierID; // Unique identifier for the barrier
    private PlayerController playerController; // Reference to the player controller
    public int requiredMarks; // Marks required to destroy the barrier
    public int gemCost; // Cost in gems to unlock the barrier
    public GameObject gemPurchasePanel; // UI panel for gem purchase
    private int boughtLands; // Number of lands bought by the player
    public bool IsDestroyed { get; private set; } // State of the barrier

    // Event triggered when a barrier is destroyed
    public static event UnityAction<string> OnBarrierDestroyed;

    private void Start()
    {
        playerController = PlayerController.Instance; // Get the player controller instance
        if (playerController == null)
        {
            Debug.LogError("PlayerController instance not found!");
        }

        LoadBarrierState(); // Load the barrier's state from player preferences
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the player collides with the barrier
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if the player has enough marks to pass
            if (playerController.GetQuizMarks() >= requiredMarks)
            {
                Debug.Log("Player has enough marks to pass");
                DestroyBarrier(); // Destroy the barrier
            }
            else
            {
                Debug.Log("You need more marks to access this area!");
                ShowGemPurchasePanel(); // Show the gem purchase panel
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Hide the gem purchase panel when the player exits the barrier's collision area
        if (collision.gameObject.CompareTag("Player"))
        {
            HideGemPurchasePanel();
        }
    }

    // Method to destroy the barrier
    private void DestroyBarrier()
    {
        IsDestroyed = true; // Set the barrier as destroyed
        PlayerPrefs.SetInt(barrierID, IsDestroyed ? 1 : 0); // Save the state
        GetComponent<Collider2D>().enabled = false; // Disable the collider
        gameObject.SetActive(false); // Deactivate the game object

        // Trigger the event to notify listeners that the barrier is destroyed
        OnBarrierDestroyed?.Invoke(barrierID);
    }

    // Method to show the gem purchase panel
    private void ShowGemPurchasePanel()
    {
        if (gemPurchasePanel != null)
        {
            GemPurchasePanel gemPurchasePanelScript = gemPurchasePanel.GetComponent<GemPurchasePanel>();
            if (gemPurchasePanelScript != null)
            {
                gemPurchasePanelScript.SetBarrier(this); // Set the barrier reference
                gemPurchasePanelScript.Show(); // Show the gem purchase panel
            }
        }
    }

    // Method to hide the gem purchase panel
    private void HideGemPurchasePanel()
    {
        if (gemPurchasePanel != null)
        {
            GemPurchasePanel gemPurchasePanelScript = gemPurchasePanel.GetComponent<GemPurchasePanel>();
            if (gemPurchasePanelScript != null)
            {
                gemPurchasePanelScript.Hide(); // Hide the gem purchase panel
            }
        }
    }

    // Method to load the barrier state
    public void LoadBarrierState()
    {
        // Check if the barrier was previously destroyed
        if (PlayerPrefs.GetInt(barrierID, 0) == 1)
        {
            IsDestroyed = true; // Set the barrier as destroyed
            GetComponent<Collider2D>().enabled = false; // Disable the collider
            gameObject.SetActive(false); // Deactivate the game object
            OnBarrierDestroyed?.Invoke(barrierID); // Ensure event is triggered on load
        }
    }

    // Method to unlock the barrier with gems
    public void UnlockWithGems()
    {
        DestroyBarrier(); // Destroy the barrier
        if (PlayerPrefs.HasKey("BoughtLands"))
        {
            boughtLands = PlayerPrefs.GetInt("BoughtLands");
            boughtLands += 1; // Increment the number of lands bought
            PlayerPrefs.SetInt("BoughtLands", boughtLands); // Save the updated count
        }
        else
        {
            boughtLands = 1; // Initialize the count if not present
            PlayerPrefs.SetInt("BoughtLands", boughtLands); // Save the count
        }
    }
}
