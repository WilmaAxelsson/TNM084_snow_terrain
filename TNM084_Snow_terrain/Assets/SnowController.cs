using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnowController : MonoBehaviour
{
    ParticleSystem myParticleSystem;
    ParticleSystem.EmissionModule emissionModule;
    public Slider slider;

    [SerializeField] private Terrain terrain;

    [SerializeField] private Button stormButton;

    [SerializeField] private Material materials;

    void Start()
    {
        myParticleSystem = GetComponent<ParticleSystem>();
        emissionModule = myParticleSystem.emission;
        slider = GameObject.Find("Slider").GetComponent<Slider>();

        
        materials.SetFloat("_SnowAmount", 0.0f);

    }

    void Update()
    {
        StartCoroutine(changeSnowAmount());

        float emitterValue = slider.value;
        emissionModule.rate = emitterValue;

     

        float prevSnowAm = materials.GetFloat("_SnowAmount");
        materials.SetFloat("_SnowAmount", prevSnowAm + (emitterValue / 99999f)); // Divide with a large number to not increase snow unneccesarilly fast


        stormButton.onClick.AddListener(StormEffect);

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

    void StormEffect()
    {
        var noise = myParticleSystem.noise; 
        noise.positionAmount = 7f; // Add even more noise to the particle system
        noise.rotationAmount = 7f;
        noise.sizeAmount = 7f;


    }

    IEnumerator changeSnowAmount()
    {
        yield return new WaitForSeconds(3);

    }
}
