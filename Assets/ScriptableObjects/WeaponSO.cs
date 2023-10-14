using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable object/weapon")]
public class WeaponSO : ItemSO
{
    [Header("Gameplay Only")]
    public GameObject bullet;

    [Header("Both")]
    public int maxAmmo = 30;
    public int durability = 100;
}
