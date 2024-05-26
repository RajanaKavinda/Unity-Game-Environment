using UnityEngine;
using UnityEngine.UI;

public class GemsDisplay : MonoBehaviour
{
    public static GemsDisplay Instance { get; private set; }

    private int gems;
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

    private void Start()
    {
        // Ensure gemsText is assigned
        AssignGemsText();

        UpdateGemsDisplay();
    }

    private void AssignGemsText()
    {
        if (gemsText == null)
        {
            GameObject textObject = GameObject.Find("Gems"); // Replace "Gems" with the actual name or tag
            if (textObject != null)
            {
                gemsText = textObject.GetComponent<Text>();
            }

            if (gemsText == null)
            {
                Debug.LogError("GemsText UI component not found. Please ensure it is correctly named or tagged in the scene.");
            }
            else
            {
                Debug.Log("GemsText UI component successfully assigned.");
            }
        }
    }

    public bool UseGems(int amount)
    {   
        gems = PlayerPrefs.GetInt("TotalGems", 0);
        if (gems >= amount)
        {
            ChangeSavedGems(-amount);
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
        gems = PlayerPrefs.GetInt("TotalGems", 0);
        if (gemsText == null)
        {
            gemsText = GameObject.FindWithTag("Gems").GetComponent<Text>();          
        }
        gemsText.text = gems.ToString();
        Debug.Log("Total Gems From PlayerPref: " + gemsText.text);
    }

    public void ChangeSavedGems(int amount)
    {
        gems = PlayerPrefs.GetInt("TotalGems", 0);
        gems += amount;
        PlayerPrefs.SetInt("TotalGems", gems);
        UpdateGemsDisplay();
    }
}
