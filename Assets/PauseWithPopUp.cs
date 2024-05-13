using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseWithPopUp : MonoBehaviour
{
    public GameObject PopUpPanel;
    private PlayerController playerController;

    void Start()
    {
        // Deactivate the pop-up panel initially
        PopUpPanel.SetActive(false);

        // Find the buttons and add listeners
        PopUpPanel.transform.Find("Save & Quit").GetComponent<Button>().onClick.AddListener(SaveAndQuit);
        PopUpPanel.transform.Find("Resume").GetComponent<Button>().onClick.AddListener(ResumeGame);

        // Find and store reference to the PlayerController
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        // Check for input to pause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PopUp();
        }
    }

    public void PopUp()
    {
        Time.timeScale = 0f; // Pause the game
        // Activate the pop-up panel
        PopUpPanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game
        // Deactivate the pop-up panel
        PopUpPanel.SetActive(false);
    }

    public void SaveAndQuit()
    {
        // Save game state here
        SaveGameState();

        // Quit the application
        Application.Quit();

        // Load the Main Menu scene
        SceneManager.LoadScene("Main Menu");
    }


    private void SaveGameState()
    {
        if (playerController != null)
        {
            // Save player position
            Vector3 playerPosition = playerController.transform.position;
            PlayerPrefs.SetFloat("PlayerX", playerPosition.x);
            PlayerPrefs.SetFloat("PlayerY", playerPosition.y);
            PlayerPrefs.SetFloat("PlayerZ", playerPosition.z);

            // Save score
            PlayerPrefs.SetInt("Score", playerController.GetCoinCount());
            Debug.Log("Score saved: " + playerController.GetCoinCount());
            PlayerPrefs.Save(); // Save PlayerPrefs data to disk
            Debug.Log("Game state saved.");
        }
        else
        {
            Debug.LogWarning("PlayerController not found. Cannot save game state.");
        }
    }
}


