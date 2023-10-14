using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private Renderer textureRender;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private Mesh planeMesh;

    private MeshGenerator meshGenerator = new MeshGenerator();
    private TextureGenerator textureGenerator = new TextureGenerator();

    public void DrawNoiseMap(float[,] noiseMap)
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
        textureRender.transform.localScale = new Vector3(width, 1, height);

        meshFilter.mesh = planeMesh;
    }

    public void DrawColorMap(Color[,] colorMap)
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
        textureRender.transform.localScale = new Vector3(width, 1, height);

        meshFilter.mesh = planeMesh;
    }

    public void DrawMeshMap(float[,] noiseMap, float heightMultiplier, Color[,] colorMap)
    {
        Mesh mesh = meshGenerator.CreateMesh(noiseMap, heightMultiplier);

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
        textureRender.transform.localScale = new Vector3(width, 1, height);

        meshFilter.mesh = mesh;
    }
}
