using UnityEngine;
using UnityEngine.UI;

public class OpenWebApplication : MonoBehaviour
{
    private void Start()
    {
        GameObject.Find("Quiz").GetComponent<Button>().onClick.AddListener(LaunchQuiz);
    }

    public void LaunchQuiz()
    {
        string url = "http://localhost:3000/" + GetMethod.userID; // URL of your web application
        Application.OpenURL(url);
    }
}
