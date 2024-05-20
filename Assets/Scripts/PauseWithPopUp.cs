using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseWithPopUp : MonoBehaviour
{
    public GameObject PopUpPanel;
    public Button PauseButton; 

    void Start()
    {
        PopUpPanel.SetActive(false);
        PopUpPanel.transform.Find("Save & Quit").GetComponent<Button>().onClick.AddListener(SaveAndQuit);
        PopUpPanel.transform.Find("Resume").GetComponent<Button>().onClick.AddListener(ResumeGame);

        if (PauseButton != null)
        {
            PauseButton.onClick.AddListener(PopUp);
        }
        else
        {
            Debug.LogError("Pause button not assigned!");
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

    private IEnumerator LoadMainMenu()
    {
        Time.timeScale = 1f;
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene("Main Menu");
    }
}
