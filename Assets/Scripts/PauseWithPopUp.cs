using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseWithPopUp : MonoBehaviour
{
    // Reference to the pop-up panel game object
    public GameObject PopUpPanel;

    // Reference to the pause button
    public Button PauseButton;

    void Start()
    {
        // Deactivate the pop-up panel initially
        PopUpPanel.SetActive(false);

        // Add listeners to the buttons in the pop-up panel
        PopUpPanel.transform.Find("Save & Quit").GetComponent<Button>().onClick.AddListener(SaveAndQuit);
        PopUpPanel.transform.Find("Resume").GetComponent<Button>().onClick.AddListener(ResumeGame);

        // Add listener to the pause button
        if (PauseButton != null)
        {
            PauseButton.onClick.AddListener(PopUp);
        }
        else
        {
            Debug.LogError("Pause button not assigned!");
        }
    }

    // Pause the game and activate the pop-up panel
    public void PopUp()
    {
        Time.timeScale = 0f;
        PopUpPanel.SetActive(true);
    }

    // Resume the game and deactivate the pop-up panel
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        PopUpPanel.SetActive(false);
    }

    // Save the game state and load the main menu
    public void SaveAndQuit()
    {
        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.SaveGame();
            StartCoroutine(LoadMainMenu());
        }
        else
        {
            Debug.LogError("SaveManager instance not found!");
        }
    }

    // Coroutine to load the main menu asynchronously
    private IEnumerator LoadMainMenu()
    {
        Time.timeScale = 1f;
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene("Main Menu");
    }
}
