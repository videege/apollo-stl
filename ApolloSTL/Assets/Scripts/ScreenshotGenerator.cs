using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenshotGenerator : MonoBehaviour
{
    private Camera camera;
    [SerializeField] private Canvas gui;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveCurrentViewAsScreenshot(string filePath)
    {
        
        StartCoroutine(CaptureScreenshot(filePath));
        
    }

    private IEnumerator CaptureScreenshot(string filePath)
    {
        yield return null;
        gui.enabled = false;
        yield return new WaitForEndOfFrame();
        ScreenCapture.CaptureScreenshot(filePath);     
        yield return null;   
        gui.enabled = true;
    }

    public void Save360DegreeScreenshots(string folder)
    {
        StartCoroutine(Capture360DegreeScreenshots(folder));
    }

    private IEnumerator Capture360DegreeScreenshots(string folder)
    {
        yield return null;
        gui.enabled = false;
        var targetPoint = GetComponent<CameraController>().targetPoint;

        for (var i = 0; i < 12; i++) 
        {
            camera.transform.RotateAround(targetPoint, new Vector3(0, 1, 0), 30);
            yield return null;
            var path = Path.Combine(folder, $"{i + 1}.png");
            ScreenCapture.CaptureScreenshot(path);
            yield return null;
        }

        yield return null;
        gui.enabled = true;
    }
}
