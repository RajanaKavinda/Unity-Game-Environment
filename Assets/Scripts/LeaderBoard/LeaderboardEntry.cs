using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntry : MonoBehaviour
{
    public Text entryRank;
    public Text entryPlayer;
    public Text entryScore;
    public Text entryLandCount;
    public Text entryGemCount;
    public Text entryCoinCount;

    public void SetEntryRank(string rank)
    {
        entryRank.text = rank;
    }

    public void SetEntryPlayer(string player)
    {
        entryPlayer.text = player;
    }

    public void SetEntryScore(string score)
    {
        entryScore.text = score;
    }

    public void SetEntryLandCount(string landCount)
    {
        entryLandCount.text = landCount;
    }

    public void SetEntryGemCount(string gemCount)
    {
        entryGemCount.text = gemCount;
    }

    public void SetEntryCoinCount(string coinCount)
    {
        entryCoinCount.text = coinCount;
    }
}
