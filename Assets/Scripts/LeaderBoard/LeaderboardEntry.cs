using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntry : MonoBehaviour
{
    public Text entryRank;

    public void SetEntryRank(string text)
    {
        entryRank.text = text;
    }
}
