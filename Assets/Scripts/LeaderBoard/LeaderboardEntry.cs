using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntry : MonoBehaviour
{
    public Text entryRank;
    public Text entryPlayer;

    public void SetEntryRank(string rank)
    {
        entryRank.text = rank;
    }

    public void SetEntryPlayer(string player)
    {
        entryPlayer.text = player;
    }
}
