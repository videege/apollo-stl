using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LightController : MonoBehaviour
{
    [SerializeField] private Light ZenithalSunlight;
    [SerializeField] private Slider ZenithalIntensitySlider;
    [SerializeField] private GameObject UserLightPrefab;
    [SerializeField] private Canvas GUI;
    [SerializeField] private GameObject UserLightGUIPrefab;

    private List<Tuple<GameObject, GameObject>> UserLights = new List<Tuple<GameObject, GameObject>>();

    public float ZenithalLightIntensityMax = 2.0f;
    public bool ZenithalSunlightEnabled = true;

    public void ToggleZenithalSunlight()
    {
        ZenithalSunlightEnabled = !ZenithalSunlightEnabled;
        ZenithalSunlight.enabled = ZenithalSunlightEnabled;
        ZenithalIntensitySlider.enabled = ZenithalSunlightEnabled;    
        ZenithalIntensitySlider.interactable = ZenithalSunlightEnabled;         
    }

    public void SetZenithalIntensity(float multiplier)
    {
        ZenithalSunlight.intensity = ZenithalLightIntensityMax * multiplier;
    }

    public void AddLight(Transform transform)
    {
        if (UserLights.Count >= 5) return;

        var light = UnityEngine.Object.Instantiate(UserLightPrefab, transform, false);
        var lightController = light.GetComponent<UserLight>();

        var lightGUI = UnityEngine.Object.Instantiate(UserLightGUIPrefab, GUI.transform);
        UserLights.Add(new Tuple<GameObject, GameObject>(light, lightGUI));
        var lightNumber = UserLights.Count;
        var guiRectTransform = lightGUI.GetComponent<RectTransform>();
        var currentPos = lightGUI.transform.position;
        guiRectTransform.position = new Vector3(currentPos.x, currentPos.y - (guiRectTransform.rect.height * 2.0f * (lightNumber - 1)), currentPos.z);

        var lightToggle = lightGUI.GetComponentInChildren<UserLightToggle>();
        lightToggle.SetLight(lightController, lightNumber);
        
        var intensitySlider = lightGUI.GetComponentInChildren<UserLightIntensitySlider>();
        intensitySlider.SetUserLight(lightController);
        lightController.SetIntensitySlider(intensitySlider.GetComponent<Slider>());
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
