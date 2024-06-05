using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    private readonly string[] apiKeys = {
        "NjVkNDIyMjNmMjc3NmU3OTI5MWJmZGIyOjY1ZDQyMjIzZjI3NzZlNzkyOTFiZmRhOA",
        "NjVkNDIyMjNmMjc3NmU3OTI5MWJmZGIzOjY1ZDQyMjIzZjI3NzZlNzkyOTFiZmRhOQ",
        "NjVkNDIyMjNmMjc3NmU3OTI5MWJmZGI0OjY1ZDQyMjIzZjI3NzZlNzkyOTFiZmRhYQ"
    };
    public Text user1;
    public Text user2;
    public Text user3;
    public Text energy1;
    public Text energy2;
    public Text energy3;
    private string urlExtension1 = "/login";
    private string urlExtension2 = "/user/profile/view";
    private string urlExtension3 = "/power-consumption/current/view";
    public InputField outputArea;

    // Dictionary to store user names, jwtToken and their corresponding energy consumption values 
    private Dictionary<string, (string, float)> userEnergyConsumption = new Dictionary<string, (string, float)>();

    // List to store user names
    private List<string> userNames = new List<string>();

    private void Start()
    {
        StartCoroutine(InitializeUserEnergyConsumption());
    }

    private IEnumerator InitializeUserEnergyConsumption()
    {
        HttpRequest httpRequest = new HttpRequest();

        for (int i = 0; i < apiKeys.Length; i++)
        {
            // Obtain the JWT token
            yield return StartCoroutine(GetJwtToken(httpRequest, apiKeys[i], i));
        }

        // Start updating energy consumption
        StartCoroutine(UpdateEnergyConsumption());
    }

    private IEnumerator GetJwtToken(HttpRequest httpRequest, string apiKey, int index)
    {
        string jsonRequestBody = "{\"apiKey\":\"" + apiKey + "\"}";
        yield return StartCoroutine(httpRequest.SendHttpRequest("post", "givenBackend", urlExtension1, "", jsonRequestBody));
        string result = (string)httpRequest.result;
        if (result == "Error")
        {
            Debug.LogError("Error obtaining JWT token for API key index " + index);
            yield break;
        }

        LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(result);
        string jwtToken = loginResponse.token;
        Debug.Log("JWT token for API key index " + index + ": " + jwtToken);

        // Obtain the user name
        yield return StartCoroutine(GetUserName(httpRequest, jwtToken));
    }

    private IEnumerator GetUserName(HttpRequest httpRequest, string jwtToken)
    {
        yield return StartCoroutine(httpRequest.SendHttpRequest("get", "givenBackend", urlExtension2, jwtToken, ""));
        string result = (string)httpRequest.result;
        if (result == "Error")
        {
            Debug.LogError("Error obtaining user name with JWT token");
            yield break;
        }

        UserDataResponse userDataResponse = JsonUtility.FromJson<UserDataResponse>(result);
        string userName = userDataResponse.user.username;
        Debug.Log("User name: " + userName);
        userNames.Add(userName);

        // Add user name and JWT token to the dictionary
        userEnergyConsumption.Add(userName, (jwtToken, 0));
    }

    private IEnumerator UpdateEnergyConsumption()
    {
        HttpRequest httpRequest = new HttpRequest();

        while (true)
        {
            foreach (var user in userNames)
            {
                string jwtToken = userEnergyConsumption[user].Item1;
                yield return StartCoroutine(GetCurrentConsumption(httpRequest, user, jwtToken));
            }

            // Print the user names and their corresponding energy consumption values
            foreach (var user in userEnergyConsumption)
            {
                Debug.Log("User: " + user.Key + ", Energy consumption: " + user.Value.Item2);
            }

            // Update the Leaderboard UI
            UpdateLeaderboardUI();

            yield return new WaitForSeconds(10);
        }
    }

    private IEnumerator GetCurrentConsumption(HttpRequest httpRequest, string userName, string jwtToken)
    {
        yield return StartCoroutine(httpRequest.SendHttpRequest("get", "givenBackend", urlExtension3, jwtToken, ""));

        string result = (string)httpRequest.result;
        if (result == "Error")
        {
            Debug.LogError("Error obtaining current consumption for user: " + userName);
            yield break;
        }

        CurrentConsumptionResponse jsonData = JsonUtility.FromJson<CurrentConsumptionResponse>(result);
        float currentConsumption = jsonData.currentConsumption;

        // Update the dictionary with the new consumption value
        userEnergyConsumption[userName] = (jwtToken, currentConsumption);
    }

    private void UpdateLeaderboardUI()
    {
        // Sort the dictionary by energy consumption values in ascending order
        var sortedList = userEnergyConsumption.OrderBy(x => x.Value.Item2).ToList();

        // Ensure there are at least three users before updating the UI
        if (sortedList.Count >= 3)
        {
            user1.text = sortedList[0].Key;
            user2.text = sortedList[1].Key;
            user3.text = sortedList[2].Key;
            energy1.text = sortedList[0].Value.Item2.ToString();
            energy2.text = sortedList[1].Value.Item2.ToString();
            energy3.text = sortedList[2].Value.Item2.ToString();
        }
    }

    public class LoginResponse
    {
        public string token;
    }

    // Serializable class to parse user data JSON response
    [System.Serializable]
    private class UserDataResponse
    {
        public UserData user;
    }

    // Serializable class to hold user data fields
    [System.Serializable]
    private class UserData
    {
        public string firstname;
        public string lastname;
        public string username;
        public string nic;
        public string phoneNumber;
        public string email;
    }

    [System.Serializable]
    public class CurrentConsumptionResponse
    {
        public float currentConsumption;
    }
}
