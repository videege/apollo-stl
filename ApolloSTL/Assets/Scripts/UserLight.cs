using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserLight : MonoBehaviour
{
    public float LightIntensityMax = 2.0f;
    [SerializeField] private Transform target;
    private Light light;
    private Slider intensitySlider;
       
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        light.transform.LookAt(target.position);
    }

    public void SetIntensitySlider(Slider slider)
    {
        intensitySlider = slider;
    }

    public void Toggle()
    {
        light.enabled = !light.enabled;
        if (intensitySlider != null) {
            intensitySlider.enabled = light.enabled;
            intensitySlider.interactable = light.enabled;
        }
    }

    public void SetIntensity(float multiplier)
    {
        light.intensity = LightIntensityMax * multiplier;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
