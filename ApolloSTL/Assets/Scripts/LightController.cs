using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    [SerializeField] private Light ZenithalSunlight;
    [SerializeField] private Slider ZenithatlIntensitySlider;

    public float ZenithalLightIntensityMax = 2.0f;
    public bool ZenithalSunlightEnabled = true;

    public void ToggleZenithalSunlight()
    {
        ZenithalSunlightEnabled = !ZenithalSunlightEnabled;
        ZenithalSunlight.enabled = ZenithalSunlightEnabled;
        ZenithatlIntensitySlider.enabled = ZenithalSunlightEnabled;    
        ZenithatlIntensitySlider.interactable = ZenithalSunlightEnabled;         
    }

    public void SetZenithalIntensity(float multiplier)
    {
        ZenithalSunlight.intensity = ZenithalLightIntensityMax * multiplier;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
