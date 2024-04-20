using UnityEngine;
using UnityEngine.UI;

public class QuitGame : MonoBehaviour
{
    // Reference to the button in the Unity Editor
    public Button Quit;

    void Start()
    {
        // Add a listener to the button's click event
        Quit.onClick.AddListener(Quiting);
    }

    void Quiting()
    {
        // Quit the application
        Application.Quit();
    }
}
