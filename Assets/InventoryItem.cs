using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas; // Reference to the canvas for scaling
    private Vector2 initialPosition; // Store the initial position of the item

    public GameObject itemPrefab; // Reference to the prefab of the corresponding game object
    public Transform gridTransform; // Reference to the grid transform

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPosition = rectTransform.anchoredPosition; // Store the initial position
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; // Consider the scale of the canvas
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Calculate the drop position in world space
        Vector3 dropPosition = eventData.position;

        // Snap the drop position to the grid
        Vector3 snappedPosition = SnapToGrid(dropPosition);

        // Instantiate the corresponding game object in the game environment at the snapped position
        Instantiate(itemPrefab, snappedPosition, Quaternion.identity);

        // Return the item to its initial position
        rectTransform.anchoredPosition = initialPosition;
    }

    // Snap the given position to the nearest grid point
    private Vector3 SnapToGrid(Vector3 position)
    {
        Vector3 localPosition = gridTransform.InverseTransformPoint(position);

        // Round to the nearest grid position
        Vector3 snappedPosition = new Vector3(
            Mathf.Round(localPosition.x),
            Mathf.Round(localPosition.y),
            Mathf.Round(localPosition.z)
        );

        return gridTransform.TransformPoint(snappedPosition);
    }
}
