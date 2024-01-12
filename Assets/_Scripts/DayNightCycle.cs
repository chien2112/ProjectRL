using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField] private Light sun;
    [SerializeField, Range(0, 24)] private float timeOfDay;
    [SerializeField] private float sunRotationSpeed;

    [Header("Lighting Data Asset")]
    [SerializeField] private LightingDataAsset dataNight;
    [SerializeField] private LightingDataAsset dataDay;
    private LightingDataAsset currentData;

    [Header("PostProcessing")]
    [SerializeField] private Volume postProcessing;
    [SerializeField] private VolumeProfile postProcessingDay; 
    [SerializeField] private VolumeProfile postProcessingNight;

    [Header("VFX")]
    [SerializeField] private GameObject VFXDay;
    [SerializeField] private GameObject VFXNight;

    [Header("Lighting Preset")]
    [SerializeField] private Gradient equatorColor;
    [SerializeField] private Gradient skyColor;
    [SerializeField] private Gradient sunColor;

    [Header("Reflection Probe")]
    [SerializeField] ReflectionProbe reflectionProbe;
    [SerializeField] private int importanceDay;
    [SerializeField] private int importanceNight;
    [SerializeField] private Color backgroundColorDay;
    [SerializeField] private Color backgroundColorNight;

    [Header("Directional Light Day")]
    [SerializeField] private float strengOfShadowDay;
    [SerializeField] private float strengOfShadowNight;


    private void Update()
    {
        timeOfDay += Time.deltaTime * sunRotationSpeed;
        if(timeOfDay > 24)
        {
            timeOfDay = 0;
        }
        UpdateSunRotation();
        UpdateLighting();
    }
    private void OnValidate()
    {
        UpdateSunRotation();
        UpdateLighting();
    }
    private void UpdateSunRotation()
    {
        float sunRotation = Mathf.Lerp(-90, 270, timeOfDay / 24);
        sun.transform.rotation = Quaternion.Euler(sunRotation, sun.transform.rotation.y, sun.transform.rotation.z);
    }
    private void UpdateLighting()
    {
        float timeFraction = timeOfDay / 24;
        RenderSettings.ambientSkyColor = skyColor.Evaluate(timeFraction);
        RenderSettings.ambientEquatorColor = equatorColor.Evaluate(timeFraction);
        sun.color = sunColor.Evaluate(timeFraction);
        if(timeOfDay>6 && timeOfDay< 19)
        {
            if (currentData == dataDay) return;
            Lightmapping.lightingDataAsset = dataDay;
            postProcessing.profile = postProcessingDay;
            sun.shadowStrength = strengOfShadowDay;
            VFXDay.SetActive(true);
            VFXNight.SetActive(false);
            currentData = dataDay;

            reflectionProbe.backgroundColor = backgroundColorDay;
            reflectionProbe.importance = importanceDay;
        }
        else
        {
            if (currentData == dataNight) return;
            Lightmapping.lightingDataAsset = dataNight;
            postProcessing.profile = postProcessingNight;
            sun.shadowStrength = strengOfShadowNight;
            VFXDay.SetActive(false);
            VFXNight.SetActive(true);
            currentData = dataNight;
            reflectionProbe.backgroundColor = backgroundColorNight;
            reflectionProbe.importance = importanceNight;
        }
    }

}
