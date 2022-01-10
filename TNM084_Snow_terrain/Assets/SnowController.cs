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
        
        materials.SetFloat("_SnowAmount", 0.0f); // Set start Snow Amount to value 0

    }

    void Update()
    {
        StartCoroutine(changeSnowAmount()); // Wait 3 secs. for snow to fall, before adding snow to terrain

        float emitterValue = slider.value; 
        emissionModule.rate = emitterValue; // Set 'emission' value of particle system to slider

        float prevSnowAm = materials.GetFloat("_SnowAmount"); // Get previous amount of snow, to be added with new amount of snow
        materials.SetFloat("_SnowAmount", prevSnowAm + (emitterValue / 99999f)); // Divide with a large number to not increase snow unneccesarilly fast


        stormButton.onClick.AddListener(StormEffect);


    }

    
    void StormEffect() // Create snowstorm effect
    {
        var noise = myParticleSystem.noise;
        noise.strength = 10f; // Add even more noise to the particle system
        noise.positionAmount = 3f;
        noise.rotationAmount = 3f;
        noise.sizeAmount = 1f;


    }

    IEnumerator changeSnowAmount()
    {
        yield return new WaitForSeconds(3);

    }
}
