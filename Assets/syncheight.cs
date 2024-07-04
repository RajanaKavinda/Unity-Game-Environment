using UnityEngine;

public class SyncHeight : MonoBehaviour
{
    public RectTransform target; // The target RectTransform to sync height with
    public RectTransform panel; // The RectTransform of the panel to adjust

    private void Update()
    {
        if (target != null && panel != null)
        {
            // Sync the height of the panel with the target's height
            panel.sizeDelta = new Vector2(panel.sizeDelta.x, target.rect.height);
        }
    }
}
