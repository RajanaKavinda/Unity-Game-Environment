using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GetMethod : MonoBehaviour
{
    InputField outputArea;
    [SerializeField] string apiKey = "NjVkNDIyMjNmMjc3NmU3OTI5MWJmZGIyOjY1ZDQyMjIzZjI3NzZlNzkyOTFiZmRhOA"; // Replace with your actual API key

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

        // Create JSON object for the request body
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
                Invoke("NextLevel", 4f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                // Parse the response to get the JWT token
                string jsonResponse = request.downloadHandler.text;
                LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(jsonResponse);
                outputArea.text = loginResponse.token;
                Invoke("NextLevel", 4f);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

            }
        }
    }

    // Class to represent the response body from the login endpoint
    [System.Serializable]
    public class LoginResponse
    {
        public string token;
    }
}
