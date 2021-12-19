using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddLightButton : MonoBehaviour
{
    [SerializeField] private LightController lightController;
    [SerializeField] private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {        
        lightController.AddLight(mainCamera.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
