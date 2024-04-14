using UnityEngine;

public class GameMusic : MonoBehaviour
{
    // The audio source component attached to this game object
    private AudioSource audioSource;

    


    private void Awake()
    {
        // Find the audio source component
        audioSource = GetComponent<AudioSource>();

        // Check if there are multiple instances of the game music
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
        if (musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            

            // Make the GameObject persist across scenes
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Function to change the volume of the game music
   
}
