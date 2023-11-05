using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private Renderer textureRender;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshCollider meshCollider;
    [SerializeField] private Mesh planeMesh;

    private MeshGenerator meshGenerator = new MeshGenerator();
    private TextureGenerator textureGenerator = new TextureGenerator();

    public void DrawNoiseMap(float[,] noiseMap, float resolution)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Color[] colorArr = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colorArr[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }

        Texture2D texture = textureGenerator.GenerateTexture(colorArr, width, height);

        textureRender.sharedMaterial.mainTexture = texture;
        meshFilter.mesh = planeMesh;

        transform.localScale = new Vector3(width / resolution, 1, height / resolution);
    }

    public void DrawColorMap(Color[,] colorMap, float resolution)
    {
        int width = colorMap.GetLength(0);
        int height = colorMap.GetLength(1);

        Color[] colorArr = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colorArr[y * width + x] = colorMap[x, y];
            }
        }

        Texture2D texture = textureGenerator.GenerateTexture(colorArr, width, height);

        textureRender.sharedMaterial.mainTexture = texture;
        meshFilter.mesh = planeMesh;

        transform.localScale = new Vector3(width / resolution, 1, height / resolution);
    }

    public void DrawMeshMap(float[,] noiseMap, float heightMultiplier, AnimationCurve meshHeightCurve, Color[,] colorMap, float resolution)
    {
        Mesh mesh = meshGenerator.CreateMesh(noiseMap, heightMultiplier, meshHeightCurve, resolution);
        meshFilter.sharedMesh = mesh;
        meshCollider.sharedMesh = mesh;

        int width = colorMap.GetLength(0);
        int height = colorMap.GetLength(1);

        Color[] colorArr = new Color[width * height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colorArr[y * width + x] = colorMap[x, y];
            }
        }

        Texture2D texture = textureGenerator.GenerateTexture(colorArr, width, height);
        textureRender.sharedMaterial.mainTexture = texture;

        transform.localScale = new Vector3(width / resolution, 1, height / resolution);
    }
}
