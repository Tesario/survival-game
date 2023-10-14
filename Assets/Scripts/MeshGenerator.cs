using UnityEngine;

public class MeshGenerator
{
    int[] triangels;
    Vector3[] vertices;
    Vector2[] uvs;

    public Mesh CreateMesh(float[,] noiseMap, float heightMultiplier)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / -2f;

        vertices = new Vector3[(width) * (height)];
        uvs = new Vector2[vertices.Length];

        int i = 0;
        for (int w = 0; w < width; w++)
        {
            for (int h = 0; h < height; h++)
            {
                float y = noiseMap[w, h] * heightMultiplier;
                vertices[i] = new Vector3(topLeftX + w, y, topLeftZ + h);
                uvs[i] = new Vector2(w / (float)width, h / (float)height);
                i++;
            }
        }

        triangels = new int[(width - 1) * (height - 1) * 6];

        int tris = 0;
        int vert = 0;

        for (int w = 0; w < width - 1; w++)
        {
            for (int h = 0; h < height - 1; h++)
            {

                triangels[tris + 0] = vert + 0;
                triangels[tris + 1] = vert + 1;
                triangels[tris + 2] = vert + height - 1 + 1;
                triangels[tris + 3] = vert + 1;
                triangels[tris + 4] = vert + height - 1 + 2;
                triangels[tris + 5] = vert + height - 1 + 1;

                vert++;
                tris += 6;
            }
            vert++;
        }

        Mesh mesh = new Mesh();
        mesh.name = "Map";
        mesh.vertices = vertices;
        mesh.triangles = triangels;
        mesh.uv = uvs;
        mesh.RecalculateBounds();

        return mesh;
    }
}
