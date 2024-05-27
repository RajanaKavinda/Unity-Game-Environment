using UnityEngine;

public class Barrier : MonoBehaviour
{
    public string barrierID;
    private PlayerController playerController;
    public int requiredMarks;
    public int gemCost;
    public GameObject gemPurchasePanel;
    private int boughtLands;
    public bool IsDestroyed { get; private set; }

    private void Start()
    {
        playerController = PlayerController.Instance;
        if (playerController == null)
        {
            Debug.LogError("PlayerController instance not found!");
        }

        LoadBarrierState();
    }

    // Method called when the player collides with the barrier
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerController.GetQuizMarks() >= requiredMarks)
            {
                Debug.Log("Player has enough marks to pass");
                DestroyBarrier();
            }
            else
            {
                Debug.Log("You need more marks to access this area!");
                ShowGemPurchasePanel(); // Show the gem purchase panel
            }
        }
    }

    // Method called when the player exits the barrier's collision area
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HideGemPurchasePanel(); // Hide the gem purchase panel
        }
    }

    // Method to destroy the barrier
    private void DestroyBarrier()
    {
        IsDestroyed = true; // Set the barrier as destroyed
        PlayerPrefs.SetInt(barrierID, IsDestroyed ? 1 : 0); // Save the state
        GetComponent<Collider2D>().enabled = false; // Disable the collider
        gameObject.SetActive(false); // Deactivate the game object

    }

    // Method to show the gem purchase panel
    private void ShowGemPurchasePanel()
    {
        if (gemPurchasePanel != null)
        {
            GemPurchasePanel gemPurchasePanelScript = gemPurchasePanel.GetComponent<GemPurchasePanel>();
            if (gemPurchasePanelScript != null)
            {
                gemPurchasePanelScript.SetBarrier(this); // Set the barrier
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
        if (PlayerPrefs.GetInt(barrierID, 0) == 1)
        {
            IsDestroyed = true; // Set the barrier as destroyed
            GetComponent<Collider2D>().enabled = false; // Disable the collider
            gameObject.SetActive(false); // Deactivate the game object
        }
    }

    // Method to unlock the barrier with gems
    public void UnlockWithGems()
    {
        DestroyBarrier(); // Destroy the barrier
        if (PlayerPrefs.HasKey("BoughtLands"))
        {
            boughtLands = PlayerPrefs.GetInt("BoughtLands");
            boughtLands += 1;
            PlayerPrefs.SetInt("BoughtLands", boughtLands);
        }
        else
        {
            boughtLands = 1;
            PlayerPrefs.SetInt("BoughtLands", boughtLands);
        }
    }
}
