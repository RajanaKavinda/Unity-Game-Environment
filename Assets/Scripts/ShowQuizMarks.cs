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
    public int gameLevel = 0;

    // Reference to the Text component where you want to display the marks
    public Text LandText;
    public Text GameCoinText;
    public Text EnergyCoinText;
    public Text GameLevelText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CheckStatus());
        
    }

    IEnumerator CheckStatus()
    {
        string urlExtension = "/user/id/" + GetMethod.userID;
        string jwtToken = GetMethod.jwtToken2;

        // Create an instance of the HttpRequest class
        HttpRequest httpRequest = new HttpRequest();

        // Send HTTP GET request
        yield return StartCoroutine(httpRequest.SendHttpRequest("get", "ourBackend", urlExtension, jwtToken, ""));

        // Check if the request was successful
        string result = (string)httpRequest.result;
        if (result == "Error")
        {
            Debug.LogError("Error checking profile and questionnaire");
            yield break;
        }
    
        // Parse the response
        PlayerProfileResponse profileResponse = JsonUtility.FromJson<PlayerProfileResponse>(result);

        // Set questionnaireCompleted property
        if (profileResponse.lands == 0){
             Land = (profileResponse.questionnaireScore)/20 + 1;
             // Create an instance of the HttpRequest class
             HttpRequest updateLands = new HttpRequest();
             string urlExtension2 = "/user/lands/" + GetMethod.userID + "/" + Land;
            // Send HTTP GET request
            yield return StartCoroutine(updateLands.SendHttpRequest("post", "ourBackend", urlExtension2, jwtToken, ""));
        }   
        else{
            Land = profileResponse.lands;
        }
        GameCoins = (profileResponse.gameCoin);
        EnergyCoins = (profileResponse.energyCoin);
        gameLevel = (profileResponse.gameLevel);

        // Update the UI text with marks
        LandText.text = Land.ToString();
        GameCoinText.text = GameCoins.ToString();
        EnergyCoinText.text = EnergyCoins.ToString();
        GameLevelText.text = "Level " + gameLevel.ToString();
    }


    public class PlayerProfileResponse
    {
        public int userId;
        public string userName;
        public bool profileEdited;
        public bool questionnaireTaken;
        public int questionnaireScore;
        public int gameCoin;
        public int energyCoin;
        public int gameLevel;
        public int lands;

    }
}
