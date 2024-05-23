using UnityEngine;
using UnityEngine.UI;

public class GemPurchasePanel : MonoBehaviour
{
    private Barrier barrier;
    public Text gemCostText;
    public Button purchaseButton;

    private void Start()
    {
        gameObject.SetActive(false); // Initially hide the panel
        if (purchaseButton != null)
        {
            purchaseButton.onClick.AddListener(OnPurchaseButtonClicked); // Add listener to purchase button
        }
    }

    // Method to set the barrier and update the panel texts
    public void SetBarrier(Barrier barrier)
    {
        this.barrier = barrier; // Set the barrier
        if (gemCostText != null)
        {
            gemCostText.text = barrier.gemCost.ToString(); // Update the gem cost text
            Debug.Log(gemCostText.text);
        }
    }

    // Method called when the purchase button is clicked
    private void OnPurchaseButtonClicked()
    {
        if (barrier == null) return;

        // Check if the player has enough gems
        if (GemManager.Instance.UseGems(barrier.gemCost))
        {
            barrier.UnlockWithGems(); // Unlock the barrier
            Hide(); // Hide the panel after purchase
        }
        else
        {
            Debug.Log("Not enough gems!"); // Log error if not enough gems
        }
    }

    // Method to show the panel
    public void Show()
    {
        gameObject.SetActive(true); // Show the panel
    }

    // Method to hide the panel
    public void Hide()
    {
        gameObject.SetActive(false); // Hide the panel
    }
}
