using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightColorController : MonoBehaviour, IColorReceiver
{
    [SerializeField] private Light light;

    private Button button;
    private Color currentColor;

    public void SetLight(Light light)
    {
        this.light = light;
    }

    public void Cancel()
    {
        light.color = currentColor;
        button.GetComponent<Image>().color = light.color;
    }

    public void PreviewColor(Color color)
    {
        light.color = color;
        button.GetComponent<Image>().color = light.color;
    }

    public void SetColor(Color color)
    {
        light.color = color;
        button.GetComponent<Image>().color = light.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenColorPicker);
        button.GetComponent<Image>().color = light.color;
    }

    private void OpenColorPicker()
    {
        var lightController = GameObject.FindGameObjectWithTag("LightController").GetComponent<LightController>();
        lightController.ColorPickerController.OpenColorPicker(light.color, this);
        currentColor = light.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
