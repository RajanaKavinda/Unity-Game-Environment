using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class getPlayerData : MonoBehaviour
{
    private const string apiUrl = "http://20.15.114.131:8080/api/user/profile/view";
    InputField profileArea;

    void Start()
    {
        profileArea = GameObject.Find("PlayerInformation").GetComponent<InputField>();
        // Make sure jwtToken is not null before fetching player information
        if (GetMethod.jwtToken != null)
        {
            StartCoroutine(FetchPlayerInformation());
            Debug.Log("Fetching player information...");
        }
        else
        {
            Debug.LogError("JWT token is null. Make sure it's properly set.");
        }
    }

    IEnumerator FetchPlayerInformation()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        request.SetRequestHeader("Authorization", "Bearer " + GetMethod.jwtToken); // Use the JWT token from GetMethod script
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching player information: " + request.error);
            profileArea.text = "Error fetching player information";
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;
            PlayerProfileData profileData = JsonUtility.FromJson<PlayerProfileData>(jsonResponse);
            UpdatePlayerInformation(profileData);
        }
    }

    void UpdatePlayerInformation(PlayerProfileData profileData)
    {
        profileArea.text = $"Name: {profileData.user.firstname} {profileData.user.lastname}\n" +
                   $"Username: {profileData.user.username}\n" +
                   $"NIC: {profileData.user.nic}\n" +
                   $"Phone Number: {profileData.user.phoneNumber}\n" +
                   $"Email: {profileData.user.email}\n";

    }

    [System.Serializable]
    private class PlayerProfileData
    {
        public UserData user;
    }

    [System.Serializable]
    private class UserData
    {
        public string firstname;
        public string lastname;
        public string username;
        public string nic;
        public string phoneNumber;
        public string email;
        public string profilePictureUrl;
    }
}
