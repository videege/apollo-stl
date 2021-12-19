using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZenithalLightIntensitySlider : MonoBehaviour
{
    Slider slider;
    [SerializeField] private LightController lightController;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate {
            SliderValueChanged(slider);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SliderValueChanged(Slider change)
    {
        lightController.SetZenithalIntensity(slider.value);
    }
}
