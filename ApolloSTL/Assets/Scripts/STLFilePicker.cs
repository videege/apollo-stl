using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleFileBrowser;


public class STLFilePicker : MonoBehaviour
{
    public Button _button;
    [SerializeField] public STLLoader Loader;

    // Start is called before the first frame update
    void Start()
    {
        FileBrowser.SetFilters(false, ".stl");
        var button = _button.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {        
        StartCoroutine(ShowLoadDialogCoroutine());
    }

    IEnumerator ShowLoadDialogCoroutine()
	{
		// Show a load file dialog and wait for a response from user
		// Load file/folder: both, Allow multiple selection: true
		// Initial path: default (Documents), Initial filename: empty
		// Title: "Load File", Submit button text: "Load"
		yield return FileBrowser.WaitForLoadDialog( FileBrowser.PickMode.Files, false, null, null, "Load STL", "Load" );

		// Dialog is closed
		// Print whether the user has selected some files/folders or cancelled the operation (FileBrowser.Success)
		Debug.Log( FileBrowser.Success );

		if( FileBrowser.Success )
		{
            Debug.Log(FileBrowser.Result[0]);
            Loader.LoadSTL(FileBrowser.Result[0]);
		}
	}
}
