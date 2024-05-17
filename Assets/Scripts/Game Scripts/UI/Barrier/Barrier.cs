using UnityEngine;

public class Barrier : MonoBehaviour
{
    public int requiredMarks = 10;
    private PlayerController playerController;

    private void Start()
    {
        playerController = PlayerController.Instance;
        if (playerController == null)
        {
            Debug.LogError("PlayerController instance not found!");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerController.GetQuizMarks() >= requiredMarks)
            {
                Debug.Log("Player has enough marks to pass");
                Destroy(gameObject);
            }
            else
            {
                // Prevent access, maybe show a message
                Debug.Log("You need more marks to access this area!");
            }
        }
    }

}
