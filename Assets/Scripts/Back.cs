using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackToMenu : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("Back").GetComponent<Button>().onClick.AddListener(GoToMainMenu);



    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    

}
