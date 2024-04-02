using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UserProfilePage : MonoBehaviour
{
    private const string apiUrl = "http://20.15.114.131:8080/api/user/profile/view";
    private const string updateUrl = "http://20.15.114.131:8080/api/user/profile/update";

    public TMP_InputField firstName;
    public TMP_InputField lastName;
    public TMP_InputField username;
    public TMP_InputField NIC;
    public TMP_InputField phoneNumber;
    public TMP_InputField email;
    public TMP_InputField profilePicture;

    private UserData currentUserData;
    private NewUserData ChangedUserData;

    void Start()
    {
        StartCoroutine(FetchUserData());
        GameObject.Find("submit").GetComponent<Button>().onClick.AddListener(SaveChanges);
    }

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
                currentUserData = userDataResponse.user;
                UpdateProfileFields();
                Debug.Log("Player data received");
                Debug.Log(currentUserData.firstname);
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

    void UpdateProfileFields()
    {
        firstName.text = currentUserData.firstname;
        lastName.text = currentUserData.lastname;
        username.text = currentUserData.username;
        NIC.text = currentUserData.nic;
        phoneNumber.text = currentUserData.phoneNumber;
        email.text = currentUserData.email;
        profilePicture.text = currentUserData.profilePictureUrl;
    }

    public void SaveChanges()
    {
        ChangedUserData = new NewUserData();
        ChangedUserData.firstname = firstName.text;
        ChangedUserData.lastname = lastName.text;
        ChangedUserData.nic = NIC.text;
        ChangedUserData.phoneNumber = phoneNumber.text;
        ChangedUserData.email = email.text;
        ChangedUserData.profilePictureUrl = profilePicture.text;

        StartCoroutine(UpdateUserData());
    }

    IEnumerator UpdateUserData()
    {
        string jsonUserData = JsonUtility.ToJson(currentUserData);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonUserData);

        UnityWebRequest request1 = UnityWebRequest.Put(updateUrl, bodyRaw);
        request1.SetRequestHeader("Authorization", "Bearer " + GetMethod.jwtToken);
        request1.SetRequestHeader("Content-Type", "application/json");
        yield return request1.SendWebRequest();

        if (request1.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("User data updated successfully!");
            SceneManager.LoadScene("Questionere Not Completed");
        }
        else
        {
            Debug.LogError("Error updating user data: " + request1.error);
        }
    }

    [System.Serializable]
    private class UserDataResponse
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

    private class NewUserData
    {
        public string firstname;
        public string lastname;       
        public string nic;
        public string phoneNumber;
        public string email;
        public string profilePictureUrl;
    }
}
