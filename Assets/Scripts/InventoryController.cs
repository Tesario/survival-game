using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private InventorySlot[] inventorySlots;
    [SerializeField] private GameObject inventory;
    [SerializeField] private Image background;

    private float openingTransition = 0.2f;

    public void OpenInventory()
    {
        Cursor.lockState = CursorLockMode.Confined;

        inventory.transform.localScale = Vector3.zero;
        inventory.LeanScale(Vector3.one, openingTransition).setEaseInSine();

        background.rectTransform.LeanAlpha(0.75f, openingTransition);
    }

    public void CloseInventory()
    {
        Cursor.lockState = CursorLockMode.Locked;

        inventory.LeanScale(Vector3.zero, openingTransition).setEaseOutSine();

        background.rectTransform.LeanAlpha(0, openingTransition);
    }

    public void SelectSlot(int slotNumber)
    {
        inventorySlots[slotNumber - 1].Select();
    }

    public void DeselectSlot(int slotNumber)
    {
        inventorySlots[slotNumber - 1].Deselect();
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            Item itemInSlot = slot.GetComponentInChildren<Item>();

            if (itemInSlot == null)
            {
                MoveItemToUI(item, slot.transform);
                break;
            }
        }
    }

    private void MoveItemToUI(Item item, Transform slot)
    {
        GameObject inventoryItemPrefab = item.GetComponentInChildren<InventoryItem>().inventoryItemPrefab;

        inventoryItemPrefab.SetActive(true);

        item.itemPrefab.SetActive(false);
        item.transform.SetParent(slot.transform, false);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;
    }

    public void DropItemToWorld(InventoryItem inventoryItem)
    {
        Vector3 dropPos = Camera.main.transform.position;
        float dropRotationY = Camera.main.transform.rotation.eulerAngles.y;
        Transform player = Camera.main.transform.parent;
        GameObject itemPrefab = inventoryItem.GetComponent<Item>().itemPrefab;

        inventoryItem.inventoryItemPrefab.SetActive(false);
        inventoryItem.parentAfterDrag = null;
        inventoryItem.transform.parent = null;
        inventoryItem.transform.position = dropPos + player.TransformDirection(new Vector3(0, -0.7f, 1));
        inventoryItem.transform.rotation = Quaternion.Euler(0, dropRotationY, 0);

        itemPrefab.SetActive(true);
        itemPrefab.GetComponent<Rigidbody>().velocity = inventoryItem.transform.TransformDirection(new Vector3(0, 0, 1.5f));
        itemPrefab.transform.localPosition = Vector3.zero;
        itemPrefab.transform.localRotation = Quaternion.identity;
    }

    public Item GetItemInSlot(int slotNumber)
    {
        return inventorySlots[slotNumber - 1].GetComponentInChildren<Item>();
    }

    public InventoryItem GetInventoryItemInSlot(int slotNumber)
    {
        return inventorySlots[slotNumber - 1].GetComponentInChildren<InventoryItem>();
    }
}
