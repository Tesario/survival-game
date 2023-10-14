using UnityEngine;

public class Item : MonoBehaviour, IItem
{
    [SerializeField] private ItemSO itemSO;
    [SerializeField] private GameObject selectItemVisual;

    public GameObject itemPrefab;

    private void Start()
    {
        InitializeItem();
    }

    public void InitializeItem()
    {
        Mesh mesh = itemSO.prefab.GetComponent<MeshFilter>().sharedMesh;
        Material[] materials = itemSO.prefab.GetComponent<MeshRenderer>().sharedMaterials;

        itemPrefab.GetComponent<MeshFilter>().sharedMesh = mesh;
        itemPrefab.GetComponent<MeshRenderer>().sharedMaterials = materials;
        itemPrefab.GetComponent<MeshCollider>().sharedMesh = mesh;
        itemPrefab.transform.localScale = itemSO.prefab.transform.localScale;
        selectItemVisual.GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    public ItemSO GetItemSO()
    {
        return itemSO;
    }

    public void InteractPrimaryAction()
    {
        // fdas
    }

    public void InteractSecondaryAction()
    {
        // tea
    }
}
