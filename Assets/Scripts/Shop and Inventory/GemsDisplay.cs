using UnityEngine;
using UnityEngine.UI;

public class GemsDisplay : MonoBehaviour
{
    // Static instance for singleton pattern
    public static GemsDisplay Instance { get; private set; }

    // Current gems count
    private int gems;

    // Reference to the UI Text component for displaying gems
    public Text gemsText;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // If another instance exists, destroy this one
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Ensure gemsText is assigned
        AssignGemsText();

        // Update the gems display
        UpdateGemsDisplay();
    }

    // Assigns the gemsText reference if not assigned
    private void AssignGemsText()
    {
        if (gemsText == null)
        {
            GameObject textObject = GameObject.Find("Gems"); // Find the GameObject with the gems display text
            if (textObject != null)
            {
                gemsText = textObject.GetComponent<Text>(); // Get the Text component
            }

            if (gemsText == null)
            {
                Debug.LogError("GemsText UI component not found. Please ensure it is correctly named in the scene.");
            }
            else
            {
                Debug.Log("GemsText UI component successfully assigned.");
            }
        }
    }

    // Use gems for a purchase
    public bool UseGems(int amount)
    {
        gems = PlayerPrefs.GetInt("TotalGems", 0); // Get the total gems count from player preferences
        if (gems >= amount) // If enough gems are available
        {
            ChangeSavedGems(-amount); // Decrease gems count
            return true; // Purchase successful
        }
        else
        {
            Debug.LogError("Not enough gems!"); // Log error if not enough gems
            return false; // Purchase unsuccessful
        }
    }

    // Update the gems display text
    public void UpdateGemsDisplay()
    {
        gems = PlayerPrefs.GetInt("TotalGems", 0); // Get the total gems count from player preferences
        if (gemsText == null)
        {
            gemsText = GameObject.FindWithTag("Gems").GetComponent<Text>(); // Find the gemsText component by tag if not assigned
        }
        gemsText.text = gems.ToString(); // Update gems display text
        Debug.Log("Total Gems From PlayerPref: " + gemsText.text); // Log the total gems count
    }

    // Change the saved gems count by a specified amount
    public void ChangeSavedGems(int amount)
    {
        gems = PlayerPrefs.GetInt("TotalGems", 0); // Get the total gems count from player preferences
        gems += amount; // Change gems count
        PlayerPrefs.SetInt("TotalGems", gems); // Save the updated gems count to player preferences
        UpdateGemsDisplay(); // Update the gems display
    }
}
