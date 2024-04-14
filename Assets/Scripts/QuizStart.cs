using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using static GetMethod;

public class OpenWebApplication : MonoBehaviour
{
    public bool questionnaireCompleted;
    public PromptMsg Prompt;

    private void Start()
    {
        GameObject.Find("Quiz").GetComponent<Button>().onClick.AddListener(LaunchQuiz);
        GameObject.Find("Next").GetComponent<Button>().onClick.AddListener(Next);
    }

    public void LaunchQuiz()
    {
        string url = "http://localhost:3000/" + GetMethod.jwtToken2 + "/" + GetMethod.userID; // URL of your web application
        Application.OpenURL(url);
    }

    public void Next()
    {
        StartCoroutine(CheckQuestionnaire());
    }

    IEnumerator CheckQuestionnaire()
    {
        // URL of the endpoint with the user ID
        string url = "http://localhost:8080/energy-quest/user/id/" + GetMethod.userID;

        // Create a GET request
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            // Set the request headers
            request.SetRequestHeader("Authorization", "Bearer " + GetMethod.jwtToken2);

            // Send the request
            yield return request.SendWebRequest();

            // Check for errors
            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error checking profile and questionnaire: " + request.error);
            }
            else
            {
                // Parse the response
                PlayerProfileResponse profileResponse = JsonUtility.FromJson<PlayerProfileResponse>(request.downloadHandler.text);

                // Set questionnaireCompleted property
                questionnaireCompleted = profileResponse.questionnaireTaken;

                Debug.Log("Questionnaire completed: " + questionnaireCompleted);

                // Check if questionnaire is completed
                if (questionnaireCompleted)
                {
                    SceneManager.LoadScene("Main Menu");
                }

                else
                {
                    Prompt.ShowPrompt("Please complete the quiz");
                }
            }
        }
    }
}
