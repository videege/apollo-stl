using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class STLFilePicker : MonoBehaviour
{
    public Button _button;
    [SerializeField] public STLLoader Loader;

    // Start is called before the first frame update
    void Start()
    {
        var button = _button.GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        var path = EditorUtility.OpenFilePanel("Select STL file", "", "stl");
        if (path.Length != 0)
        {
            Debug.Log(path);
            Loader.LoadSTL(path);
        }
    }
}
