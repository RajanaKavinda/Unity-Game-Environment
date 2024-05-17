using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackWithPopUp : MonoBehaviour
{
    public GameObject PopUpPanel;

    void Start()
    {
        // Deactivate the pop-up panel initially
        PopUpPanel.SetActive(false);

        // Find the buttons and add listeners
        GameObject.Find("BackToMainMenu").GetComponent<Button>().onClick.AddListener(PopUp);
        transform.Find("PopUpPanel").Find("BackPopUpPanel").GetComponent<Button>().onClick.AddListener(BackToMainMenu);
        transform.Find("PopUpPanel").Find("Play").GetComponent<Button>().onClick.AddListener(BackToGame);
    }

    public void PopUp()
    {
        // Activate the pop-up panel
        PopUpPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void BackToMainMenu()
    {
        // Load the Main Menu scene
        SceneManager.LoadScene("Main Menu");
    }

    public void BackToGame()
    {
        // Deactivate the pop-up panel
        PopUpPanel.SetActive(false);
        Time.timeScale = 1;
    }
}
