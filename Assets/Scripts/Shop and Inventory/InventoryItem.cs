using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector2 initialPosition; // Store the initial position of the item

    public GameObject itemPrefab; // Reference to the prefab of the corresponding game object
    public Transform gridTransform; // Reference to the grid transform
    public Camera mainCamera; // Reference to the main camera
    public InventoryManager inventoryManager; // Reference to the Inventory Manager
    public int itemType; // The type of item this script handles

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (inventoryManager.GetItemCount(itemType) <= 0)
        {
            Debug.Log("No items left to drag!");
            return;
        }

        initialPosition = rectTransform.anchoredPosition; // Store the initial position
        Debug.Log("Initial Position: " + initialPosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (inventoryManager.GetItemCount(itemType) <= 0)
        {
            return;
        }

        rectTransform.anchoredPosition += eventData.delta / gridTransform.localScale.x; // Consider the scale of the grid
        Debug.Log("Dragged Position: " + rectTransform.anchoredPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (inventoryManager.GetItemCount(itemType) <= 0)
        {
            return;
        }

        // Use a fixed Z-depth for the world point conversion
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, mainCamera.nearClipPlane)); // Adjust nearClipPlane to an appropriate depth if needed
        Debug.Log("World Pointer Position: " + worldPoint);

        // Snap the drop position to the grid
        Vector3 snappedPosition = SnapToGrid(worldPoint);
        Debug.Log("Snapped Position: " + snappedPosition);

        // Instantiate the corresponding game object in the game environment at the snapped position
        GameObject newObject = Instantiate(itemPrefab, snappedPosition, Quaternion.identity);

        // Optionally, parent the new object if needed
        if (gridTransform != null)
        {
            newObject.transform.SetParent(gridTransform, true);
        }

        // Reduce item count
        inventoryManager.DecreaseItemCount(itemType);

        // Ensure the new object is visible
        newObject.transform.position = new Vector3(newObject.transform.position.x, newObject.transform.position.y, 0.0f);

        // Return the item to its initial position
        rectTransform.anchoredPosition = initialPosition;
    }

    // Snap the given position to the nearest grid point
    private Vector3 SnapToGrid(Vector3 position)
    {
        Vector3 localPosition = gridTransform.InverseTransformPoint(position);

        // Adjust to grid spacing if necessary (assuming grid spacing of 1 unit)
        float gridSpacing = 1.0f; // Adjust this value based on your grid size
        Vector3 snappedPosition = new Vector3(
            Mathf.Round(localPosition.x / gridSpacing) * gridSpacing,
            Mathf.Round(localPosition.y / gridSpacing) * gridSpacing,
            Mathf.Round(localPosition.z / gridSpacing) * gridSpacing
        );

        return gridTransform.TransformPoint(snappedPosition);
    }
}
