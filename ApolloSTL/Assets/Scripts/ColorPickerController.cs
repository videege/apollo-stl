using System;
using System.Collections;
using System.Collections.Generic;
using HSVPicker;
using UnityEngine;
using UnityEngine.UI;

public class ColorPickerController : MonoBehaviour
{
    [SerializeField] ColorPicker colorPicker;
    [SerializeField] Button okButton;
    [SerializeField] Button cancelButton;
    private IColorReceiver currentReceiver;
    // Start is called before the first frame update
    void Start()
    {
        colorPicker.onValueChanged.AddListener(color =>
        {
            if (currentReceiver == null)
            {
                Debug.LogWarning("Color changed but no listener registered.");
                return;
            }
            currentReceiver.PreviewColor(color);
        });

        okButton.onClick.AddListener(ClickedOK);
        cancelButton.onClick.AddListener(ClickedCancel);
        gameObject.SetActive(false);
    }

    public void OpenColorPicker(Color existingColor, IColorReceiver receiver)
    {
        currentReceiver = receiver;
        colorPicker.CurrentColor = existingColor;        
        gameObject.SetActive(true);
    }

    private void ClickedCancel()
    {
        currentReceiver.Cancel();
        gameObject.SetActive(false);
        currentReceiver = null;
    }

    private void ClickedOK()
    {
        currentReceiver.SetColor(colorPicker.CurrentColor);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
