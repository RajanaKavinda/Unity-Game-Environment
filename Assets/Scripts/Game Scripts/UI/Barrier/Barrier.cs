using System.Threading;
using UnityEngine;

public class Barrier : MonoBehaviour
{
    public string barrierID;
    private PlayerController playerController;
    public int requiredMarks;
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
                IsDestroyed = true;
                PlayerPrefs.SetInt(barrierID, IsDestroyed ? 1 : 0);
                GetComponent<Collider2D>().enabled = false; 
                gameObject.SetActive(false); 
            }
            else
            {
                Debug.Log("You need more marks to access this area!");
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
        }
    }

}
