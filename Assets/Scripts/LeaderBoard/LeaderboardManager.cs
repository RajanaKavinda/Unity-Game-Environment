using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    [SerializeField] private GameObject leaderboardEntryPrefab;
    [SerializeField] private Transform leaderboardRank;
    [SerializeField] private Transform leaderboardPlayer;
    [SerializeField] private Transform leaderboardScore;
    [SerializeField] private Transform leaderboardLandCount;
    [SerializeField] private Transform leaderboardGemCount;
    [SerializeField] private Transform leaderboardCoinCount;

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
                    int gemCount = Random.Range(0, 200); 
                    int landCount = Random.Range(1, 14); 
                    int coinCount = Random.Range(0, 1000); 

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

        foreach (Transform child in leaderboardRank)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in leaderboardPlayer)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in leaderboardScore)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in leaderboardLandCount)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in leaderboardGemCount)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in leaderboardCoinCount)
        {
            Destroy(child.gameObject);
        }

        // Set a variable to store the rank of the current player
        int rank = 0;
        foreach (PlayerData player in players)
        {
            rank++;
            GameObject entryRank = Instantiate(leaderboardEntryPrefab, leaderboardRank);
            GameObject entryPlayer = Instantiate(leaderboardEntryPrefab, leaderboardPlayer);
            GameObject entryScore = Instantiate(leaderboardEntryPrefab, leaderboardScore);
            GameObject entryLandCount = Instantiate(leaderboardEntryPrefab, leaderboardLandCount);
            GameObject entryGemCount = Instantiate(leaderboardEntryPrefab, leaderboardGemCount);
            GameObject entryCoinCount = Instantiate(leaderboardEntryPrefab, leaderboardCoinCount);
            LeaderboardEntry leaderboardEntryRank = entryRank.GetComponent<LeaderboardEntry>();
            LeaderboardEntry leaderboardEntryPlayer = entryPlayer.GetComponent<LeaderboardEntry>();
            LeaderboardEntry leaderboardEntryScore = entryScore.GetComponent<LeaderboardEntry>();
            LeaderboardEntry leaderboardEntryLandCount = entryLandCount.GetComponent<LeaderboardEntry>();
            LeaderboardEntry leaderboardEntryGemCount = entryGemCount.GetComponent<LeaderboardEntry>();
            LeaderboardEntry leaderboardEntryCoinCount = entryCoinCount.GetComponent<LeaderboardEntry>();
            //leaderboardEntry.SetEntryRank($"{rank} {player.Username}: {player.Score} ({player.LandCount}, {player.GemCount},  {player.CoinCount})");
            leaderboardEntryRank.SetEntryRank($"{rank}");
            leaderboardEntryPlayer.SetEntryPlayer($"{player.Username}");
            leaderboardEntryScore.SetEntryScore($"{player.Score}");
            leaderboardEntryLandCount.SetEntryLandCount($"{player.LandCount}");
            leaderboardEntryGemCount.SetEntryGemCount($"{player.GemCount}");
            leaderboardEntryCoinCount.SetEntryCoinCount($"{player.CoinCount}");
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
