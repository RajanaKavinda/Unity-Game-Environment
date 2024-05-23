using UnityEngine;
using UnityEngine.UI;

public class GemsDisplay : MonoBehaviour
{
    public static GemsDisplay Instance { get; private set; }

    public int gems;
    public Text gemsText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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

    public int GetGems()
    {
        return gems;
    }

    public void AddGems(int amount)
    {
        gems += amount;
        UpdateGemsDisplay();
    }

    public bool UseGems(int amount)
    {
        if (gems >= amount)
        {
            gems -= amount;
            UpdateGemsDisplay();
            return true;
        }
        else
        {
            Debug.LogError("Not enough gems!");
            return false;
        }
    }

    public void UpdateGemsDisplay()
    {

        int totalGems = PlayerPrefs.GetInt("TotalGems", 0);
        gemsText.text = gems.ToString();
        
    }
}
