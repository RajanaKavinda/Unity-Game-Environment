using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0; // Pause the game
        SceneManager.LoadScene("Game Menu"); // Load the menu scene
    }
}
