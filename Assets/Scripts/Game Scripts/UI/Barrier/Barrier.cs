using UnityEngine;

public class Barrier : MonoBehaviour
{
    public string barrierID;
    private PlayerController playerController;
    public int requiredMarks;
    public int gemCost; // Cost in gems to unlock the barrier
    public GameObject gemPurchasePanel; // Reference to the gem purchase panel

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

    private void DestroyBarrier()
    {
        IsDestroyed = true;
        PlayerPrefs.SetInt(barrierID, IsDestroyed ? 1 : 0);
        GetComponent<Collider2D>().enabled = false;
        gameObject.SetActive(false);
    }

    private void ShowGemPurchasePanel()
    {
        gemPurchasePanel.SetActive(true);
        gemPurchasePanel.GetComponent<GemPurchasePanel>().SetBarrier(this);
    }

    public void LoadBarrierState()
    {
        if (PlayerPrefs.GetInt(barrierID, 0) == 1)
        {
            IsDestroyed = true;
            GetComponent<Collider2D>().enabled = false;
            gameObject.SetActive(false);
        }
    }

    public void UnlockWithGems()
    {
        DestroyBarrier();
    }
}
