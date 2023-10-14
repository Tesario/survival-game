using System;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    static public PlayerLook Instance { get; private set; }

    public event EventHandler<OnSelectedItemChangedEventArgs> OnSelectedItemChanged;
    public class OnSelectedItemChangedEventArgs : EventArgs
    {
        public Item selectedItem;
    }

    [SerializeField] private float mouseSensitivity = 8f;
    [SerializeField] private Transform playerBody;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask itemLayerMask;

    private float xRotation = 0f;
    private Item selectedItem;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (selectedItem != null && !InventoryManager.Instance.isOpen)
        {
            InventoryManager.Instance.PickUpItem(selectedItem);
        }
    }

    void Update()
    {
        if (!InventoryManager.Instance.isOpen)
        {
            HandleLook();
            HandleInteractions();
        }
    }

    public void HandleInteractions()
    {
        float interactDistance = 2.5f;
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, interactDistance, itemLayerMask))
        {
            if (hit.transform.parent.TryGetComponent(out Item item))
            {
                if (selectedItem != item)
                {
                    OnSelectItem(item);
                }
            }
            else
            {
                OnSelectItem(null);
            }
        }
        else
        {
            OnSelectItem(null);
        }
    }

    private void OnSelectItem(Item selectedItem)
    {
        this.selectedItem = selectedItem;

        OnSelectedItemChanged?.Invoke(this, new OnSelectedItemChangedEventArgs
        {
            selectedItem = selectedItem
        });
    }

    public void HandleLook()
    {
        Vector2 mouseInput = gameInput.GetLookingAxis();
        float mouseX = mouseInput.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseInput.y * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
