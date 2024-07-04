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
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (inventoryManager.GetItemCount(itemType) <= 0)
        {
            return;
        }

        rectTransform.anchoredPosition += eventData.delta / gridTransform.localScale.x; // Consider the scale of the grid
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (inventoryManager.GetItemCount(itemType) <= 0)
        {
            return;
        }

        // Use a fixed Z-depth for the world point conversion
        Vector3 worldPoint = mainCamera.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, mainCamera.nearClipPlane)); // Adjust nearClipPlane to an appropriate depth if needed
        

        // Snap the drop position to the grid
        Vector3 snappedPosition = SnapToGrid(worldPoint);
        

        // Instantiate the corresponding game object in the game environment at the snapped position
        GameObject newObject = Instantiate(itemPrefab, snappedPosition, Quaternion.identity);

        // Optionally, parent the new object if needed
        if (gridTransform != null)
        {
            newObject.transform.SetParent(gridTransform, true);
        }

        // Reduce item count
        inventoryManager.DecreaseItemCount(itemType);

        // Add the new item to the SaveManager's list of placed items
        SaveManager.Instance.AddPlacedItem(newObject);

        // Move the UI item back to its initial position
        rectTransform.anchoredPosition = initialPosition;
    }

    private Vector3 SnapToGrid(Vector3 position)
    {
        // Implement your grid snapping logic here
        float gridSize = 1.0f; // Set your grid size
        float snappedX = Mathf.Round(position.x / gridSize) * gridSize;
        float snappedY = Mathf.Round(position.y / gridSize) * gridSize;
        return new Vector3(snappedX, snappedY, position.z);
    }
}
