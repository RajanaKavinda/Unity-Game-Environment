using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private GameObject leaderboardEntryPrefab;
    [SerializeField] private Transform leaderboardContent;

    private string userListUrl = "http://20.15.114.131:8080/api/user/profile/list";
    private List<PlayerData> players = new List<PlayerData>();

    private void Start()
    {
        // Fetch user list and then add the current player's data
        StartCoroutine(FetchUserList());
    }

    private IEnumerator FetchUserList()
    {
        UnityWebRequest request = UnityWebRequest.Get(userListUrl);
        request.SetRequestHeader("Authorization", "Bearer " + GetMethod.jwtToken);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            UserListResponse userList = JsonUtility.FromJson<UserListResponse>(request.downloadHandler.text);
  
            int userNo = 1;
            foreach (var user in userList.userViews)
            {
                if (userNo == 13) // Ensure this matches your player's username
                {
                    AddCurrentPlayerData(user.username);
                }
                else
                {
                    int gemCount = Random.Range(-5, 5); // Random change once a day
                    int landCount = Random.Range(0, 2); // Rare increase
                    int coinCount = Random.Range(0, 50); // Frequent change

                    int score = ScoringSystem.CalculateScore(gemCount, landCount, coinCount);

                    players.Add(new PlayerData
                    {
                        Username = user.username,
                        Score = score,
                        GemCount = gemCount,
                        LandCount = landCount,
                        CoinCount = coinCount
                    });
                }
                userNo++;
            }

            UpdateLeaderboard();
        }
        else
        {
            Debug.LogError("Failed to fetch user list: " + request.error);
        }
    }

    private void AddCurrentPlayerData(string username)
    {
        int gemCount = PlayerPrefs.GetInt("TotalGems",0);
        int landCount = (PlayerPrefs.GetInt("QuizMarks",0)/10 + 1) + PlayerPrefs.GetInt("BoughtLands",0);
        int coinCount = PlayerPrefs.GetInt("Score", 0);

        int score = ScoringSystem.CalculateScore(gemCount, landCount, coinCount);

        players.Add(new PlayerData
        {
            Username = username,
            Score = score,
            GemCount = gemCount,
            LandCount = landCount,
            CoinCount = coinCount,
        });
    }


    private void UpdateLeaderboard()
    {
        players.Sort((a, b) => b.Score.CompareTo(a.Score));

        foreach (Transform child in leaderboardContent)
        {
            Destroy(child.gameObject);
        }

        // Set a variable to store the rank of the current player
        int rank = 0;
        foreach (PlayerData player in players)
        {
            rank++;
            GameObject entry = Instantiate(leaderboardEntryPrefab, leaderboardContent);
            LeaderboardEntry leaderboardEntry = entry.GetComponent<LeaderboardEntry>();

            leaderboardEntry.SetEntryText($"{rank} {player.Username}: {player.Score} ({player.LandCount}, {player.GemCount},  {player.CoinCount})");

        }
    }

    [System.Serializable]
    private class UserListResponse
    {
        public List<UserView> userViews;
    }

    [System.Serializable]
    private class UserView
    {
        public string firstname;
        public string lastname;
        public string username;
    }

    private class PlayerData
    {
        public string Username;
        public int Score;
        public int GemCount;
        public int LandCount;
        public int CoinCount;
    }
}
