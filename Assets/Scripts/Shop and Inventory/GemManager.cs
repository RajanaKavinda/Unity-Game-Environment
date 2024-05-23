using UnityEngine;
using UnityEngine.UI;

public class GemManager : MonoBehaviour
{
    public static GemManager Instance;

    public int gems;
    public Text gemCountText;

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

    private void Start()
    {
        UpdateGemUI();
    }

    public int GetGems()
    {
        return gems;
    }

    public void AddGems(int amount)
    {
        gems += amount;
        UpdateGemUI();
    }

    public bool UseGems(int amount)
    {
        if (gems >= amount)
        {
            gems -= amount;
            UpdateGemUI();
            return true;
        }
        else
        {
            Debug.LogError("Not enough gems!");
            return false;
        }
    }

    private void UpdateGemUI()
    {
        if (gemCountText != null)
        {
            gemCountText.text = gems.ToString();
        }
    }
}
