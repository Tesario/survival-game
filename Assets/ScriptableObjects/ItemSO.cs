using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/item")]
public class ItemSO : ScriptableObject
{
    [Header("Only gameplay")]
    public ItemType type;
    public Transform prefab;

    [Header("Only UI")]
    public Sprite image;

    [Header("Both")]
    public string title;
}

public enum ItemType
{
    Resource,
    Tool,
    Food,
    Weapon,
}