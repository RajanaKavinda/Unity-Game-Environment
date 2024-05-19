using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        GameObject.Find("Resume").GetComponent<Button>().onClick.AddListener(ResumeGame);
        GameObject.Find("Save & Quit").GetComponent<Button>().onClick.AddListener(SaveAndQuitGame);
    }

    public void ResumeGame()
    {
        // Load saved game data
        LoadSavedGameData();

        // Resume the game
        Time.timeScale = 1;
    }

    private void LoadSavedGameData()
    {
        // Check if PlayerController.Instance is not null
        if (PlayerController.Instance != null)
        {
            // Load player position
            float playerX = PlayerPrefs.GetFloat("PlayerX");
            float playerY = PlayerPrefs.GetFloat("PlayerY");
            float playerZ = PlayerPrefs.GetFloat("PlayerZ");
            Vector3 playerPosition = new Vector3(playerX, playerY, playerZ);
            PlayerController.Instance.transform.position = playerPosition;

            // Load coin count
            int coinCount = PlayerPrefs.GetInt("CoinCount");
            CoinManager.SetCoins(coinCount);
        }
        else
        {
            Debug.LogWarning("PlayerController.Instance is null. Cannot load game data.");
        }
    }

    public void SaveAndQuitGame()
    {
        SaveGame(); // Save game data
        Application.Quit(); // Quit the application
    }

    private void SaveGame()
    {
        // Check if PlayerController.Instance is not null
        if (PlayerController.Instance != null)
        {
            // Save player position
            Vector3 playerPosition = PlayerController.Instance.transform.position;
            PlayerPrefs.SetFloat("PlayerX", playerPosition.x);
            PlayerPrefs.SetFloat("PlayerY", playerPosition.y);
            PlayerPrefs.SetFloat("PlayerZ", playerPosition.z);

            // Save coin count
            PlayerPrefs.SetInt("CoinCount", CoinManager.currentCoins);

            // Save quit time
            PlayerPrefs.SetString("QuitTime", System.DateTime.Now.ToString());

            PlayerPrefs.Save(); // Save PlayerPrefs data to disk
        }
        else
        {
            Debug.LogWarning("PlayerController.Instance is null. Cannot save game data.");
        }
    }
}
