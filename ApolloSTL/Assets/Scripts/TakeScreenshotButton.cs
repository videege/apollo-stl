using System;
using System.Collections;
using System.Collections.Generic;
using SimpleFileBrowser;
using UnityEngine;
using UnityEngine.UI;

public class TakeScreenshotButton : MonoBehaviour
{
    [SerializeField] ScreenshotGenerator screenshotGenerator;
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        FileBrowser.SetFilters(false, ".png");
        StartCoroutine(ShowSaveDialogCoroutine());
    }

    IEnumerator ShowSaveDialogCoroutine()
    {
        // Show a load file dialog and wait for a response from user
        // Load file/folder: both, Allow multiple selection: true
        // Initial path: default (Documents), Initial filename: empty
        // Title: "Load File", Submit button text: "Load"
        
        yield return FileBrowser.WaitForSaveDialog(FileBrowser.PickMode.Files,
            false, null, "screenshot.png", "Save Screenshot", "Save");
        
        // Dialog is closed
        // Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
        Debug.Log(FileBrowser.Success);

        if (FileBrowser.Success)
        {
            Debug.Log(FileBrowser.Result[0]);
            screenshotGenerator.SaveCurrentViewAsScreenshot(FileBrowser.Result[0]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
