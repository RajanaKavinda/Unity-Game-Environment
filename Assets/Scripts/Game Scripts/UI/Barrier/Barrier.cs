using UnityEngine;
using UnityEngine.Events;

public class Barrier : MonoBehaviour
{
    public string barrierID;
    private PlayerController playerController;
    public int requiredMarks;
    public int gemCost;
    public GameObject gemPurchasePanel;

    public bool IsDestroyed { get; private set; }

    public static event UnityAction<string> OnBarrierDestroyed;

    private void Start()
    {
        playerController = PlayerController.Instance;
        if (playerController == null)
        {
            Debug.LogError("PlayerController instance not found!");
        }

        LoadBarrierState();
    }

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
                ShowGemPurchasePanel();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HideGemPurchasePanel();
        }
    }

    private void DestroyBarrier()
    {
        IsDestroyed = true;
        PlayerPrefs.SetInt(barrierID, IsDestroyed ? 1 : 0);
        GetComponent<Collider2D>().enabled = false;
        gameObject.SetActive(false);

        OnBarrierDestroyed?.Invoke(barrierID);
    }

    private void ShowGemPurchasePanel()
    {
        if (gemPurchasePanel != null)
        {
            GemPurchasePanel gemPurchasePanelScript = gemPurchasePanel.GetComponent<GemPurchasePanel>();
            if (gemPurchasePanelScript != null)
            {
                gemPurchasePanelScript.SetBarrier(this);
                gemPurchasePanelScript.Show();
            }
        }
    }

    private void HideGemPurchasePanel()
    {
        if (gemPurchasePanel != null)
        {
            GemPurchasePanel gemPurchasePanelScript = gemPurchasePanel.GetComponent<GemPurchasePanel>();
            if (gemPurchasePanelScript != null)
            {
                gemPurchasePanelScript.Hide();
            }
        }
    }

    public void LoadBarrierState()
    {
        if (PlayerPrefs.GetInt(barrierID, 0) == 1)
        {
            IsDestroyed = true;
            GetComponent<Collider2D>().enabled = false;
            gameObject.SetActive(false);
            OnBarrierDestroyed?.Invoke(barrierID); // Ensure event is triggered on load
        }
    }

    public void UnlockWithGems()
    {
        DestroyBarrier();
    }
}
