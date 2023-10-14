using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    public event EventHandler OnOpenInventoryAction;
    public event EventHandler OnDropItemAction;
    public event EventHandler OnRunningAction;
    public event EventHandler OnStopRunningAction;
    public event EventHandler OnJumpAction;
    public event EventHandler<OnSlotSelectionActionChangedEventArgs> OnSlotSelectionAction;
    public class OnSlotSelectionActionChangedEventArgs : EventArgs
    {
        public int slotNumber;
    }

    PlayerInputActions playerInputActions;

    string[] SLOT_BINDING_NAMES = { "Slot 1 Selection", "Slot 2 Selection", "Slot 3 Selection", "Slot 4 Selection", "Slot 5 Selection" };

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.OpenInventory.performed += OpenInventory_performed;
        playerInputActions.Player.DropItem.performed += DropItem_performed;
        playerInputActions.Player.Running.performed += Running_performed;
        playerInputActions.Player.Running.canceled += Running_canceled; ;
        playerInputActions.Player.Jump.performed += Jump_performed;

        playerInputActions.Player.Slot1Selection.performed += SlotSelection_performed;
        playerInputActions.Player.Slot2Selection.performed += SlotSelection_performed;
        playerInputActions.Player.Slot3Selection.performed += SlotSelection_performed;
        playerInputActions.Player.Slot4Selection.performed += SlotSelection_performed;
        playerInputActions.Player.Slot5Selection.performed += SlotSelection_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void OpenInventory_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnOpenInventoryAction?.Invoke(this, EventArgs.Empty);
    }

    private void DropItem_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnDropItemAction?.Invoke(this, EventArgs.Empty);
    }

    private void Running_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnRunningAction?.Invoke(this, EventArgs.Empty);
    }

    private void Running_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnStopRunningAction?.Invoke(this, EventArgs.Empty);
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    private void SlotSelection_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSlotSelectionAction?.Invoke(this, new OnSlotSelectionActionChangedEventArgs
        {
            slotNumber = Array.IndexOf(SLOT_BINDING_NAMES, obj.action.name) + 1
        });
    }

    public Vector2 GetMovementVectorNormalized()
    {
        return playerInputActions.Player.Movement.ReadValue<Vector2>().normalized;
    }

    public Vector2 GetLookingAxis()
    {
        return playerInputActions.Player.Look.ReadValue<Vector2>();
    }
}
