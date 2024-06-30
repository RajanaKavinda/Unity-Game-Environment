using UnityEngine;
using UnityEngine.UI;

public class LeaderboardEntry : MonoBehaviour
{
    public Text entryText;

    public void SetEntryText(string text)
    {
        entryText.text = text;
    }
}
