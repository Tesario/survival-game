using UnityEngine;

public class SpawningItemController : MonoBehaviour
{
    public Terrain terrain;
    public Item[] items;
    public GameObject itemPrefab;
    public int spawnCount;

    private void SpawnItem(Vector3 vector)
    {
        GameObject newItem = Instantiate(itemPrefab, vector, Quaternion.identity);
        newItem.transform.position += new Vector3(0, newItem.GetComponent<Collider>().bounds.size.y / 2, 0);
    }

    void Start()
    {
        Vector3 groundSize = terrain.GetComponent<Collider>().bounds.size;
        Vector3 groundPos = terrain.transform.position;
        float y = groundPos.y + (groundSize.y / 2);

        for (int i = 0; i < spawnCount; i++)
        {
            float x = Random.Range(groundPos.x, groundSize.x);
            float z = Random.Range(groundPos.z, groundSize.z);
            SpawnItem(new Vector3(x, y, z));
        }
    }
}
