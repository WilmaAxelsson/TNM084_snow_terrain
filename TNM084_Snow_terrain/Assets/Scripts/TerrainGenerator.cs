using UnityEngine;
using UnityEngine.UI;

public class TerrainGenerator : MonoBehaviour
{
    public int depth = 30;

    public int width = 500;
    public int height = 500;

    public float scale = 20f;

    public float offsetX = 100f;
    public float offsetY = 100f;

    [SerializeField] private Button terrainButton;

    void Start()
    {
        randomOffset();
    }

    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

        terrainButton.onClick.AddListener(randomOffset); //If "Change terrain" button is clicked, generate a new random terrain using new random offset and scale values
        
    }

    void randomOffset() //Generate a new, random terrain each time using different scale and offset values
    {
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);

        scale = Random.Range(5f, 40f);
}

    TerrainData GenerateTerrain(TerrainData terrainData) // Set new terrain
    {
        terrainData.heightmapResolution = width + 1;

        terrainData.size = new Vector3(width, depth, height);

        terrainData.SetHeights(0, 0, GenerateHeights()); //Set new heights for terrain

        return terrainData;
    }

    float[,] GenerateHeights() //Generates new coordinates for terrain
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

    float CalculateHeight(int x, int y) //Calculate new heights of terrain
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        // Fractal Brownian Motion (FBM)
        int octaves = 3;
        float amplitude = 0.5f;
        float frequency = 1f;
        float noise = 0;

        for (var oc = 0; oc < octaves; oc++)
        {
            noise += amplitude * Mathf.PerlinNoise(xCoord * frequency, yCoord * frequency); //Add Perlin noise
            amplitude *= 0.5f;
            frequency *= 2f;
        }

        return noise;
    }
}
