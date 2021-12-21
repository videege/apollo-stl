using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class LightController : MonoBehaviour
{
    [SerializeField] private Light ZenithalSunlight;
    [SerializeField] private Slider ZenithalIntensitySlider;
    [SerializeField] private GameObject UserLightPrefab;
    [SerializeField] private Canvas GUI;
    [SerializeField] private GameObject UserLightGUIPrefab;
    [SerializeField] public ColorPickerController ColorPickerController;
    [SerializeField] public GameObject Model;

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

        Debug.Log($"Camera position: {transform.position}");
        var light = Instantiate(UserLightPrefab, transform.position, 
            Quaternion.LookRotation(Model.GetComponent<MeshRenderer>().bounds.center - transform.position));
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

        var deleteButton = lightGUI.GetComponentInChildren<UserLightDeleteButton>();
        deleteButton.Setup(this, lightController);

        var colorPicker = lightGUI.GetComponentInChildren<LightColorController>();
        colorPicker.SetLight(light.GetComponent<Light>());
    }

    public void DeleteUserLight(UserLight userLight)
    {
        var entry = UserLights.FirstOrDefault(x => x.Item1.GetComponent<UserLight>() == userLight);
        if (entry == null)
        {
            Debug.LogWarning("Attempted to delete unfound user light.");
            return;
        }

        var index = UserLights.IndexOf(entry);
        // close the color picker if it's open
        var colorPicker = GameObject.FindGameObjectWithTag("ColorPickerController");
        if (colorPicker != null)
            colorPicker.SetActive(false);

        Destroy(entry.Item1);
        Destroy(entry.Item2);

        UserLights.Remove(entry);

        for (var i = index; i < UserLights.Count; i++)
        {
            var lightGUI = UserLights[i].Item2;
            // Move the light up a slot
            var guiRectTransform = lightGUI.GetComponent<RectTransform>();
            var currentPos = lightGUI.transform.position;
            guiRectTransform.position = new Vector3(currentPos.x, currentPos.y + (guiRectTransform.rect.height * 2.0f), currentPos.z);
            // Relabel the light
            var lightToggle = lightGUI.GetComponentInChildren<UserLightToggle>();
            lightToggle.UpdateNumber(i + 1);
        }
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
