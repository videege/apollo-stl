using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZenithalLightToggle : MonoBehaviour
{
    Toggle toggle;
    [SerializeField] private LightController lightController;

    // Start is called before the first frame update
    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate {
            ToggleValueChanged(toggle);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToggleValueChanged(Toggle change)
    {
        lightController.ToggleZenithalSunlight();
    }
}
