using UnityEngine;

public class MusicController : MonoBehaviour
{
    // Reference to the GameMusic script
    public GameMusic gameMusic;

    // Reference to the button UI element
    public UnityEngine.UI.Button musicButton;

    // Variable to track if the music is currently playing
    private bool isMusicPlaying = true;

    // Static instance to make sure MusicController persists across scenes
    private static MusicController instance;

    private void Start()
    {
        // Check if an instance already exists
        if (instance == null)
        {
            // If not, set this as the instance and don't destroy it when loading new scenes
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If an instance already exists, destroy this one
            Destroy(gameObject);
            return;
        }

        // Add listener to the button click event
        musicButton.onClick.AddListener(ToggleMusic);
    }

    // Function to toggle between play and pause
    private void ToggleMusic()
    {
        if (isMusicPlaying)
        {
            // Pause the music
            gameMusic.PauseMusic();
            isMusicPlaying = false;
            // Change the button text to "Play"
            musicButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Play";
        }
        else
        {
            // Resume playing the music
            gameMusic.PlayMusic();
            isMusicPlaying = true;
            // Change the button text to "Pause"
            musicButton.GetComponentInChildren<UnityEngine.UI.Text>().text = "Pause";
        }
    }
}
