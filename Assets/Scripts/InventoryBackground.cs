using UnityEngine;
using UnityEngine.EventSystems;


public class InventoryBackground : MonoBehaviour, IDropHandler
{
    [SerializeField] private InventoryManager inventoryManager;

    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        inventoryManager.DropItem(inventoryItem);
    }
}
