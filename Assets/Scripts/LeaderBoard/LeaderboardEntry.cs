using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntry : MonoBehaviour
{
    // UI elements for the leaderboard entry
    public Text entryRank;
    public Text entryPlayer;
    public Text entryScore;
    public Text entryLandCount;
    public Text entryGemCount;
    public Text entryCoinCount;

    // Set the rank text of the entry
    public void SetEntryRank(string rank)
    {
        entryRank.text = rank;
    }

    // Set the player name text of the entry
    public void SetEntryPlayer(string player)
    {
        entryPlayer.text = player;
    }

    // Set the score text of the entry
    public void SetEntryScore(string score)
    {
        entryScore.text = score;
    }

    // Set the land count text of the entry
    public void SetEntryLandCount(string landCount)
    {
        entryLandCount.text = landCount;
    }

    // Set the gem count text of the entry
    public void SetEntryGemCount(string gemCount)
    {
        entryGemCount.text = gemCount;
    }

    // Set the coin count text of the entry
    public void SetEntryCoinCount(string coinCount)
    {
        entryCoinCount.text = coinCount;
    }

    // Set the color of the entry text elements
    public void SetEntryColor(Color color)
    {
        entryRank.color = color;
        entryPlayer.color = color;
        entryScore.color = color;
        entryLandCount.color = color;
        entryGemCount.color = color;
        entryCoinCount.color = color;
    }
}
