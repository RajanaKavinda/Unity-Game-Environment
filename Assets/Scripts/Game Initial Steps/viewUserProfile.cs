using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ViewUserProfile : MonoBehaviour
{
    // URL for fetching user profile data
    private const string apiUrl = "http://20.15.114.131:8080/api/user/profile/view";

    // Text fields to display user profile data
    public Text firstName;
    public Text lastName;
    public Text username;
    public Text NIC;
    public Text phoneNumber;
    public Text email;

    // Holder for current user data
    private UserData currentUserData;

    void Start()
    {
        // Fetch user profile data when the script starts
        StartCoroutine(FetchUserData());
    }

    // Coroutine to fetch user profile data from the server
    IEnumerator FetchUserData()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        request.SetRequestHeader("Authorization", "Bearer " + GetMethod.jwtToken);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            UserDataResponse userDataResponse = JsonUtility.FromJson<UserDataResponse>(jsonResponse);

            if (userDataResponse != null && userDataResponse.user != null)
            {
                // Store received user data and update profile fields
                currentUserData = userDataResponse.user;
                UpdateProfileFields();
                Debug.Log("Player data received");
                Debug.Log(currentUserData.lastname);
            }
            else
            {
                Debug.LogError("Error: User data is null or invalid JSON response structure.");
            }
        }
        else
        {
            Debug.LogError("Error fetching user data: " + request.error);
        }
    }

    // Method to update profile fields with fetched user data
    void UpdateProfileFields()
    {
        firstName.text = currentUserData.firstname;
        lastName.text = currentUserData.lastname;
        username.text = currentUserData.username;
        NIC.text = currentUserData.nic;
        phoneNumber.text = currentUserData.phoneNumber;
        email.text = currentUserData.email;
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
}
