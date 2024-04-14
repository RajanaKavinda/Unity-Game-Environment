using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;



public class GetMethod : MonoBehaviour
{
    InputField outputArea;
    string apiKey = "NjVkNDIyMjNmMjc3NmU3OTI5MWJmZGIyOjY1ZDQyMjIzZjI3NzZlNzkyOTFiZmRhOA"; // Replace with your actual API key
    public static string jwtToken; // Static variable to store the JWT token
    public static string jwtToken2;
    public static string userID;
    public bool profileCompleted;
    public bool questionereCompleted;
    public int questionereScore;



    void Start()
    {
        outputArea = GameObject.Find("OutputArea").GetComponent<InputField>();
        GameObject.Find("GetButton").GetComponent<Button>().onClick.AddListener(GetData);
    }

    void GetData() => StartCoroutine(GetData_Coroutine());

    IEnumerator GetData_Coroutine()
    {
        outputArea.text = "Loading...";
        string uri = "http://20.15.114.131:8080/api/login";
        string jsonRequestBody = "{\"apiKey\":\"" + apiKey + "\"}";

        using (UnityWebRequest request = new UnityWebRequest(uri, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                outputArea.text = request.error;
                yield return new WaitForSeconds(1f);
                SceneManager.LoadScene("Error");
            }
            else
            {
                string jsonResponse = request.downloadHandler.text;
                LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(jsonResponse);
                jwtToken = loginResponse.token;

                yield return new WaitForSeconds(1f);
                yield return StartCoroutine(GetUserTokenCoroutine(jwtToken));

                // SceneManager.LoadScene("Player Profile");
            }
        }
    }

    

    IEnumerator GetUserTokenCoroutine(string jwtToken)
    {
        string baseURL = "http://localhost:8080/energy-quest/user";
        string jsonRequestBody = "\"" + jwtToken + "\"";
        using (UnityWebRequest request = new UnityWebRequest(baseURL, "POST"))
        {
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("accept", "*/*");

            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonRequestBody);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error getting user token: " + request.error);
            }
            else
            {
                string responseBody = request.downloadHandler.text;
                UserResponse response = JsonUtility.FromJson<UserResponse>(responseBody);

                userID = response.userID;
                jwtToken2 = response.token;

                Debug.Log("User ID: " + userID);
                Debug.Log("JWT Token: " + jwtToken2);

                yield return StartCoroutine(CheckProfileAndQuestionnaire());


            }
        }
    }

    IEnumerator CheckProfileAndQuestionnaire()
    {
        // URL of the endpoint with the user ID
        string url = "http://localhost:8080/energy-quest/user/id/" + userID;

        // Create a GET request
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            // Set the request headers
            request.SetRequestHeader("Authorization", "Bearer " + jwtToken2);

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

                // Set profileCompleted and questionnaireCompleted properties
                profileCompleted = profileResponse.profileEdited;
                questionereCompleted = profileResponse.questionnaireTaken;
                questionereScore = profileResponse.questionnaireScore;

                // Log the values for verification
                Debug.Log("Profile Completed: " + profileCompleted);
                Debug.Log("Questionnaire Completed: " + questionereCompleted);

                if (!profileCompleted)
                {
                    SceneManager.LoadScene("Player Profile");
                }
                else if (!questionereCompleted)
                {
                    SceneManager.LoadScene("Questionere");
                }
                else
                {
                    SceneManager.LoadScene("Main Menu");
                }
            }
        }
    }









    [System.Serializable]
    public class LoginResponse
    {
        public string token;
    }

 

    [System.Serializable]
    public class UserResponse
    {
        public string userID;
        public string token;
    }

    

    // Class to represent the response body from the user questionnaire endpoint
    [System.Serializable]
    public class PlayerProfileResponse
    {
        public int userId;
        public string userName;
        public bool profileEdited;
        public bool questionnaireTaken;
        public int questionnaireScore;
    }
}
