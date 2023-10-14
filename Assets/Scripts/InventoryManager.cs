using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public event EventHandler<OnItemInActiveSlotChangedEventArgs> OnItemInActiveSlotChanged;
    public class OnItemInActiveSlotChangedEventArgs : EventArgs
    {
        public Item item;
    }

    [HideInInspector] public bool isOpen = false;
    [HideInInspector] public int activeSlotNumber = 1;

    public static InventoryManager Instance { get; private set; }

    [SerializeField] private GameInput gameInput;
    [SerializeField] private InventoryController inventoryController;

    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        inventoryController.SelectSlot(activeSlotNumber);

        gameInput.OnOpenInventoryAction += GameInput_OnOpenInventoryAction;
        gameInput.OnSlotSelectionAction += GameInput_OnSlotSelectionAction;
        gameInput.OnDropItemAction += GameInput_OnDropItemAction; ;
    }

    private void GameInput_OnOpenInventoryAction(object sender, System.EventArgs e)
    {
        if (!isOpen)
            inventoryController.OpenInventory();
        else
            inventoryController.CloseInventory();

        isOpen = !isOpen;
    }

    private void GameInput_OnSlotSelectionAction(object sender, GameInput.OnSlotSelectionActionChangedEventArgs e)
    {
        inventoryController.DeselectSlot(activeSlotNumber);

        activeSlotNumber = e.slotNumber;

        inventoryController.SelectSlot(activeSlotNumber);
        ChangeItemInHand();
    }

    private void GameInput_OnDropItemAction(object sender, EventArgs e)
    {
        InventoryItem inventoryItem = inventoryController.GetInventoryItemInSlot(activeSlotNumber);

        if (inventoryItem != null)
        {
            inventoryController.DropItemToWorld(inventoryItem);
            ChangeItemInHand();
        }
    }

    public void PickUpItem(Item item)
    {
        inventoryController.AddItem(item);
        ChangeItemInHand();
    }

    public void DropItem(InventoryItem inventoryItem)
    {
        inventoryController.DropItemToWorld(inventoryItem);
        ChangeItemInHand();
    }

    public void ChangeItemInHand()
    {
        OnItemInActiveSlotChanged?.Invoke(this, new OnItemInActiveSlotChangedEventArgs
        {
            item = inventoryController.GetItemInSlot(activeSlotNumber)
        });
    }
}
