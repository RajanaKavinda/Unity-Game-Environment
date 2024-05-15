using System.Collections; // Add this directive
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseWithPopUp : MonoBehaviour
{
    public GameObject PopUpPanel;
    private PlayerController playerController;

    void Start()
    {
        PopUpPanel.SetActive(false);
        PopUpPanel.transform.Find("Save & Quit").GetComponent<Button>().onClick.AddListener(SaveAndQuit);
        PopUpPanel.transform.Find("Resume").GetComponent<Button>().onClick.AddListener(ResumeGame);

        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PopUp();
        }
    }

    public void PopUp()
    {
        Time.timeScale = 0f;
        PopUpPanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        PopUpPanel.SetActive(false);
    }

    public void SaveAndQuit()
    {
        SaveGameState();
        StartCoroutine(LoadMainMenu());
    }

    private void SaveGameState()
    {
        if (playerController != null)
        {
            Vector3 playerPosition = playerController.transform.position;
            PlayerPrefs.SetFloat("PlayerX", playerPosition.x);
            PlayerPrefs.SetFloat("PlayerY", playerPosition.y);
            PlayerPrefs.SetFloat("PlayerZ", playerPosition.z);
            PlayerPrefs.SetInt("Score", playerController.GetCoinCount());
            PlayerPrefs.Save();
        }
        else
        {
            Debug.LogWarning("PlayerController not found. Cannot save game state.");
        }
    }

    private IEnumerator LoadMainMenu()
    {
        Time.timeScale = 1f;
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene("Main Menu");
    }
}
