using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneNavigator : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("Play").GetComponent<Button>().onClick.AddListener(GoToGameScene);
        GameObject.Find("ViewProfile").GetComponent<Button>().onClick.AddListener(GoToProfilePage);
        GameObject.Find("QuizReview").GetComponent<Button>().onClick.AddListener(GoToQuizScene);
    }

    public void GoToGameScene()
    {
        SceneManager.LoadScene("Game Env");
    }

    public void GoToQuizScene()
    {
        string url = "http://localhost:3000/" + GetMethod.jwtToken2 + "/" + GetMethod.userID;
        Application.OpenURL(url);
    }

    public void GoToProfilePage()
    {
        SceneManager.LoadScene("View Player Profile");
    }
}