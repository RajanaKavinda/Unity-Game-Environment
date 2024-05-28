using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using static GetMethod;

public class ShowQuizMarks : MonoBehaviour
{
    public int Land = 0;
    public int GameCoins = 0;
    public int EnergyCoins = 0;
    

    // Reference to the Text component where you want to display the marks
    public Text LandText;
    public Text GameCoinText;
    public Text EnergyCoinText;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckStatus());
        
    }

    IEnumerator CheckStatus()
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
                Land = (profileResponse.questionnaireScore)/10 + 1;
                if (PlayerPrefs.HasKey("BoughtLands"))
                {
                    Land += PlayerPrefs.GetInt("BoughtLands");
                }

                GameCoins = PlayerPrefs.GetInt("Score");
                EnergyCoins = PlayerPrefs.GetInt("TotalGems");
                


                // Update the UI text with marks
                LandText.text = Land.ToString();
                GameCoinText.text = GameCoins.ToString();
                EnergyCoinText.text = EnergyCoins.ToString();
                
            }
        }
    }

    public class PlayerProfileResponse
    {
        public int userId;
        public string userName;
        public bool profileEdited;
        public bool questionnaireTaken;
        public int questionnaireScore;

    }
}
