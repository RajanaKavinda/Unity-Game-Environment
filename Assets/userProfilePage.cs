using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class UserProfilePage : MonoBehaviour
{
    private const string apiUrl = "http://20.15.114.131:8080/api/user/profile/view";

    public TMP_InputField firstName;
    public TMP_InputField lastName;
    public TMP_InputField username;
    public TMP_InputField NIC;
    public TMP_InputField phoneNumber;
    public TMP_InputField email;
    public TMP_InputField profilePicture;

    private UserData currentUserData;

    void Start()
    {
        StartCoroutine(FetchUserData());
    }

    IEnumerator FetchUserData()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        request.SetRequestHeader("Authorization", "Bearer " + GetMethod.jwtToken);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string jsonResponse = request.downloadHandler.text;
            currentUserData = JsonUtility.FromJson<UserData>(jsonResponse);
            Debug.Log("Player data received");
            UpdateProfileFields();
        }
        else
        {
            Debug.LogError("Error fetching user data: " + request.error);
        }
    }

    void UpdateProfileFields()
    {
        firstName.text = currentUserData.firstname;
        lastName.text = currentUserData.lastname;
        username.text = currentUserData.username;
        NIC.text = currentUserData.nic;
        phoneNumber.text = currentUserData.phoneNumber;
        email.text = currentUserData.email;
        profilePicture.text = currentUserData.profilePicture;
    }

    public void SaveChanges()
    {
        currentUserData.firstname = firstName.text;
        currentUserData.lastname = lastName.text;
        currentUserData.username = username.text;
        currentUserData.nic = NIC.text;
        currentUserData.phoneNumber = phoneNumber.text;
        currentUserData.email = email.text;
        currentUserData.profilePicture = profilePicture.text;

        StartCoroutine(UpdateUserData());
    }

    IEnumerator UpdateUserData()
    {
        string jsonUserData = JsonUtility.ToJson(currentUserData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonUserData);

        UnityWebRequest request = UnityWebRequest.Put(apiUrl, bodyRaw);
        request.SetRequestHeader("Authorization", "Bearer " + GetMethod.jwtToken);
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("User data updated successfully!");
        }
        else
        {
            Debug.LogError("Error updating user data: " + request.error);
        }
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
        public string profilePicture;
    }
}
