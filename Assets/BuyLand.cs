using UnityEngine;
using UnityEngine.UI;

public class GemPurchasePanel : MonoBehaviour
{
    private Barrier barrier;
    public Text gemCostText;
    public Button purchaseButton;

    private void Start()
    {
        purchaseButton.onClick.AddListener(OnPurchaseButtonClicked);
    }

    public void SetBarrier(Barrier barrier)
    {
        this.barrier = barrier;
        gemCostText.text = "Cost: " + barrier.gemCost + " Gems";
    }

    private void OnPurchaseButtonClicked()
    {
        if (GemManager.Instance.UseGems(barrier.gemCost))
        {
            barrier.UnlockWithGems();
            gameObject.SetActive(false); // Hide the panel after purchase
        }
        else
        {
            Debug.Log("Not enough gems!");
        }
    }
}
