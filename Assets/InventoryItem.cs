using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector2 initialPosition; // Store the initial position of the item

    public GameObject itemPrefab; // Reference to the prefab of the corresponding game object
    public Transform gridTransform; // Reference to the grid transform

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        initialPosition = rectTransform.anchoredPosition; // Store the initial position
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Calculate the drop position in world space
        Vector3 dropPosition = eventData.pointerCurrentRaycast.worldPosition;

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
        Vector3Int snappedPosition = new Vector3Int(
            Mathf.RoundToInt(position.x),
            Mathf.RoundToInt(position.y),
            Mathf.RoundToInt(position.z)
        );

        return gridTransform.TransformPoint(snappedPosition);
    }
}
