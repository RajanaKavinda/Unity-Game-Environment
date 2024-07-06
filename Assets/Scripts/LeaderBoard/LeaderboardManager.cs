using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LeaderboardManager : MonoBehaviour
{
    // Prefab for the leaderboard entry
    [SerializeField] private GameObject leaderboardEntryPrefab;

    // Transform references for the leaderboard columns
    [SerializeField] private Transform leaderboardRank;
    [SerializeField] private Transform leaderboardPlayer;
    [SerializeField] private Transform leaderboardScore;
    [SerializeField] private Transform leaderboardLandCount;
    [SerializeField] private Transform leaderboardGemCount;
    [SerializeField] private Transform leaderboardCoinCount;

    // URL to fetch the user list
    private string userListUrl = "http://20.15.114.131:8080/api/user/profile/list";

    // List to store player data
    private List<PlayerData> players = new List<PlayerData>();

    // Username of the current player
    private string currentPlayerUsername;

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
                    currentPlayerUsername = user.username;
                    AddCurrentPlayerData(user.username);
                }
                else
                {
                    // Generate random data for other players
                    int gemCount = Random.Range(0, 200);
                    int landCount = Random.Range(1, 14);
                    int coinCount = Random.Range(0, 1000);

                    int score = ScoringSystem.CalculateScore(gemCount, landCount, coinCount);

                    // Add the player data to the list
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
        // Fetch current player's data from PlayerPrefs
        int gemCount = PlayerPrefs.GetInt("TotalGems", 0);
        int landCount = (PlayerPrefs.GetInt("QuizMarks", 0) / 10 + 1) + PlayerPrefs.GetInt("BoughtLands", 0);
        int coinCount = PlayerPrefs.GetInt("Score", 0);

        int score = ScoringSystem.CalculateScore(gemCount, landCount, coinCount);

        // Add current player's data to the list
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
        // Sort players by score in descending order
        players.Sort((a, b) => b.Score.CompareTo(a.Score));

        // Clear existing leaderboard entries
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

        // Add new entries to the leaderboard
        int rank = 0;
        foreach (PlayerData player in players)
        {
            rank++;
            // Instantiate leaderboard entry prefabs
            GameObject entryRank = Instantiate(leaderboardEntryPrefab, leaderboardRank);
            GameObject entryPlayer = Instantiate(leaderboardEntryPrefab, leaderboardPlayer);
            GameObject entryScore = Instantiate(leaderboardEntryPrefab, leaderboardScore);
            GameObject entryLandCount = Instantiate(leaderboardEntryPrefab, leaderboardLandCount);
            GameObject entryGemCount = Instantiate(leaderboardEntryPrefab, leaderboardGemCount);
            GameObject entryCoinCount = Instantiate(leaderboardEntryPrefab, leaderboardCoinCount);

            // Get the LeaderboardEntry component from the instantiated objects
            LeaderboardEntry leaderboardEntryRank = entryRank.GetComponent<LeaderboardEntry>();
            LeaderboardEntry leaderboardEntryPlayer = entryPlayer.GetComponent<LeaderboardEntry>();
            LeaderboardEntry leaderboardEntryScore = entryScore.GetComponent<LeaderboardEntry>();
            LeaderboardEntry leaderboardEntryLandCount = entryLandCount.GetComponent<LeaderboardEntry>();
            LeaderboardEntry leaderboardEntryGemCount = entryGemCount.GetComponent<LeaderboardEntry>();
            LeaderboardEntry leaderboardEntryCoinCount = entryCoinCount.GetComponent<LeaderboardEntry>();

            // Set the entry text values
            leaderboardEntryRank.SetEntryRank($"{rank}");
            leaderboardEntryPlayer.SetEntryPlayer($"{player.Username}");
            leaderboardEntryScore.SetEntryScore($"{player.Score}");
            leaderboardEntryLandCount.SetEntryLandCount($"{player.LandCount}");
            leaderboardEntryGemCount.SetEntryGemCount($"{player.GemCount}");
            leaderboardEntryCoinCount.SetEntryCoinCount($"{player.CoinCount}");

            // Highlight the current player with red color
            if (player.Username == currentPlayerUsername)
            {
                Color highlightColor = Color.red;
                leaderboardEntryRank.SetEntryColor(highlightColor);
                leaderboardEntryPlayer.SetEntryColor(highlightColor);
                leaderboardEntryScore.SetEntryColor(highlightColor);
                leaderboardEntryLandCount.SetEntryColor(highlightColor);
                leaderboardEntryGemCount.SetEntryColor(highlightColor);
                leaderboardEntryCoinCount.SetEntryColor(highlightColor);

                Debug.Log($"Highlighting player {player.Username} with red color.");
            }
            else
            {
                // Highlight other players with blue color
                Color highlightColor = Color.blue;
                leaderboardEntryRank.SetEntryColor(highlightColor);
                leaderboardEntryPlayer.SetEntryColor(highlightColor);
                leaderboardEntryScore.SetEntryColor(highlightColor);
                leaderboardEntryLandCount.SetEntryColor(highlightColor);
                leaderboardEntryGemCount.SetEntryColor(highlightColor);
                leaderboardEntryCoinCount.SetEntryColor(highlightColor);

                Debug.Log($"Highlighting player {player.Username} with blue color.");
            }
        }
    }

    // Response class for user list
    [System.Serializable]
    private class UserListResponse
    {
        public List<UserView> userViews;
    }

    // Class representing a user
    [System.Serializable]
    private class UserView
    {
        public string firstname;
        public string lastname;
        public string username;
    }

    // Class representing player data
    private class PlayerData
    {
        public string Username;
        public int Score;
        public int GemCount;
        public int LandCount;
        public int CoinCount;
    }
}
