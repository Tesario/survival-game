using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Color32 selectColor;

    private Color defaultColor;

    public void Start()
    {
        defaultColor = transform.GetComponent<Image>().color;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
    }

    public void Select()
    {
        transform.GetComponent<Image>().color = selectColor;
    }

    public void Deselect()
    {
        transform.GetComponent<Image>().color = defaultColor;
    }
}
