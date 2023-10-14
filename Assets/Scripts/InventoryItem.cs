using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Item item;
    public GameObject inventoryItemPrefab;

    [HideInInspector] public Transform parentAfterDrag;

    private Image image;

    public void Start()
    {
        InitialiseItem(item);
    }

    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        image = inventoryItemPrefab.GetComponent<Image>();
        image.sprite = item.GetItemSO().image;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;

        transform.SetParent(parentAfterDrag);
        if (parentAfterDrag != null)
        {
            transform.localPosition = Vector3.zero;
        }

        InventoryManager.Instance.ChangeItemInHand();
    }
}
