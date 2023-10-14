using UnityEngine;

public class SelectItemVisual : MonoBehaviour
{
    [SerializeField] private Item item;
    [SerializeField] public GameObject selectItemVisual;

    private void Start()
    {
        PlayerLook.Instance.OnSelectedItemChanged += PlayerLook_OnSelectedItemChanged;
    }

    private void PlayerLook_OnSelectedItemChanged(object sender, PlayerLook.OnSelectedItemChangedEventArgs e)
    {
        if (e.selectedItem == item)
        {
            selectItemVisual.SetActive(true);
        }
        else
        {
            selectItemVisual.SetActive(false);
        }
    }
}
