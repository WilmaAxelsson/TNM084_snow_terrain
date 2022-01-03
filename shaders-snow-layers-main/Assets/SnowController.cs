using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowController : MonoBehaviour
{
    ParticleSystem myParticleSystem;
    ParticleSystem.EmissionModule emissionModule;
    public Slider slider;

    // public GameObject sphere;

    public int height = 100;
    public int width = 100;
    [SerializeField] private Terrain terrain;


    [SerializeField] private Material materials;

    void Start()
    {
        myParticleSystem = GetComponent<ParticleSystem>();
        emissionModule = myParticleSystem.emission;
        slider = GameObject.Find("Slider").GetComponent<Slider>();

        
        materials.SetFloat("_SnowAmount", 0.0f);


        //   sphere = GameObject.Find("Sphere"); // Find the Sphere object that generates the snow

    }

    void Update()
    {
        StartCoroutine(changeSnowAmount());

        float emitterValue = slider.value;
        emissionModule.rate = emitterValue; // Need to multiply by 500 to see results on emission of snow 

       

        float prevSnowAm = materials.GetFloat("_SnowAmount");
        materials.SetFloat("_SnowAmount", prevSnowAm + (emitterValue / 999999f)); // Divide with a large number to not increase snow unneccesarilly fast
        

        //sphere.GetComponent<Renderer>().sharedMaterial.SetFloat("Snow Amount", emitterValue);

        /* if (materials[i].GetFloat("_SnowAmount") >= 0.6f)
         {
             int xRes = terrain.terrainData.heightmapWidth;

             int yRes = terrain.terrainData.heightmapHeight;


             float[,] heights = terrain.terrainData.GetHeights(width, height, width, height);


             heights[10, 10] = 1f;
             heights[20, 20] = 0.5f;

             terrain.terrainData.SetHeights(width, height, heights);



         }*/


    }

    IEnumerator changeSnowAmount()
    {
        yield return new WaitForSeconds(3);
        


    }
}
