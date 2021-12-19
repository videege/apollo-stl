using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserLightToggle : MonoBehaviour
{
    Toggle toggle;
    UserLight userLight;

    public void SetLight(UserLight userLight, int number)
    {
        this.userLight = userLight;
        UpdateNumber(number);
    }

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
        if (userLight != null)
            userLight.Toggle();
    }

    void UpdateNumber(int number) 
    {
        if (toggle == null) 
            toggle = GetComponent<Toggle>();
        var text = toggle.GetComponentInChildren<Text>();
        text.text = $"Light #{number}";
    }
}
