using UnityEngine;

public class ItemInHand : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;

    private ItemInHandAnimator animator;
    private Item selectedItem;

    void Start()
    {
        animator = GetComponent<ItemInHandAnimator>();
        inventoryManager.OnItemInActiveSlotChanged += InventoryManager_OnItemInActiveSlotChanged;
    }

    private void InventoryManager_OnItemInActiveSlotChanged(object sender, InventoryManager.OnItemInActiveSlotChangedEventArgs e)
    {
        if (e.item != null)
        {
            if (e.item != selectedItem)
            {
                selectedItem = e.item;
                transform.GetComponent<MeshFilter>().sharedMesh = e.item.GetItemSO().prefab.GetComponent<MeshFilter>().sharedMesh;
                transform.GetComponent<MeshRenderer>().sharedMaterials = e.item.GetItemSO().prefab.GetComponent<MeshRenderer>().sharedMaterials;
                transform.localScale = e.item.GetItemSO().prefab.localScale;
                animator.AnimatePickUpItem();
            }
        }
        else
        {
            selectedItem = null;
            transform.GetComponent<MeshFilter>().mesh.Clear();
            transform.GetComponent<MeshRenderer>().materials = new Material[0];
        }

    }
}
