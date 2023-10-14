using System;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private int width = 20;
    [SerializeField] private int height = 20;
    [SerializeField] private float scale = .3f;
    [Range(0, 1)]
    [SerializeField] private float persistance;
    [SerializeField] private float lacunarity;
    [SerializeField] private int octaves;
    [SerializeField] private int seed;
    [SerializeField] private Vector2 offset;
    [SerializeField] private float heightMultiplier;
    [SerializeField] public bool autoUpdate = true;
    [SerializeField] private TerrainType[] terrainTypes;
    [SerializeField] private DisplayMode displayMode = DisplayMode.NOISE;
    enum DisplayMode
    {
        NOISE,
        COLOR,
        MESH,
    }

    private MapDisplay mapDisplay;

    [Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color color;
    }

    public void GenerateMap()
    {
        mapDisplay = GetComponent<MapDisplay>();
        float[,] noiseMap = Noise.GenerateNoiseMap(width, height, scale, octaves, persistance, lacunarity, seed, offset);

        switch (displayMode)
        {
            case DisplayMode.NOISE:
                mapDisplay.DrawNoiseMap(noiseMap);
                break;
            case DisplayMode.COLOR:
                mapDisplay.DrawColorMap(GenerateColorMap(noiseMap));
                break;
            case DisplayMode.MESH:
                mapDisplay.DrawMeshMap(noiseMap, heightMultiplier, GenerateColorMap(noiseMap));
                break;
        }
    }

    private Color[,] GenerateColorMap(float[,] noiseMap)
    {
        Color[,] colorMap = new Color[noiseMap.GetLength(0), noiseMap.GetLength(1)];

        for (int x = 0; x < colorMap.GetLength(0); x++)
        {
            for (int y = 0; y < colorMap.GetLength(1); y++)
            {
                foreach (TerrainType terrainType in terrainTypes)
                {
                    if (terrainType.height >= noiseMap[x, y])
                    {
                        colorMap[x, y] = terrainType.color;
                        break;
                    }
                }
            }
        }

        return colorMap;
    }

    private void OnValidate()
    {
        if (width < 1)
        {
            width = 1;
        }
        if (height < 1)
        {
            height = 1;
        }
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
    }
}
