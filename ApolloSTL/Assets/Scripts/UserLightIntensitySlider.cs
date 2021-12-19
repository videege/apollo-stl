using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserLightIntensitySlider : MonoBehaviour
{
    Slider slider;
    private UserLight userLight;
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

    public void SetUserLight(UserLight light)
    {
        userLight = light;
    }

    void SliderValueChanged(Slider change)
    {
        userLight.SetIntensity(slider.value);
    }
}
