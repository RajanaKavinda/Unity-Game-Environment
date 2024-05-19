using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

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

    public void SaveGame()
    { 
        SavePlayerState();
        PlayerPrefs.Save();
    }

    private void SavePlayerState()
    {
        PlayerController playerController = PlayerController.Instance;
        if (playerController != null)
        {
            Vector3 playerPosition = playerController.transform.position;
            PlayerPrefs.SetFloat("PlayerX", playerPosition.x);
            PlayerPrefs.SetFloat("PlayerY", playerPosition.y);
            PlayerPrefs.SetFloat("PlayerZ", playerPosition.z);
            PlayerPrefs.SetInt("Score", CoinManager.currentCoins);
            Debug.Log("Saved player state.");
        }
        else
        {
            Debug.LogWarning("PlayerController not found. Cannot save player state.");
        }
    }

    public void LoadGame()
    {
        LoadPlayerState();
        LoadBarrierStates();
    }

    private void LoadPlayerState()
    {
        PlayerController playerController = PlayerController.Instance;
        if (PlayerPrefs.HasKey("PlayerX") && PlayerPrefs.HasKey("PlayerY") && PlayerPrefs.HasKey("PlayerZ"))
        {
            Vector3 playerPosition = new Vector3(
                PlayerPrefs.GetFloat("PlayerX"),
                PlayerPrefs.GetFloat("PlayerY"),
                PlayerPrefs.GetFloat("PlayerZ")
            );
            playerController.SetPlayerPosition(playerPosition);
        }

        if (PlayerPrefs.HasKey("Score"))
        {
            CoinManager.currentCoins = PlayerPrefs.GetInt("Score");
        }

        Debug.Log("Loaded player state.");
    }

    private void LoadBarrierStates()
    {
        Barrier[] barriers = FindObjectsOfType<Barrier>();
        foreach (Barrier barrier in barriers)
        {
            barrier.LoadBarrierState();           
        }
        Debug.Log("Loaded barrier states.");
    }
}
