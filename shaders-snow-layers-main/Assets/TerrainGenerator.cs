
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{


    public int depth = 7;

    public int width = 100;
    public int height = 100;

    public float scale = 20f;

    public float offsetX = 100f;
    public float offsetY = 100f;



    void Start()
    {
        //Generate a new, random terrain each time:
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);

    }

    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;

        terrainData.size = new Vector3(width, depth, height); //x, y, z

        terrainData.SetHeights(0, 0, GenerateHeights());

        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, height];
        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y); // CalculateHeights returns the Perlin noise for new Height value
            }
        }
        return heights;
    }

    float CalculateHeight(int x, int y) // Using Perlin noise
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        float noise = Mathf.PerlinNoise(xCoord, yCoord);

        return noise;
    }
}
