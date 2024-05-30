
using UnityEngine;
using UnityEngine.UI;

public class GemsDisplay : MonoBehaviour
{
    public static GemsDisplay Instance { get; private set; }
    public Text gemsText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateGemsDisplay();
    }

    public void UpdateGemsDisplay()
    {
        int totalGems = PlayerPrefs.GetInt("TotalGems", 0);
        gemsText.text = totalGems.ToString();
    }
}
