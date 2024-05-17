using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public RectTransform rectTransform;
    private Vector2 initialPosition; // Store the initial position of the item

    public GameObject itemPrefab; // Reference to the prefab of the corresponding game object
    public Transform gridTransform; // Reference to the grid transform
    public Transform parentTransform; // Reference to the parent transform to attach to

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
        rectTransform.anchoredPosition += eventData.delta / gridTransform.localScale.x; // Consider the scale of the grid
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Convert the drop position from screen space to world space
        Vector3 dropPosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out dropPosition);

        // Snap the drop position to the grid
        Vector3 snappedPosition = SnapToGrid(dropPosition);

        // Instantiate the corresponding game object in the game environment at the snapped position
        GameObject newObject = Instantiate(itemPrefab, snappedPosition, Quaternion.identity);

        // Attach the new object to the parent if needed
        if (parentTransform != null)
        {
            newObject.transform.parent = parentTransform;
        }

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
