using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneNavigator : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("Play").GetComponent<Button>().onClick.AddListener(GoToGameScene);
        GameObject.Find("ViewProfile").GetComponent<Button>().onClick.AddListener(GoToProfilePage);
        GameObject.Find("Marks").GetComponent<Button>().onClick.AddListener(GoToMarksScene);

       
    }

    public void GoToGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void GoToMarksScene()
    {
        SceneManager.LoadScene("Marks");
    }

    public void GoToProfilePage()
    {
        SceneManager.LoadScene("View Player Profile");
    }
}
