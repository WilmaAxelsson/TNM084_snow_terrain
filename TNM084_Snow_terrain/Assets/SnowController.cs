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


    }

    void StormEffect()
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
