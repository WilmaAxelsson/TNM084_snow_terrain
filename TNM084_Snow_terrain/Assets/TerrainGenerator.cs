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

    void randomOffset() //Generate a new, random terrain each time
    {
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);

        scale = Random.Range(5f, 40f);
}

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        Debug.Log(terrainData.heightmapResolution);

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
