using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Axis
{
    X = 0,
    Y = 1,
    Z = 2
}

public class ModelRotator : MonoBehaviour
{
    [SerializeField] private GameObject model;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Axis axis;
    [SerializeField] private bool isPositiveDirection;
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        var rotation = isPositiveDirection ? 45 : -45;
        model.transform.Rotate(new Vector3(
            Convert.ToInt32(axis == Axis.X),
            Convert.ToInt32(axis == Axis.Y),
            Convert.ToInt32(axis == Axis.Z)
        ), rotation);
        var renderer = model.GetComponent<MeshRenderer>();
           // Tell the camera to refocus on the new center of the object
        cameraController.Refocus(renderer.bounds.center);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
