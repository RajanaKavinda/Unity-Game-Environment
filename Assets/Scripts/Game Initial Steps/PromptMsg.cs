using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PromptMsg : MonoBehaviour
{
    public GameObject PromptPanel;
    public Text PromptText;

    private void Start()
    {
        HidePrompt();
    }

    public void ShowPrompt(string message)
    {
        PromptText.text = message;
        PromptPanel.SetActive(true);

        // Start a coroutine to hide the prompt after 1 second
        StartCoroutine(HidePromptAfterDelay(1f));
    }

    private IEnumerator HidePromptAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Hide the prompt after the delay
        HidePrompt();
    }

    public void HidePrompt()
    {
        PromptPanel.SetActive(false);
    }
}
